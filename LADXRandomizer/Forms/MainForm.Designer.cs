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
            this.btnCreateROM = new System.Windows.Forms.Button();
            this.txtSeed = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnCustomize = new System.Windows.Forms.Button();
            this.btnSaveSpoiler = new System.Windows.Forms.Button();
            this.btnBulkCreate = new System.Windows.Forms.Button();
            this.txtBatchNum = new System.Windows.Forms.TextBox();
            this.grpGeneration = new System.Windows.Forms.GroupBox();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbPreset = new System.Windows.Forms.ComboBox();
            this.grpLog = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.grpGeneration.SuspendLayout();
            this.grpOptions.SuspendLayout();
            this.grpLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateROM
            // 
            this.btnCreateROM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateROM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateROM.Location = new System.Drawing.Point(158, 58);
            this.btnCreateROM.Name = "btnCreateROM";
            this.btnCreateROM.Size = new System.Drawing.Size(75, 23);
            this.btnCreateROM.TabIndex = 1;
            this.btnCreateROM.Text = "Create ROM";
            this.btnCreateROM.UseVisualStyleBackColor = true;
            this.btnCreateROM.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // txtSeed
            // 
            this.txtSeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSeed.Location = new System.Drawing.Point(6, 32);
            this.txtSeed.Name = "txtSeed";
            this.txtSeed.Size = new System.Drawing.Size(308, 20);
            this.txtSeed.TabIndex = 0;
            this.txtSeed.Text = "test";
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(3, 16);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(504, 222);
            this.txtLog.TabIndex = 2;
            this.txtLog.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Enter seed string: (leave blank for random seed)";
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblVersion.AutoSize = true;
            this.lblVersion.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblVersion.Location = new System.Drawing.Point(4, 343);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(37, 13);
            this.lblVersion.TabIndex = 5;
            this.lblVersion.Text = "v0.0.0";
            // 
            // btnCustomize
            // 
            this.btnCustomize.Location = new System.Drawing.Point(102, 58);
            this.btnCustomize.Name = "btnCustomize";
            this.btnCustomize.Size = new System.Drawing.Size(75, 23);
            this.btnCustomize.TabIndex = 2;
            this.btnCustomize.Text = "Customize...";
            this.btnCustomize.UseVisualStyleBackColor = true;
            this.btnCustomize.Click += new System.EventHandler(this.btnCustomize_Click);
            // 
            // btnSaveSpoiler
            // 
            this.btnSaveSpoiler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveSpoiler.Enabled = false;
            this.btnSaveSpoiler.Location = new System.Drawing.Point(239, 58);
            this.btnSaveSpoiler.Name = "btnSaveSpoiler";
            this.btnSaveSpoiler.Size = new System.Drawing.Size(75, 23);
            this.btnSaveSpoiler.TabIndex = 2;
            this.btnSaveSpoiler.Text = "Save Spoiler";
            this.btnSaveSpoiler.UseVisualStyleBackColor = true;
            this.btnSaveSpoiler.Click += new System.EventHandler(this.btnSaveLog_Click);
            // 
            // btnBulkCreate
            // 
            this.btnBulkCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBulkCreate.Location = new System.Drawing.Point(6, 58);
            this.btnBulkCreate.Name = "btnBulkCreate";
            this.btnBulkCreate.Size = new System.Drawing.Size(51, 23);
            this.btnBulkCreate.TabIndex = 7;
            this.btnBulkCreate.Text = "Bulk";
            this.btnBulkCreate.UseVisualStyleBackColor = true;
            this.btnBulkCreate.Visible = false;
            this.btnBulkCreate.Click += new System.EventHandler(this.btnBatch_Click);
            // 
            // txtBatchNum
            // 
            this.txtBatchNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtBatchNum.Location = new System.Drawing.Point(63, 60);
            this.txtBatchNum.Name = "txtBatchNum";
            this.txtBatchNum.Size = new System.Drawing.Size(51, 20);
            this.txtBatchNum.TabIndex = 8;
            this.txtBatchNum.Text = "100";
            this.txtBatchNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // grpGeneration
            // 
            this.grpGeneration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpGeneration.Controls.Add(this.label1);
            this.grpGeneration.Controls.Add(this.txtSeed);
            this.grpGeneration.Controls.Add(this.btnCreateROM);
            this.grpGeneration.Controls.Add(this.txtBatchNum);
            this.grpGeneration.Controls.Add(this.btnSaveSpoiler);
            this.grpGeneration.Controls.Add(this.btnBulkCreate);
            this.grpGeneration.Location = new System.Drawing.Point(201, 12);
            this.grpGeneration.Name = "grpGeneration";
            this.grpGeneration.Size = new System.Drawing.Size(321, 88);
            this.grpGeneration.TabIndex = 9;
            this.grpGeneration.TabStop = false;
            this.grpGeneration.Text = "Generation";
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.label2);
            this.grpOptions.Controls.Add(this.cmbPreset);
            this.grpOptions.Controls.Add(this.btnCustomize);
            this.grpOptions.Location = new System.Drawing.Point(12, 12);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(183, 88);
            this.grpOptions.TabIndex = 10;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Options";
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
            // cmbPreset
            // 
            this.cmbPreset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPreset.FormattingEnabled = true;
            this.cmbPreset.Items.AddRange(new object[] {
            "Standard",
            "Custom"});
            this.cmbPreset.Location = new System.Drawing.Point(53, 16);
            this.cmbPreset.Name = "cmbPreset";
            this.cmbPreset.Size = new System.Drawing.Size(123, 21);
            this.cmbPreset.TabIndex = 12;
            this.cmbPreset.SelectedIndexChanged += new System.EventHandler(this.cmbPreset_SelectedIndexChanged);
            // 
            // grpLog
            // 
            this.grpLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpLog.Controls.Add(this.txtLog);
            this.grpLog.Location = new System.Drawing.Point(12, 99);
            this.grpLog.Name = "grpLog";
            this.grpLog.Size = new System.Drawing.Size(510, 241);
            this.grpLog.TabIndex = 11;
            this.grpLog.TabStop = false;
            this.grpLog.Text = "Log";
            // 
            // progressBar1
            // 
            this.progressBar1.Enabled = false;
            this.progressBar1.Location = new System.Drawing.Point(47, 343);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(173, 13);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 12;
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnCreateROM;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 361);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.grpLog);
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.grpGeneration);
            this.Controls.Add(this.lblVersion);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(550, 300);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LADX Randomizer";
            this.grpGeneration.ResumeLayout(false);
            this.grpGeneration.PerformLayout();
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.grpLog.ResumeLayout(false);
            this.grpLog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateROM;
        private System.Windows.Forms.TextBox txtSeed;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button btnCustomize;
        private System.Windows.Forms.Button btnSaveSpoiler;
        private System.Windows.Forms.Button btnBulkCreate;
        private System.Windows.Forms.TextBox txtBatchNum;
        private System.Windows.Forms.GroupBox grpGeneration;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.GroupBox grpLog;
        private System.Windows.Forms.ComboBox cmbPreset;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

