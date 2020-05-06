namespace LADXRandomizer
{
    partial class SettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Import = new System.Windows.Forms.Button();
            this.cmb_SelectedROM = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chk_IncludeMarinHouse = new System.Windows.Forms.CheckBox();
            this.chk_IncludeEgg = new System.Windows.Forms.CheckBox();
            this.chk_PreventDefaultWarps = new System.Windows.Forms.CheckBox();
            this.chk_PreventInaccessible = new System.Windows.Forms.CheckBox();
            this.chk_CheckSolvability = new System.Windows.Forms.CheckBox();
            this.chk_PairWarps = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_OK.Location = new System.Drawing.Point(131, 187);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(212, 187);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_Import
            // 
            this.btn_Import.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Import.Location = new System.Drawing.Point(6, 187);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(75, 23);
            this.btn_Import.TabIndex = 6;
            this.btn_Import.Text = "Import...";
            this.btn_Import.UseVisualStyleBackColor = true;
            this.btn_Import.Click += new System.EventHandler(this.btn_Import_Click);
            // 
            // cmb_SelectedROM
            // 
            this.cmb_SelectedROM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_SelectedROM.FormattingEnabled = true;
            this.cmb_SelectedROM.Items.AddRange(new object[] {
            "v1.0 (J)",
            "v1.0 (U)",
            "v1.2 (U)"});
            this.cmb_SelectedROM.Location = new System.Drawing.Point(118, 8);
            this.cmb_SelectedROM.Name = "cmb_SelectedROM";
            this.cmb_SelectedROM.Size = new System.Drawing.Size(121, 21);
            this.cmb_SelectedROM.TabIndex = 3;
            this.cmb_SelectedROM.SelectedIndexChanged += new System.EventHandler(this.chk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select ROM to use:";
            // 
            // chk_IncludeMarinHouse
            // 
            this.chk_IncludeMarinHouse.AutoSize = true;
            this.chk_IncludeMarinHouse.Location = new System.Drawing.Point(12, 127);
            this.chk_IncludeMarinHouse.Name = "chk_IncludeMarinHouse";
            this.chk_IncludeMarinHouse.Size = new System.Drawing.Size(176, 17);
            this.chk_IncludeMarinHouse.TabIndex = 2;
            this.chk_IncludeMarinHouse.Text = "Randomize Marin/Tarin\'s house";
            this.chk_IncludeMarinHouse.UseVisualStyleBackColor = true;
            this.chk_IncludeMarinHouse.Click += new System.EventHandler(this.chk_Click);
            // 
            // chk_IncludeEgg
            // 
            this.chk_IncludeEgg.AutoSize = true;
            this.chk_IncludeEgg.Location = new System.Drawing.Point(12, 150);
            this.chk_IncludeEgg.Name = "chk_IncludeEgg";
            this.chk_IncludeEgg.Size = new System.Drawing.Size(176, 17);
            this.chk_IncludeEgg.TabIndex = 2;
            this.chk_IncludeEgg.Text = "Randomize the Wind Fish\'s Egg";
            this.chk_IncludeEgg.UseVisualStyleBackColor = true;
            this.chk_IncludeEgg.Click += new System.EventHandler(this.chk_Click);
            // 
            // chk_PreventDefaultWarps
            // 
            this.chk_PreventDefaultWarps.AutoSize = true;
            this.chk_PreventDefaultWarps.Location = new System.Drawing.Point(12, 104);
            this.chk_PreventDefaultWarps.Name = "chk_PreventDefaultWarps";
            this.chk_PreventDefaultWarps.Size = new System.Drawing.Size(189, 17);
            this.chk_PreventDefaultWarps.TabIndex = 2;
            this.chk_PreventDefaultWarps.Text = "Prevent default warp combinations";
            this.chk_PreventDefaultWarps.UseVisualStyleBackColor = true;
            this.chk_PreventDefaultWarps.Click += new System.EventHandler(this.chk_Click);
            // 
            // chk_PreventInaccessible
            // 
            this.chk_PreventInaccessible.AutoSize = true;
            this.chk_PreventInaccessible.Location = new System.Drawing.Point(12, 81);
            this.chk_PreventInaccessible.Name = "chk_PreventInaccessible";
            this.chk_PreventInaccessible.Size = new System.Drawing.Size(201, 17);
            this.chk_PreventInaccessible.TabIndex = 2;
            this.chk_PreventInaccessible.Text = "Prevent physically inaccessible areas";
            this.chk_PreventInaccessible.UseVisualStyleBackColor = true;
            this.chk_PreventInaccessible.Click += new System.EventHandler(this.chk_Click);
            // 
            // chk_CheckSolvability
            // 
            this.chk_CheckSolvability.AutoSize = true;
            this.chk_CheckSolvability.Location = new System.Drawing.Point(12, 35);
            this.chk_CheckSolvability.Name = "chk_CheckSolvability";
            this.chk_CheckSolvability.Size = new System.Drawing.Size(156, 17);
            this.chk_CheckSolvability.TabIndex = 2;
            this.chk_CheckSolvability.Text = "Make sure ROM is solvable";
            this.chk_CheckSolvability.UseVisualStyleBackColor = true;
            this.chk_CheckSolvability.Click += new System.EventHandler(this.chk_Click);
            // 
            // chk_PairWarps
            // 
            this.chk_PairWarps.AutoSize = true;
            this.chk_PairWarps.Location = new System.Drawing.Point(12, 58);
            this.chk_PairWarps.Name = "chk_PairWarps";
            this.chk_PairWarps.Size = new System.Drawing.Size(133, 17);
            this.chk_PairWarps.TabIndex = 2;
            this.chk_PairWarps.Text = "Connect warps in pairs";
            this.chk_PairWarps.UseVisualStyleBackColor = true;
            this.chk_PairWarps.Click += new System.EventHandler(this.chk_Click);
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 216);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chk_IncludeMarinHouse);
            this.Controls.Add(this.btn_Import);
            this.Controls.Add(this.cmb_SelectedROM);
            this.Controls.Add(this.chk_PairWarps);
            this.Controls.Add(this.chk_IncludeEgg);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.chk_CheckSolvability);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.chk_PreventInaccessible);
            this.Controls.Add(this.chk_PreventDefaultWarps);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Import;
        private System.Windows.Forms.ComboBox cmb_SelectedROM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chk_IncludeMarinHouse;
        private System.Windows.Forms.CheckBox chk_IncludeEgg;
        private System.Windows.Forms.CheckBox chk_PreventDefaultWarps;
        private System.Windows.Forms.CheckBox chk_PreventInaccessible;
        private System.Windows.Forms.CheckBox chk_CheckSolvability;
        private System.Windows.Forms.CheckBox chk_PairWarps;
    }
}