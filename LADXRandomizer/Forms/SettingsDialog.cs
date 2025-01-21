using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace LADXRandomizer
{
    public partial class SettingsDialog : Form
    {
        public SettingsDialog(ulong settingsValue)
        {
            InitializeComponent();

            if (Program.Debug)
            {
                chkDebugMode.Visible = true;
                chkDebugMode.Enabled = true;
            }
            
            LoadSettings(settingsValue);
        }

        private void LoadSettings(ulong settingsValue)
        {
            GetAllControls<CheckBox>().ForEach(x => x.Checked = false);

            var settings = ((Settings)settingsValue).ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var setting in settings)
                if (FindCheckBox(setting, out CheckBox checkbox))
                    if (checkbox.Enabled)
                        checkbox.Checked = true;
            
            UpdateValue();
        }

        private void UpdateValue()
        {
            var sb = new StringBuilder();

            foreach (var setting in Enum.GetNames(typeof(Settings)))
                if (FindCheckBox(setting, out CheckBox checkbox))
                    if (checkbox.Checked)
                        sb.Append(checkbox.Name.Replace("chk", "") + ", ");

            if (Enum.TryParse(sb.ToString().Trim(',', ' '), out Settings settings))
                txtValue.Text = Base62.ToBase62((uint)settings);
            else
                txtValue.Text = "0";
        }

        private bool FindCheckBox(string search, out CheckBox checkbox)
        {
            checkbox = GetAllControls<CheckBox>().Where(x => x.Name == "chk" + search).FirstOrDefault();

            return (checkbox == null) ? false : true;
        }

        private List<T> GetAllControls<T>() where T : Control
        {
            var list = new List<T>();

            foreach (var control in Controls.OfType<T>())
                list.Add(control);

            foreach (TabPage tab in tabControl1.TabPages)
            {
                foreach (var control in tab.Controls.OfType<T>())
                    list.Add(control);

                foreach (var groupbox in tab.Controls.OfType<GroupBox>())
                    foreach (var control in groupbox.Controls.OfType<T>())
                        list.Add(control);
            }

            return list;
        }

        //form methods
        private void btnOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SettingsValue = Base62.Parse(txtValue.Text);
            Properties.Settings.Default.Save();

            DialogResult = DialogResult.OK;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (var dialog = new ImportDialog())
            {
                dialog.ShowDialog();

                if (dialog.DialogResult == DialogResult.OK)
                    LoadSettings(dialog.SettingsValue);
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox)sender;

            if (checkbox.Checked)
            {
                if (checkbox.Name == "chkShuffleWarps")
                    grpWarpOptions.Enabled = true;
                else if (checkbox.Name == "chkShuffleItems")
                    grpItemOptions.Enabled = true;
            }
            else
            {
                if (checkbox.Name == "chkShuffleWarps")
                    grpWarpOptions.Enabled = false;
                else if (checkbox.Name == "chkShuffleItems")
                    grpItemOptions.Enabled = false;
            }

            UpdateValue();
        }

        private void groupBox_EnabledChanged(object sender, EventArgs e)
        {
            var groupbox = (GroupBox)sender;

            if (!groupbox.Enabled)
                foreach (var checkbox in ((GroupBox)sender).Controls.OfType<CheckBox>())
                    checkbox.Checked = false;
        }
    }
}
