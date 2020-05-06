namespace LADXRandomizer
{
    partial class ImportDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radioPreset = new System.Windows.Forms.RadioButton();
            this.radioValue = new System.Windows.Forms.RadioButton();
            this.radioFile = new System.Windows.Forms.RadioButton();
            this.cmbPreset = new System.Windows.Forms.ComboBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radioPreset
            // 
            this.radioPreset.AutoSize = true;
            this.radioPreset.Enabled = false;
            this.radioPreset.Location = new System.Drawing.Point(12, 35);
            this.radioPreset.Name = "radioPreset";
            this.radioPreset.Size = new System.Drawing.Size(58, 17);
            this.radioPreset.TabIndex = 0;
            this.radioPreset.Text = "Preset:";
            this.radioPreset.UseVisualStyleBackColor = true;
            // 
            // radioValue
            // 
            this.radioValue.AutoSize = true;
            this.radioValue.Location = new System.Drawing.Point(12, 12);
            this.radioValue.Name = "radioValue";
            this.radioValue.Size = new System.Drawing.Size(55, 17);
            this.radioValue.TabIndex = 1;
            this.radioValue.Text = "Value:";
            this.radioValue.UseVisualStyleBackColor = true;
            // 
            // radioFile
            // 
            this.radioFile.AutoSize = true;
            this.radioFile.Enabled = false;
            this.radioFile.Location = new System.Drawing.Point(12, 64);
            this.radioFile.Name = "radioFile";
            this.radioFile.Size = new System.Drawing.Size(44, 17);
            this.radioFile.TabIndex = 0;
            this.radioFile.Text = "File:";
            this.radioFile.UseVisualStyleBackColor = true;
            // 
            // cmbPreset
            // 
            this.cmbPreset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPreset.Enabled = false;
            this.cmbPreset.FormattingEnabled = true;
            this.cmbPreset.Items.AddRange(new object[] {
            "Standard",
            "Chaos"});
            this.cmbPreset.Location = new System.Drawing.Point(76, 34);
            this.cmbPreset.Name = "cmbPreset";
            this.cmbPreset.Size = new System.Drawing.Size(100, 21);
            this.cmbPreset.TabIndex = 1;
            this.cmbPreset.TabStop = false;
            this.cmbPreset.Enter += new System.EventHandler(this.cmb_Preset_Enter);
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(76, 11);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(100, 20);
            this.txtValue.TabIndex = 0;
            this.txtValue.Enter += new System.EventHandler(this.txt_Mask_Enter);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Enabled = false;
            this.btnBrowse.Location = new System.Drawing.Point(76, 61);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.TabStop = false;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(29, 104);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(110, 104);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // ImportDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(191, 133);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.cmbPreset);
            this.Controls.Add(this.radioFile);
            this.Controls.Add(this.radioValue);
            this.Controls.Add(this.radioPreset);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ImportDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import settings from...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioPreset;
        private System.Windows.Forms.RadioButton radioValue;
        private System.Windows.Forms.RadioButton radioFile;
        private System.Windows.Forms.ComboBox cmbPreset;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}