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
using LADXRandomizer.Properties;

namespace LADXRandomizer
{
    public partial class MainForm : Form
    {
        private Log log;
        private string filename;
        private Stopwatch stopwatch = new Stopwatch();

        private Version version = Assembly.GetExecutingAssembly().GetName().Version;

        public MainForm(bool debug)
        {
            InitializeComponent();
            
            lblVersion.Text = "v" + version.ToString(3);

            cmbPreset.SelectedIndex = 1;
            btnBulkCreate.Visible = Program.Debug;
            txtBatchNum.Visible = Program.Debug;

            var settings = (Settings)Properties.Settings.Default.SettingsValue;
            if (settings.HasFlag(Settings.DebugMode) && !Program.Debug)
            {
                settings &= ~Settings.DebugMode;
                Properties.Settings.Default.SettingsValue = (uint)settings;
            }
        }

        public void CreateRom(Settings settings)
        {
            var rom = new ROMBuffer(Resources.romJ10);
            var seed = Randomizer.GetSeed(txtSeed.Text);
            var rng = new MT19937_64(Base62.Parse(seed));

            log = new Log(seed, settings);
            log.UpdateLog += log_onUpdateLog;
            log.Print("Seed: " + seed, "Settings: " + Base62.ToBase62((uint)settings), "");

            rom.ApplySettings(settings);

            if (true)
                Randomizer.ShuffleThemes(rng, ref rom);

            rom.ApplyScreenEdits();

            if (settings.HasFlag(Settings.ShuffleWarps))
            {
                log.Print("Shuffling warps...");
                (var warpList, var success) = Randomizer.ShuffleWarps(rng, settings, log);
                rom.UpdateWarps(warpList);
                log.RecordWarps(warpList, settings);
            }

            filename = "V" + version.ToString(1) + "_" + seed;
            rom.Save(filename, settings);
        }

        private void BulkCreate()
        {
            //if (!int.TryParse(txt_BatchNum.Text, out int tests))
            //    return;

            //var successRate = 0;
            //var sw = new Stopwatch();
            //sw.Restart();
            //for (int i = 0; i < tests; i++)
            //{
            //    var rng = new MT19937(Randomizer.GetSeed(""));
            //    (var warpData, var success) = Randomizer.GenerateData(rng, Randomizer.GenerateMapEdits(rng), new RandomizerSettings(385), new RandomizerLog(debug));
            //    if (success)
            //        successRate++;
            //}
            //sw.Stop();

            //MessageBox.Show("Average Time: " + (sw.ElapsedMilliseconds * (1.0 / tests)).ToString() + " ms\n" + "Success Rate: " + (successRate * 1.0 / tests).ToString("p"));
        }

        //event methods//
        private void log_onUpdateLog(object sender, LogArgs e)
        {
            txtLog.SynchronizedInvoke(() => txtLog.AppendText(e.Message));
        }

        //form methods//
        private async void btnCreate_Click(object sender, EventArgs e)
        {
            txtLog.Clear();

            btnCreateROM.Enabled = false;
            btnCustomize.Enabled = false;
            btnSaveSpoiler.Enabled = false;
            btnBulkCreate.Enabled = false;
            cmbPreset.Enabled = false;

            Settings settings = 0;
            if (cmbPreset.SelectedIndex == 1) //custom settings
                settings = (Settings)Properties.Settings.Default.SettingsValue;
            else if (Enum.TryParse(cmbPreset.SelectedItem.ToString(), out SettingPreset preset))
                settings = (Settings)preset;

            if (Program.GetFrameworkVersion() < 460798)
                txtLog.AppendText("ERROR: This program requires .Net Framework 4.7 or later.\n(https://www.microsoft.com/net/download/framework)");
            else
            {
                progressBar1.Style = ProgressBarStyle.Marquee;
                stopwatch.Restart();
                await Task.Run(() => CreateRom(settings));
                stopwatch.Stop();
                progressBar1.Style = ProgressBarStyle.Continuous;

                log.Print("DONE! (" + stopwatch.ElapsedMilliseconds + "ms)");
            }

            //txt_Log.SelectionStart = 0;
            //txt_Log.ScrollToCaret();

            btnCreateROM.Enabled = true;
            btnCustomize.Enabled = true;
            btnSaveSpoiler.Enabled = true;
            btnBulkCreate.Enabled = true;
            cmbPreset.Enabled = true;
        }

        private void btnCustomize_Click(object sender, EventArgs e)
        {
            cmbPreset.SelectedIndex = 1;

            using (var dialog = new SettingsDialog(Properties.Settings.Default.SettingsValue))
                dialog.ShowDialog();
        }

        private void btnSaveLog_Click(object sender, EventArgs e)
        {
            log.Save(filename);
            log.Print("Spoiler successfully saved!");
        }

        private void cmbPreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            //update displayed mask
        }

        private void btnBatch_Click(object sender, EventArgs e)
        {
            BulkCreate();
        }
    }
}
