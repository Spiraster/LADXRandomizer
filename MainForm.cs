using LADXRandomizer.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LADXRandomizer.IO.RandomizerIO;

namespace LADXRandomizer
{
    public partial class MainForm : Form
    {
        private bool debug = true;

        private Randomizer randomizer;
        private RandomizerLog log;
        private Version version;

        private Stopwatch stopwatch = new Stopwatch();

        private string Filename
        {
            get { return "[V" + version.ToString(1) + "] - " + randomizer.Seed; }
        }

        public MainForm()
        {
            InitializeComponent();

            version = Assembly.GetExecutingAssembly().GetName().Version;
            lbl_Version.Text = "v" + version.ToString(3);

            cmb_Preset.SelectedIndex = 0;
            btn_Batch.Visible = debug;
            txt_BatchNum.Visible = debug;
        }

        private void CreateRom(int mask)
        {
            log = new RandomizerLog(debug);
            log.UpdateLog += log_onUpdateLog;

            var settings = new RandomizerSettings(mask);

            //var pathfinder = new Pathfinding.Pathfinder();
            //var warpdata = new WarpData(new RandomizerSettings(385));
            //warpdata.ForEach(x => x.Destination = x.Default);
            //txt_Log.SynchronizedInvoke(() => txt_Log.Text = pathfinder.IsSolvable(warpdata).ToString() + " (count = " + pathfinder.FinalCount.ToString() + ")" + "\r\n\r\n" + pathfinder.Result);

            randomizer = new Randomizer(txt_Seed.Text.Trim(' '), log, settings);

            stopwatch.Restart();
            randomizer.GenerateData();
            stopwatch.Stop();
            log.Write(LogMode.Info, "", "DONE! (" + stopwatch.ElapsedMilliseconds.ToString() + "ms)", "<l1>");

            log.LogSettings(settings);

            WriteRom(randomizer, settings, Filename);
        }

        private void BatchTest()
        {
            int tests;
            if (!int.TryParse(txt_BatchNum.Text, out tests))
                return;

            var sw = new Stopwatch();
            sw.Restart();
            for (int i = 0; i < tests; i++)
            {
                btn_Create_Click(this, EventArgs.Empty);
            }
            sw.Stop();
            MessageBox.Show("Average: " + (sw.ElapsedMilliseconds / tests).ToString() + " ms");
        }

        //event methods//
        private void log_onUpdateLog(object sender, LogArgs e)
        {
            if (e.doClear)
                txt_Log.Clear();

            txt_Log.SynchronizedInvoke(() => txt_Log.AppendText(e.Message));
        }

        //UI methods//
        private async void btn_Create_Click(object sender, EventArgs e)
        {
            txt_Log.Clear();

            btn_Create.Enabled = false;
            btn_Settings.Enabled = false;
            btn_SaveLog.Enabled = false;
            chk_FullLog.Enabled = false;
            cmb_Preset.Enabled = false;
            lbl_LogSaved.Visible = false;

            int mask = 1;
            if (cmb_Preset.SelectedIndex == 2) //custom settings
                mask = Settings.Default.OptionsMask;
            else
                mask = (int)Enum.Parse(typeof(Preset), cmb_Preset.SelectedItem.ToString());
            
            await Task.Run(() => CreateRom(mask));

            if (chk_FullLog.Checked)
                txt_Log.Text = log.FullLog;

            txt_Log.SelectionStart = 0;
            txt_Log.ScrollToCaret();

            btn_Create.Enabled = true;
            btn_SaveLog.Enabled = true;
            chk_FullLog.Enabled = true;
            cmb_Preset.Enabled = true;
            if (cmb_Preset.SelectedIndex == 2)
                btn_Settings.Enabled = true;
        }

        private void btn_Settings_Click(object sender, EventArgs e)
        {
            var dialog = new SettingsDialog(Settings.Default.OptionsMask);

            dialog.ShowDialog();
            dialog.Dispose();
        }

        private void btn_SaveLog_Click(object sender, EventArgs e)
        {
            SaveLog(log, Filename);
            lbl_LogSaved.Visible = true;
        }

        private void btn_Batch_Click(object sender, EventArgs e)
        {
            BatchTest();
        }

        private void cmb_Preset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Preset.SelectedIndex == 2)
                btn_Settings.Enabled = true;
            else
                btn_Settings.Enabled = false;
        }
    }

    public static class ExtensionMethods
    { 
        //thread-safe invoke//
        public static void SynchronizedInvoke(this ISynchronizeInvoke sync, Action action)
        {
            if (!sync.InvokeRequired)
            {
                action();
                return;
            }
            
            sync.Invoke(action, null);
        }
    }
}
