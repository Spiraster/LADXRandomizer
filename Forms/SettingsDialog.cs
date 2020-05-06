using LADXRandomizer.Properties;
using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LADXRandomizer
{
    public partial class SettingsDialog : Form
    {
        private int mask;

        public SettingsDialog(int mask)
        {
            InitializeComponent();

            this.mask = mask;

            LoadSettings();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            Settings.Default.OptionsMask = mask;
            Settings.Default.Save();

            DialogResult = DialogResult.OK;
        }

        private void btn_Import_Click(object sender, EventArgs e)
        {
            var dialog = new ImportDialog();

            dialog.ShowDialog();

            if (dialog.DialogResult == DialogResult.OK)
            {
                mask = dialog.Mask;
                LoadSettings();
            }

            dialog.Dispose();
        }

        private void LoadSettings()
        {
            var settingsMask = (SettingsMask)mask;
            var settings = settingsMask.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var combobox in Controls.OfType<ComboBox>())
                combobox.SelectedIndex = 0;

            foreach (var checkbox in Controls.OfType<CheckBox>())
                checkbox.Checked = false;

            foreach (var setting in settings)
            {
                if (setting.Contains("SelectedROM"))
                {
                    cmb_SelectedROM.SelectedIndex = Convert.ToInt32(setting.Split('_')[1]);
                }
                else
                {
                    CheckBox checkbox = null;
                    if (GetCheckBox(setting, ref checkbox))
                        checkbox.Checked = true;
                }
            }

            CheckControls();
            UpdateMask();
        }

        private void UpdateMask()
        {
            var sb = new StringBuilder();

            foreach (var setting in new RandomizerSettings())
            {
                CheckBox checkbox = null;
                if (GetCheckBox(setting.Name, ref checkbox))
                {
                    if (checkbox.Checked)
                        sb.Append(checkbox.Name.Replace("chk_", "") + ", ");
                }

                ComboBox combobox = null;
                if (GetComboBox(setting.Name, ref combobox))
                {
                    sb.Append(combobox.Name.Replace("cmb_", "") + "_" + combobox.SelectedIndex + ", ");
                }
            }

            if (Enum.TryParse(sb.ToString().Trim(',', ' '), out SettingsMask settingsMask))
                mask = (int)settingsMask;

            Text = "Settings (Mask = " + mask.ToString() + ")";
        }

        private void CheckControls()
        {
            if (chk_CheckSolvability.Checked)
            {
                chk_PairWarps.Checked = true;
                chk_PairWarps.Enabled = false;

                chk_PreventInaccessible.Checked = true;
                chk_PreventInaccessible.Enabled = false;

                chk_ExcludeMarinHouse.Checked = false;
                chk_ExcludeMarinHouse.Enabled = false;

                chk_ExcludeEgg.Checked = false;
                chk_ExcludeEgg.Enabled = false;
            }
            else
            {
                chk_PairWarps.Enabled = true;
                chk_PreventInaccessible.Enabled = true;
                chk_ExcludeMarinHouse.Enabled = true;
                chk_ExcludeEgg.Enabled = true;
            }
        }

        private void chk_Click(object sender, EventArgs e)
        {
            CheckControls();
            UpdateMask();
        }

        private bool GetCheckBox(string search, ref CheckBox checkbox)
        {
            foreach (CheckBox control in Controls.OfType<CheckBox>())
            {
                if (control.Name == "chk_" + search)
                {
                    checkbox = control;
                    return true;
                }
            }

            return false;
        }

        private bool GetComboBox(string search, ref ComboBox combobox)
        {
            foreach (ComboBox control in Controls.OfType<ComboBox>())
            {
                if (control.Name == "cmb_" + search)
                {
                    combobox = control;
                    return true;
                }
            }

            return false;
        }
    }
}
