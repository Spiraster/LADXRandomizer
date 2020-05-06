using System;
using System.Linq;
using System.Windows.Forms;

namespace LADXRandomizer
{
    public partial class OptionsDialog : Form
    {
        public event EventHandler<OptionsArgs> SaveOptions;

        public OptionsDialog(RandomizerOptions options, int y, int x)
        {
            InitializeComponent();

            this.Top = y;
            this.Left = x;
            this.cmb_SelectedROM.SelectedIndex = 0;

            foreach (var option in options)
            {
                CheckBox checkbox = null;
                if (GetCheckBox(option.Name, ref checkbox))
                    checkbox.Checked = option.Enabled;

                ComboBox combobox = null;
                if (GetComboBox(option.Name, ref combobox))
                    combobox.SelectedIndex = option.Index;
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            var options = new RandomizerOptions();

            foreach (var option in options)
            {
                CheckBox checkbox = null;
                if (GetCheckBox(option.Name, ref checkbox))
                    option.Enabled = checkbox.Checked;

                ComboBox combobox = null;
                if (GetComboBox(option.Name, ref combobox))
                    option.Index = combobox.SelectedIndex;
            }

            this.SaveOptions?.Invoke(this, new OptionsArgs(options));
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool GetCheckBox(string search, ref CheckBox checkbox)
        {
            foreach (CheckBox control in this.Controls.OfType<CheckBox>())
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
            foreach (ComboBox control in this.Controls.OfType<ComboBox>())
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
