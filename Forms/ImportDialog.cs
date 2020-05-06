using LADXRandomizer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LADXRandomizer
{
    public partial class ImportDialog : Form
    {
        private ulong settingsValue;
        public ulong SettingsValue => settingsValue;

        public ImportDialog()
        {
            InitializeComponent();

            cmbPreset.SelectedIndex = 0;
            radioValue.Checked = true;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (radioValue.Checked)
            {
                if (Base62.TryParse(txtValue.Text, out ulong value))
                {
                    if (value >= (ulong)Settings.Max)
                        value = (ulong)(Settings.Max - 1);

                    settingsValue = value;
                    DialogResult = DialogResult.OK;
                }
                else
                    MessageBox.Show("Entered value contains invalid characters.");
            }
        }

        private void cmb_Preset_Enter(object sender, EventArgs e)
        {
            radioPreset.Checked = true;
        }

        private void txt_Mask_Enter(object sender, EventArgs e)
        {
            radioValue.Checked = true;
        }
    }
}
