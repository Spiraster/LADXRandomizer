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
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_Version = new System.Windows.Forms.Label();
            this.btn_Options = new System.Windows.Forms.Button();
            this.chk_FullLog = new System.Windows.Forms.CheckBox();
            this.btn_SaveLog = new System.Windows.Forms.Button();
            this.lbl_LogSaved = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Create
            // 
            this.btn_Create.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Create.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Create.Location = new System.Drawing.Point(447, 12);
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
            this.txt_Seed.Location = new System.Drawing.Point(12, 25);
            this.txt_Seed.Name = "txt_Seed";
            this.txt_Seed.Size = new System.Drawing.Size(429, 20);
            this.txt_Seed.TabIndex = 0;
            // 
            // txt_Log
            // 
            this.txt_Log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Log.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Log.Location = new System.Drawing.Point(12, 73);
            this.txt_Log.Multiline = true;
            this.txt_Log.Name = "txt_Log";
            this.txt_Log.ReadOnly = true;
            this.txt_Log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Log.Size = new System.Drawing.Size(510, 228);
            this.txt_Log.TabIndex = 2;
            this.txt_Log.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(244, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Enter seed or string: (leave blank for random seed)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Log:";
            // 
            // lbl_Version
            // 
            this.lbl_Version.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_Version.AutoSize = true;
            this.lbl_Version.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lbl_Version.Location = new System.Drawing.Point(4, 318);
            this.lbl_Version.Name = "lbl_Version";
            this.lbl_Version.Size = new System.Drawing.Size(37, 13);
            this.lbl_Version.TabIndex = 5;
            this.lbl_Version.Text = "v0.0.0";
            // 
            // btn_Options
            // 
            this.btn_Options.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Options.Location = new System.Drawing.Point(447, 41);
            this.btn_Options.Name = "btn_Options";
            this.btn_Options.Size = new System.Drawing.Size(75, 23);
            this.btn_Options.TabIndex = 2;
            this.btn_Options.Text = "Options >>";
            this.btn_Options.UseVisualStyleBackColor = true;
            this.btn_Options.Click += new System.EventHandler(this.btn_Options_Click);
            // 
            // chk_FullLog
            // 
            this.chk_FullLog.AutoSize = true;
            this.chk_FullLog.Location = new System.Drawing.Point(41, 56);
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
            this.btn_SaveLog.Location = new System.Drawing.Point(447, 307);
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
            this.lbl_LogSaved.Location = new System.Drawing.Point(81, 312);
            this.lbl_LogSaved.Name = "lbl_LogSaved";
            this.lbl_LogSaved.Size = new System.Drawing.Size(360, 15);
            this.lbl_LogSaved.TabIndex = 3;
            this.lbl_LogSaved.Text = "Log saved!";
            this.lbl_LogSaved.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_LogSaved.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 336);
            this.Controls.Add(this.btn_SaveLog);
            this.Controls.Add(this.chk_FullLog);
            this.Controls.Add(this.btn_Options);
            this.Controls.Add(this.lbl_Version);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_LogSaved);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Log);
            this.Controls.Add(this.txt_Seed);
            this.Controls.Add(this.btn_Create);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(550, 300);
            this.Name = "MainForm";
            this.Text = "LADX Randomizer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Create;
        private System.Windows.Forms.TextBox txt_Seed;
        private System.Windows.Forms.TextBox txt_Log;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_Version;
        private System.Windows.Forms.Button btn_Options;
        private System.Windows.Forms.CheckBox chk_FullLog;
        private System.Windows.Forms.Button btn_SaveLog;
        private System.Windows.Forms.Label lbl_LogSaved;
    }
}

