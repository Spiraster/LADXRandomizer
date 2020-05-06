namespace LADXRandomizer
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btn_Create = new System.Windows.Forms.Button();
            this.txt_Seed = new System.Windows.Forms.TextBox();
            this.txt_Log = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Version = new System.Windows.Forms.Label();
            this.btn_Settings = new System.Windows.Forms.Button();
            this.chk_FullLog = new System.Windows.Forms.CheckBox();
            this.btn_SaveLog = new System.Windows.Forms.Button();
            this.lbl_LogSaved = new System.Windows.Forms.Label();
            this.btn_Batch = new System.Windows.Forms.Button();
            this.txt_BatchNum = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_Preset = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Create
            // 
            this.btn_Create.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Create.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Create.Location = new System.Drawing.Point(251, 58);
            this.btn_Create.Name = "btn_Create";
            this.btn_Create.Size = new System.Drawing.Size(75, 23);
            this.btn_Create.TabIndex = 1;
            this.btn_Create.Text = "Create";
            this.btn_Create.UseVisualStyleBackColor = true;
            this.btn_Create.Click += new System.EventHandler(this.btn_Create_Click);
            // 
            // txt_Seed
            // 
            this.txt_Seed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Seed.Location = new System.Drawing.Point(6, 32);
            this.txt_Seed.Name = "txt_Seed";
            this.txt_Seed.Size = new System.Drawing.Size(320, 20);
            this.txt_Seed.TabIndex = 0;
            this.txt_Seed.Text = "test";
            // 
            // txt_Log
            // 
            this.txt_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Log.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Log.Location = new System.Drawing.Point(3, 16);
            this.txt_Log.Multiline = true;
            this.txt_Log.Name = "txt_Log";
            this.txt_Log.ReadOnly = true;
            this.txt_Log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Log.Size = new System.Drawing.Size(515, 259);
            this.txt_Log.TabIndex = 2;
            this.txt_Log.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(244, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Enter seed or string: (leave blank for random seed)";
            // 
            // lbl_Version
            // 
            this.lbl_Version.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_Version.AutoSize = true;
            this.lbl_Version.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lbl_Version.Location = new System.Drawing.Point(4, 401);
            this.lbl_Version.Name = "lbl_Version";
            this.lbl_Version.Size = new System.Drawing.Size(37, 13);
            this.lbl_Version.TabIndex = 5;
            this.lbl_Version.Text = "v0.0.0";
            // 
            // btn_Settings
            // 
            this.btn_Settings.Enabled = false;
            this.btn_Settings.Location = new System.Drawing.Point(99, 56);
            this.btn_Settings.Name = "btn_Settings";
            this.btn_Settings.Size = new System.Drawing.Size(75, 23);
            this.btn_Settings.TabIndex = 2;
            this.btn_Settings.Text = "Settings...";
            this.btn_Settings.UseVisualStyleBackColor = true;
            this.btn_Settings.Click += new System.EventHandler(this.btn_Settings_Click);
            // 
            // chk_FullLog
            // 
            this.chk_FullLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chk_FullLog.AutoSize = true;
            this.chk_FullLog.Location = new System.Drawing.Point(165, 62);
            this.chk_FullLog.Name = "chk_FullLog";
            this.chk_FullLog.Size = new System.Drawing.Size(80, 17);
            this.chk_FullLog.TabIndex = 6;
            this.chk_FullLog.Text = "Print full log";
            this.chk_FullLog.UseVisualStyleBackColor = true;
            // 
            // btn_SaveLog
            // 
            this.btn_SaveLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_SaveLog.Enabled = false;
            this.btn_SaveLog.Location = new System.Drawing.Point(458, 390);
            this.btn_SaveLog.Name = "btn_SaveLog";
            this.btn_SaveLog.Size = new System.Drawing.Size(75, 23);
            this.btn_SaveLog.TabIndex = 2;
            this.btn_SaveLog.Text = "Save Log";
            this.btn_SaveLog.UseVisualStyleBackColor = true;
            this.btn_SaveLog.Click += new System.EventHandler(this.btn_SaveLog_Click);
            // 
            // lbl_LogSaved
            // 
            this.lbl_LogSaved.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_LogSaved.Location = new System.Drawing.Point(92, 395);
            this.lbl_LogSaved.Name = "lbl_LogSaved";
            this.lbl_LogSaved.Size = new System.Drawing.Size(360, 15);
            this.lbl_LogSaved.TabIndex = 3;
            this.lbl_LogSaved.Text = "Log saved!";
            this.lbl_LogSaved.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_LogSaved.Visible = false;
            // 
            // btn_Batch
            // 
            this.btn_Batch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Batch.Location = new System.Drawing.Point(47, 390);
            this.btn_Batch.Name = "btn_Batch";
            this.btn_Batch.Size = new System.Drawing.Size(75, 23);
            this.btn_Batch.TabIndex = 7;
            this.btn_Batch.Text = "Batch";
            this.btn_Batch.UseVisualStyleBackColor = true;
            this.btn_Batch.Visible = false;
            this.btn_Batch.Click += new System.EventHandler(this.btn_Batch_Click);
            // 
            // txt_BatchNum
            // 
            this.txt_BatchNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txt_BatchNum.Location = new System.Drawing.Point(128, 392);
            this.txt_BatchNum.Name = "txt_BatchNum";
            this.txt_BatchNum.Size = new System.Drawing.Size(51, 20);
            this.txt_BatchNum.TabIndex = 8;
            this.txt_BatchNum.Text = "100";
            this.txt_BatchNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_Seed);
            this.groupBox1.Controls.Add(this.btn_Create);
            this.groupBox1.Controls.Add(this.chk_FullLog);
            this.groupBox1.Location = new System.Drawing.Point(201, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 87);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Generation";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cmb_Preset);
            this.groupBox2.Controls.Add(this.btn_Settings);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(183, 87);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Preset:";
            // 
            // cmb_Preset
            // 
            this.cmb_Preset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Preset.FormattingEnabled = true;
            this.cmb_Preset.Items.AddRange(new object[] {
            "Standard",
            "Chaos",
            "Custom..."});
            this.cmb_Preset.Location = new System.Drawing.Point(53, 16);
            this.cmb_Preset.Name = "cmb_Preset";
            this.cmb_Preset.Size = new System.Drawing.Size(121, 21);
            this.cmb_Preset.TabIndex = 12;
            this.cmb_Preset.SelectedIndexChanged += new System.EventHandler(this.cmb_Preset_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.txt_Log);
            this.groupBox3.Location = new System.Drawing.Point(12, 106);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(521, 278);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Log";
            // 
            // MainForm
            // 
            this.AcceptButton = this.btn_Create;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 419);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txt_BatchNum);
            this.Controls.Add(this.btn_Batch);
            this.Controls.Add(this.btn_SaveLog);
            this.Controls.Add(this.lbl_Version);
            this.Controls.Add(this.lbl_LogSaved);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(550, 300);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LADX Randomizer";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Create;
        private System.Windows.Forms.TextBox txt_Seed;
        private System.Windows.Forms.TextBox txt_Log;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Version;
        private System.Windows.Forms.Button btn_Settings;
        private System.Windows.Forms.CheckBox chk_FullLog;
        private System.Windows.Forms.Button btn_SaveLog;
        private System.Windows.Forms.Label lbl_LogSaved;
        private System.Windows.Forms.Button btn_Batch;
        private System.Windows.Forms.TextBox txt_BatchNum;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmb_Preset;
        private System.Windows.Forms.Label label2;
    }
}

