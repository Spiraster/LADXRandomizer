using LADXRandomizer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using LADXRandomizer.IO;

namespace LADXRandomizer
{
    public partial class MainForm : Form
    {
        private bool debug = true;
        
        private RandomizerLog log;
        private string filename;

        private Version version => Assembly.GetExecutingAssembly().GetName().Version;

        private Stopwatch stopwatch = new Stopwatch();
        
        public MainForm()
        {
            InitializeComponent();
            
            lbl_Version.Text = "v" + version.ToString(3);

            cmb_Preset.SelectedIndex = 0;
            btn_Batch.Visible = debug;
            txt_BatchNum.Visible = debug;
        }

        private void CreateRom(int mask)
        {
            log = new RandomizerLog(debug);
            log.UpdateLog += log_onUpdateLog;

            //var settings = new RandomizerSettings(mask);
            var settings = new RandomizerSettings(385);

            var seed = Randomizer.GetSeed(txt_Seed.Text);
            var rng = new MT19937(seed);

            log.Write(LogMode.Info, "Seed: " + seed.ToString("X8"));

            var mapEdits = Randomizer.GenerateMapEdits(rng);
            (var warpData, var success) = Randomizer.GenerateData(rng, mapEdits, settings, log);

            //stopwatch.Restart();
            //randomizer.GenerateData();
            //stopwatch.Stop();
            //log.Write(LogMode.Info, "", "DONE! (" + stopwatch.ElapsedMilliseconds.ToString() + "ms)", "<l1>");

            log.LogSettings(settings);

            foreach (var warp1 in warpData.Overworld1)
            {
                var warp2 = warp1.GetDestinationWarp();

                string text1 = "[" + warp1.Code + "] " + warp1.Description;

                string text2 = "";
                if (warp2 != null)
                    text2 = "[" + warp2.Code + "] " + warp2.Description;

                log.Write(LogMode.Spoiler, text1 + "\r\n    ^=> " + text2 + "\r\n");
            }

            filename = "[V" + version.ToString(1) + "] - " + seed.ToString("X8");

            RandomizerIO.WriteRom(warpData, mapEdits, seed, settings, filename);
        }

        private void BatchTest()
        {
            if (!int.TryParse(txt_BatchNum.Text, out int tests))
                return;

            var successRate = 0;
            var sw = new Stopwatch();
            sw.Restart();
            for (int i = 0; i < tests; i++)
            {
                var rng = new MT19937(Randomizer.GetSeed(""));
                (var warpData, var success) = Randomizer.GenerateData(rng, Randomizer.GenerateMapEdits(rng), new RandomizerSettings(385), new RandomizerLog(debug));
                if (success)
                    successRate++;
            }
            sw.Stop();

            MessageBox.Show("Average Time: " + (sw.ElapsedMilliseconds * (1.0 / tests)).ToString() + " ms\n" + "Success Rate: " + (successRate * 1.0 / tests).ToString("p"));
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
            RandomizerIO.SaveLog(log, filename);
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

        private void button1_Click(object sender, EventArgs e)
        {
            
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

        //get random item from a list//
        public static T Random<T>(this IEnumerable<T> list, MT19937 rng) => list.ElementAt(rng.Next(list.Count()));

        ////randomly shuffle the items in a list//
        //public static List<T> Shuffle<T>(this List<T> oldList, MT19937 rng)
        //{
        //    int count = oldList.Count;
        //    var newList = new List<T>(oldList);
        //    for (int i = 0; i < count - 1; i++)
        //    {
        //        int k = rng.Next(i, count);
        //        T temp = newList[i];
        //        newList[i] = newList[k];
        //        newList[k] = temp;
        //    }

        //    return newList;
        //}
    }
}
