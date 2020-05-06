using System;
using System.Reflection;
using System.Windows.Forms;
using static LADXRandomizer.IO.RandomizerIO;

namespace LADXRandomizer
{
    public partial class MainForm : Form
    {
        private Randomizer randomizer;
        private RandomizerLog log;
        private RandomizerOptions options;

        private Version version;

        private string Filename
        {
            get { return "[V" + version.ToString(1) + "] - " + randomizer.Seed; }
        }

        public MainForm()
        {
            InitializeComponent();

            version = Assembly.GetExecutingAssembly().GetName().Version;
            lbl_Version.Text = "v" + version.ToString(3);

            options = LoadOptions();
        }

        void CreateRom()
        {
            log = new RandomizerLog(true);
            log.UpdateLog += log_onUpdateLog;

            randomizer = new Randomizer(txt_Seed.Text.Trim(' '), log, options);

            randomizer.GenerateData();
            log.Write(LogMode.Info, "", "DONE!", "<l1>");

            log.LogSettings(options);

            WriteRom(randomizer, options, Filename);
        }

        //event methods//
        private void log_onUpdateLog(object sender, LogArgs e)
        {
            if (e.doClear)
                txt_Log.Clear();

            txt_Log.AppendText(e.Message);
        }

        private void dialog_onSaveOptions(object sender, OptionsArgs e)
        {
            options = e.Options;
            SaveOptions(options);
        }

        //button clicks//
        private void btn_Create_Click(object sender, EventArgs e)
        {
            btn_SaveLog.Enabled = false;
            lbl_LogSaved.Visible = false;
            txt_Log.Clear();

            CreateRom();

            btn_SaveLog.Enabled = true;

            if (chk_FullLog.Checked)
                txt_Log.Text = log.FullLog;

            txt_Log.SelectionStart = 0;
            txt_Log.ScrollToCaret();
        }

        private void btn_Options_Click(object sender, EventArgs e)
        {
            var dialog = new OptionsDialog(options, this.Top, this.Right);
            dialog.SaveOptions += dialog_onSaveOptions;
            dialog.ShowDialog();
        }

        private void btn_SaveLog_Click(object sender, EventArgs e)
        {
            SaveLog(log, Filename);
            lbl_LogSaved.Visible = true;
        }
    }
}
