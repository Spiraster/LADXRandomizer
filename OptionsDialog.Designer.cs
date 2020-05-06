namespace LADXRandomizer
{
    partial class OptionsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsDialog));
            this.btn_OK = new System.Windows.Forms.Button();
            this.chk_IncludeMarinHouse = new System.Windows.Forms.CheckBox();
            this.chk_IncludeEgg = new System.Windows.Forms.CheckBox();
            this.chk_PreventDefaultWarps = new System.Windows.Forms.CheckBox();
            this.cmb_SelectedROM = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chk_PreventInaccessible = new System.Windows.Forms.CheckBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_OK.Location = new System.Drawing.Point(104, 179);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // chk_IncludeMarinHouse
            // 
            this.chk_IncludeMarinHouse.AutoSize = true;
            this.chk_IncludeMarinHouse.Location = new System.Drawing.Point(12, 37);
            this.chk_IncludeMarinHouse.Name = "chk_IncludeMarinHouse";
            this.chk_IncludeMarinHouse.Size = new System.Drawing.Size(208, 17);
            this.chk_IncludeMarinHouse.TabIndex = 2;
            this.chk_IncludeMarinHouse.Text = "Include Marin\'s house in randomization";
            this.chk_IncludeMarinHouse.UseVisualStyleBackColor = true;
            // 
            // chk_IncludeEgg
            // 
            this.chk_IncludeEgg.AutoSize = true;
            this.chk_IncludeEgg.Location = new System.Drawing.Point(12, 60);
            this.chk_IncludeEgg.Name = "chk_IncludeEgg";
            this.chk_IncludeEgg.Size = new System.Drawing.Size(219, 17);
            this.chk_IncludeEgg.TabIndex = 2;
            this.chk_IncludeEgg.Text = "Include Wind Fish\'s Egg in randomization";
            this.chk_IncludeEgg.UseVisualStyleBackColor = true;
            // 
            // chk_PreventDefaultWarps
            // 
            this.chk_PreventDefaultWarps.AutoSize = true;
            this.chk_PreventDefaultWarps.Location = new System.Drawing.Point(12, 83);
            this.chk_PreventDefaultWarps.Name = "chk_PreventDefaultWarps";
            this.chk_PreventDefaultWarps.Size = new System.Drawing.Size(189, 17);
            this.chk_PreventDefaultWarps.TabIndex = 2;
            this.chk_PreventDefaultWarps.Text = "Prevent default warp combinations";
            this.chk_PreventDefaultWarps.UseVisualStyleBackColor = true;
            // 
            // cmb_SelectedROM
            // 
            this.cmb_SelectedROM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_SelectedROM.FormattingEnabled = true;
            this.cmb_SelectedROM.Items.AddRange(new object[] {
            "v1.0 (J)",
            "v1.0 (U)",
            "v1.2 (U)"});
            this.cmb_SelectedROM.Location = new System.Drawing.Point(119, 10);
            this.cmb_SelectedROM.Name = "cmb_SelectedROM";
            this.cmb_SelectedROM.Size = new System.Drawing.Size(121, 21);
            this.cmb_SelectedROM.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select ROM to use:";
            // 
            // chk_PreventInaccessible
            // 
            this.chk_PreventInaccessible.AutoSize = true;
            this.chk_PreventInaccessible.Location = new System.Drawing.Point(12, 106);
            this.chk_PreventInaccessible.Name = "chk_PreventInaccessible";
            this.chk_PreventInaccessible.Size = new System.Drawing.Size(225, 17);
            this.chk_PreventInaccessible.TabIndex = 2;
            this.chk_PreventInaccessible.Text = "Prevent any areas from being inaccessible";
            this.chk_PreventInaccessible.UseVisualStyleBackColor = true;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.Location = new System.Drawing.Point(185, 179);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // OptionsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 208);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_SelectedROM);
            this.Controls.Add(this.chk_PreventInaccessible);
            this.Controls.Add(this.chk_PreventDefaultWarps);
            this.Controls.Add(this.chk_IncludeEgg);
            this.Controls.Add(this.chk_IncludeMarinHouse);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OptionsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.CheckBox chk_IncludeMarinHouse;
        private System.Windows.Forms.CheckBox chk_IncludeEgg;
        private System.Windows.Forms.CheckBox chk_PreventDefaultWarps;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_SelectedROM;
        private System.Windows.Forms.CheckBox chk_PreventInaccessible;
        private System.Windows.Forms.Button btn_Cancel;
    }
}