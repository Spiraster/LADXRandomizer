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
        public int Mask { get; set; }

        public ImportDialog()
        {
            InitializeComponent();

            cmb_Preset.SelectedIndex = 0;
            radioButton1.Checked = true;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Mask = (int)Enum.Parse(typeof(Preset), cmb_Preset.SelectedItem.ToString());
                DialogResult = DialogResult.OK;
            }
            else if (radioButton2.Checked)
            {
                int mask;
                if (!int.TryParse(txt_Mask.Text, out mask) || mask < 1 || mask > 509)
                    MessageBox.Show("Mask must be between 1-252.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (((SettingsMask)mask).HasFlag(SettingsMask.SelectedROM_0 | SettingsMask.SelectedROM_1) 
                         || ((SettingsMask)mask).HasFlag(SettingsMask.SelectedROM_1 | SettingsMask.SelectedROM_2) 
                         || ((SettingsMask)mask).HasFlag(SettingsMask.SelectedROM_0 | SettingsMask.SelectedROM_2))
                    MessageBox.Show("The entered mask is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    Mask = mask;
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void cmb_Preset_Enter(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        private void txt_Mask_Enter(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
        }
    }
}
