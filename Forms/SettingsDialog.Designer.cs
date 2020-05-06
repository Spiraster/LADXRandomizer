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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabWarps = new System.Windows.Forms.TabPage();
            this.grpWarpOptions = new System.Windows.Forms.GroupBox();
            this.chkExcludeEgg = new System.Windows.Forms.CheckBox();
            this.chkExcludeHouse = new System.Windows.Forms.CheckBox();
            this.chkPairWarps = new System.Windows.Forms.CheckBox();
            this.chkShuffleWarps = new System.Windows.Forms.CheckBox();
            this.tabItems = new System.Windows.Forms.TabPage();
            this.grpItemOptions = new System.Windows.Forms.GroupBox();
            this.chkShuffleItems = new System.Windows.Forms.CheckBox();
            this.tabMisc = new System.Windows.Forms.TabPage();
            this.grpMiscScreenEdits = new System.Windows.Forms.GroupBox();
            this.chkRemoveOwls = new System.Windows.Forms.CheckBox();
            this.chkPreventWaterSoftlocks = new System.Windows.Forms.CheckBox();
            this.chkRemoveHouseMarin = new System.Windows.Forms.CheckBox();
            this.chkCoverD7Pit = new System.Windows.Forms.CheckBox();
            this.grpMiscPatches = new System.Windows.Forms.GroupBox();
            this.chkPatchD0Entrance = new System.Windows.Forms.CheckBox();
            this.chkPatchTrendy = new System.Windows.Forms.CheckBox();
            this.chkPatchWaterfalls = new System.Windows.Forms.CheckBox();
            this.chkDisableLanmolasPit = new System.Windows.Forms.CheckBox();
            this.chkDisableBirdKeyPits = new System.Windows.Forms.CheckBox();
            this.chkPatchEggMaze = new System.Windows.Forms.CheckBox();
            this.chkPatchSlimeKey = new System.Windows.Forms.CheckBox();
            this.chkPatchWarpSaving = new System.Windows.Forms.CheckBox();
            this.chkDisableBowwowKids = new System.Windows.Forms.CheckBox();
            this.chkPatchSignpostMaze = new System.Windows.Forms.CheckBox();
            this.chkPatchGhost = new System.Windows.Forms.CheckBox();
            this.chkHouseWarp = new System.Windows.Forms.CheckBox();
            this.lblMask = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.chkDebugMode = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabWarps.SuspendLayout();
            this.grpWarpOptions.SuspendLayout();
            this.tabItems.SuspendLayout();
            this.tabMisc.SuspendLayout();
            this.grpMiscScreenEdits.SuspendLayout();
            this.grpMiscPatches.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(147, 482);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(228, 482);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabWarps);
            this.tabControl1.Controls.Add(this.tabItems);
            this.tabControl1.Controls.Add(this.tabMisc);
            this.tabControl1.Location = new System.Drawing.Point(5, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(301, 445);
            this.tabControl1.TabIndex = 7;
            // 
            // tabWarps
            // 
            this.tabWarps.Controls.Add(this.grpWarpOptions);
            this.tabWarps.Controls.Add(this.chkShuffleWarps);
            this.tabWarps.Location = new System.Drawing.Point(4, 22);
            this.tabWarps.Name = "tabWarps";
            this.tabWarps.Padding = new System.Windows.Forms.Padding(3);
            this.tabWarps.Size = new System.Drawing.Size(293, 419);
            this.tabWarps.TabIndex = 0;
            this.tabWarps.Text = "Warps";
            this.tabWarps.UseVisualStyleBackColor = true;
            // 
            // grpWarpOptions
            // 
            this.grpWarpOptions.Controls.Add(this.chkExcludeEgg);
            this.grpWarpOptions.Controls.Add(this.chkExcludeHouse);
            this.grpWarpOptions.Controls.Add(this.chkPairWarps);
            this.grpWarpOptions.Enabled = false;
            this.grpWarpOptions.Location = new System.Drawing.Point(3, 23);
            this.grpWarpOptions.Name = "grpWarpOptions";
            this.grpWarpOptions.Size = new System.Drawing.Size(285, 92);
            this.grpWarpOptions.TabIndex = 1;
            this.grpWarpOptions.TabStop = false;
            this.grpWarpOptions.Text = "Options";
            this.grpWarpOptions.EnabledChanged += new System.EventHandler(this.groupBox_EnabledChanged);
            // 
            // chkExcludeEgg
            // 
            this.chkExcludeEgg.AutoSize = true;
            this.chkExcludeEgg.Location = new System.Drawing.Point(6, 65);
            this.chkExcludeEgg.Name = "chkExcludeEgg";
            this.chkExcludeEgg.Size = new System.Drawing.Size(160, 17);
            this.chkExcludeEgg.TabIndex = 0;
            this.chkExcludeEgg.Text = "Exclude the egg from shuffle";
            this.chkExcludeEgg.UseVisualStyleBackColor = true;
            this.chkExcludeEgg.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // chkExcludeHouse
            // 
            this.chkExcludeHouse.AutoSize = true;
            this.chkExcludeHouse.Location = new System.Drawing.Point(6, 42);
            this.chkExcludeHouse.Name = "chkExcludeHouse";
            this.chkExcludeHouse.Size = new System.Drawing.Size(190, 17);
            this.chkExcludeHouse.TabIndex = 0;
            this.chkExcludeHouse.Text = "Exclude starting house from shuffle";
            this.chkExcludeHouse.UseVisualStyleBackColor = true;
            this.chkExcludeHouse.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // chkPairWarps
            // 
            this.chkPairWarps.AutoSize = true;
            this.chkPairWarps.Location = new System.Drawing.Point(6, 19);
            this.chkPairWarps.Name = "chkPairWarps";
            this.chkPairWarps.Size = new System.Drawing.Size(88, 17);
            this.chkPairWarps.TabIndex = 0;
            this.chkPairWarps.Text = "Pair all warps";
            this.chkPairWarps.UseVisualStyleBackColor = true;
            this.chkPairWarps.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // chkShuffleWarps
            // 
            this.chkShuffleWarps.AutoSize = true;
            this.chkShuffleWarps.Location = new System.Drawing.Point(6, 6);
            this.chkShuffleWarps.Name = "chkShuffleWarps";
            this.chkShuffleWarps.Size = new System.Drawing.Size(90, 17);
            this.chkShuffleWarps.TabIndex = 0;
            this.chkShuffleWarps.Text = "Shuffle warps";
            this.chkShuffleWarps.UseVisualStyleBackColor = true;
            this.chkShuffleWarps.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // tabItems
            // 
            this.tabItems.Controls.Add(this.grpItemOptions);
            this.tabItems.Controls.Add(this.chkShuffleItems);
            this.tabItems.Location = new System.Drawing.Point(4, 22);
            this.tabItems.Name = "tabItems";
            this.tabItems.Padding = new System.Windows.Forms.Padding(3);
            this.tabItems.Size = new System.Drawing.Size(293, 419);
            this.tabItems.TabIndex = 1;
            this.tabItems.Text = "Items";
            this.tabItems.UseVisualStyleBackColor = true;
            // 
            // grpItemOptions
            // 
            this.grpItemOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpItemOptions.Enabled = false;
            this.grpItemOptions.Location = new System.Drawing.Point(3, 23);
            this.grpItemOptions.Name = "grpItemOptions";
            this.grpItemOptions.Size = new System.Drawing.Size(285, 95);
            this.grpItemOptions.TabIndex = 1;
            this.grpItemOptions.TabStop = false;
            this.grpItemOptions.Text = "Options";
            this.grpItemOptions.EnabledChanged += new System.EventHandler(this.groupBox_EnabledChanged);
            // 
            // chkShuffleItems
            // 
            this.chkShuffleItems.AutoSize = true;
            this.chkShuffleItems.Enabled = false;
            this.chkShuffleItems.Location = new System.Drawing.Point(6, 6);
            this.chkShuffleItems.Name = "chkShuffleItems";
            this.chkShuffleItems.Size = new System.Drawing.Size(86, 17);
            this.chkShuffleItems.TabIndex = 0;
            this.chkShuffleItems.Text = "Shuffle items";
            this.chkShuffleItems.UseVisualStyleBackColor = true;
            this.chkShuffleItems.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // tabMisc
            // 
            this.tabMisc.AutoScroll = true;
            this.tabMisc.Controls.Add(this.grpMiscScreenEdits);
            this.tabMisc.Controls.Add(this.grpMiscPatches);
            this.tabMisc.Location = new System.Drawing.Point(4, 22);
            this.tabMisc.Name = "tabMisc";
            this.tabMisc.Size = new System.Drawing.Size(293, 419);
            this.tabMisc.TabIndex = 2;
            this.tabMisc.Text = "Misc.";
            this.tabMisc.UseVisualStyleBackColor = true;
            // 
            // grpMiscScreenEdits
            // 
            this.grpMiscScreenEdits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMiscScreenEdits.Controls.Add(this.chkRemoveOwls);
            this.grpMiscScreenEdits.Controls.Add(this.chkPreventWaterSoftlocks);
            this.grpMiscScreenEdits.Controls.Add(this.chkRemoveHouseMarin);
            this.grpMiscScreenEdits.Controls.Add(this.chkCoverD7Pit);
            this.grpMiscScreenEdits.Location = new System.Drawing.Point(3, 304);
            this.grpMiscScreenEdits.Name = "grpMiscScreenEdits";
            this.grpMiscScreenEdits.Size = new System.Drawing.Size(285, 111);
            this.grpMiscScreenEdits.TabIndex = 1;
            this.grpMiscScreenEdits.TabStop = false;
            this.grpMiscScreenEdits.Text = "Screen Edits";
            // 
            // chkRemoveOwls
            // 
            this.chkRemoveOwls.AutoSize = true;
            this.chkRemoveOwls.Location = new System.Drawing.Point(6, 88);
            this.chkRemoveOwls.Name = "chkRemoveOwls";
            this.chkRemoveOwls.Size = new System.Drawing.Size(103, 17);
            this.chkRemoveOwls.TabIndex = 0;
            this.chkRemoveOwls.Text = "Remove all owls";
            this.chkRemoveOwls.UseVisualStyleBackColor = true;
            this.chkRemoveOwls.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // chkPreventWaterSoftlocks
            // 
            this.chkPreventWaterSoftlocks.AutoSize = true;
            this.chkPreventWaterSoftlocks.Location = new System.Drawing.Point(6, 42);
            this.chkPreventWaterSoftlocks.Name = "chkPreventWaterSoftlocks";
            this.chkPreventWaterSoftlocks.Size = new System.Drawing.Size(157, 17);
            this.chkPreventWaterSoftlocks.TabIndex = 0;
            this.chkPreventWaterSoftlocks.Text = "Prevent flipperless softlocks";
            this.chkPreventWaterSoftlocks.UseVisualStyleBackColor = true;
            this.chkPreventWaterSoftlocks.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // chkRemoveHouseMarin
            // 
            this.chkRemoveHouseMarin.AutoSize = true;
            this.chkRemoveHouseMarin.Location = new System.Drawing.Point(6, 65);
            this.chkRemoveHouseMarin.Name = "chkRemoveHouseMarin";
            this.chkRemoveHouseMarin.Size = new System.Drawing.Size(154, 17);
            this.chkRemoveHouseMarin.TabIndex = 0;
            this.chkRemoveHouseMarin.Text = "Remove opening cutscene";
            this.chkRemoveHouseMarin.UseVisualStyleBackColor = true;
            this.chkRemoveHouseMarin.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // chkCoverD7Pit
            // 
            this.chkCoverD7Pit.AutoSize = true;
            this.chkCoverD7Pit.Location = new System.Drawing.Point(6, 19);
            this.chkCoverD7Pit.Name = "chkCoverD7Pit";
            this.chkCoverD7Pit.Size = new System.Drawing.Size(116, 17);
            this.chkCoverD7Pit.TabIndex = 0;
            this.chkCoverD7Pit.Text = "Cover pit below D7";
            this.chkCoverD7Pit.UseVisualStyleBackColor = true;
            this.chkCoverD7Pit.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // grpMiscPatches
            // 
            this.grpMiscPatches.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMiscPatches.Controls.Add(this.chkPatchD0Entrance);
            this.grpMiscPatches.Controls.Add(this.chkPatchTrendy);
            this.grpMiscPatches.Controls.Add(this.chkPatchWaterfalls);
            this.grpMiscPatches.Controls.Add(this.chkDisableLanmolasPit);
            this.grpMiscPatches.Controls.Add(this.chkDisableBirdKeyPits);
            this.grpMiscPatches.Controls.Add(this.chkPatchEggMaze);
            this.grpMiscPatches.Controls.Add(this.chkPatchSlimeKey);
            this.grpMiscPatches.Controls.Add(this.chkPatchWarpSaving);
            this.grpMiscPatches.Controls.Add(this.chkDisableBowwowKids);
            this.grpMiscPatches.Controls.Add(this.chkPatchSignpostMaze);
            this.grpMiscPatches.Controls.Add(this.chkPatchGhost);
            this.grpMiscPatches.Controls.Add(this.chkHouseWarp);
            this.grpMiscPatches.Location = new System.Drawing.Point(3, 3);
            this.grpMiscPatches.Name = "grpMiscPatches";
            this.grpMiscPatches.Size = new System.Drawing.Size(285, 295);
            this.grpMiscPatches.TabIndex = 0;
            this.grpMiscPatches.TabStop = false;
            this.grpMiscPatches.Text = "Patches";
            // 
            // chkPatchD0Entrance
            // 
            this.chkPatchD0Entrance.AutoSize = true;
            this.chkPatchD0Entrance.Location = new System.Drawing.Point(6, 134);
            this.chkPatchD0Entrance.Name = "chkPatchD0Entrance";
            this.chkPatchD0Entrance.Size = new System.Drawing.Size(232, 17);
            this.chkPatchD0Entrance.TabIndex = 1;
            this.chkPatchD0Entrance.Text = "Allow access to D0 entrance with a follower";
            this.chkPatchD0Entrance.UseVisualStyleBackColor = true;
            // 
            // chkPatchTrendy
            // 
            this.chkPatchTrendy.AutoSize = true;
            this.chkPatchTrendy.Location = new System.Drawing.Point(6, 203);
            this.chkPatchTrendy.Name = "chkPatchTrendy";
            this.chkPatchTrendy.Size = new System.Drawing.Size(275, 17);
            this.chkPatchTrendy.TabIndex = 1;
            this.chkPatchTrendy.Text = "Allow acquiring Trendy Game powder with mushroom";
            this.chkPatchTrendy.UseVisualStyleBackColor = true;
            // 
            // chkPatchWaterfalls
            // 
            this.chkPatchWaterfalls.AutoSize = true;
            this.chkPatchWaterfalls.Location = new System.Drawing.Point(6, 180);
            this.chkPatchWaterfalls.Name = "chkPatchWaterfalls";
            this.chkPatchWaterfalls.Size = new System.Drawing.Size(271, 17);
            this.chkPatchWaterfalls.TabIndex = 1;
            this.chkPatchWaterfalls.Text = "Allow jumping off waterfalls without flippers (incl. raft)";
            this.chkPatchWaterfalls.UseVisualStyleBackColor = true;
            // 
            // chkDisableLanmolasPit
            // 
            this.chkDisableLanmolasPit.AutoSize = true;
            this.chkDisableLanmolasPit.Location = new System.Drawing.Point(6, 272);
            this.chkDisableLanmolasPit.Name = "chkDisableLanmolasPit";
            this.chkDisableLanmolasPit.Size = new System.Drawing.Size(149, 17);
            this.chkDisableLanmolasPit.TabIndex = 0;
            this.chkDisableLanmolasPit.Text = "Disable Lanmolas pit warp";
            this.chkDisableLanmolasPit.UseVisualStyleBackColor = true;
            this.chkDisableLanmolasPit.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // chkDisableBirdKeyPits
            // 
            this.chkDisableBirdKeyPits.AutoSize = true;
            this.chkDisableBirdKeyPits.Location = new System.Drawing.Point(6, 249);
            this.chkDisableBirdKeyPits.Name = "chkDisableBirdKeyPits";
            this.chkDisableBirdKeyPits.Size = new System.Drawing.Size(184, 17);
            this.chkDisableBirdKeyPits.TabIndex = 0;
            this.chkDisableBirdKeyPits.Text = "Disable pit warps in bird key cave";
            this.chkDisableBirdKeyPits.UseVisualStyleBackColor = true;
            this.chkDisableBirdKeyPits.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // chkPatchEggMaze
            // 
            this.chkPatchEggMaze.AutoSize = true;
            this.chkPatchEggMaze.Location = new System.Drawing.Point(6, 88);
            this.chkPatchEggMaze.Name = "chkPatchEggMaze";
            this.chkPatchEggMaze.Size = new System.Drawing.Size(271, 17);
            this.chkPatchEggMaze.TabIndex = 0;
            this.chkPatchEggMaze.Text = "Prevent the egg maze from having a default solution";
            this.chkPatchEggMaze.UseVisualStyleBackColor = true;
            this.chkPatchEggMaze.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // chkPatchSlimeKey
            // 
            this.chkPatchSlimeKey.AutoSize = true;
            this.chkPatchSlimeKey.Location = new System.Drawing.Point(6, 157);
            this.chkPatchSlimeKey.Name = "chkPatchSlimeKey";
            this.chkPatchSlimeKey.Size = new System.Drawing.Size(236, 17);
            this.chkPatchSlimeKey.TabIndex = 0;
            this.chkPatchSlimeKey.Text = "Prevent slime key softlock following villa skip";
            this.chkPatchSlimeKey.UseVisualStyleBackColor = true;
            this.chkPatchSlimeKey.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // chkPatchWarpSaving
            // 
            this.chkPatchWarpSaving.AutoSize = true;
            this.chkPatchWarpSaving.Location = new System.Drawing.Point(6, 42);
            this.chkPatchWarpSaving.Name = "chkPatchWarpSaving";
            this.chkPatchWarpSaving.Size = new System.Drawing.Size(222, 17);
            this.chkPatchWarpSaving.TabIndex = 0;
            this.chkPatchWarpSaving.Text = "Save D6 and D8 back exit warps on S+Q";
            this.chkPatchWarpSaving.UseVisualStyleBackColor = true;
            this.chkPatchWarpSaving.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // chkDisableBowwowKids
            // 
            this.chkDisableBowwowKids.AutoSize = true;
            this.chkDisableBowwowKids.Location = new System.Drawing.Point(6, 226);
            this.chkDisableBowwowKids.Name = "chkDisableBowwowKids";
            this.chkDisableBowwowKids.Size = new System.Drawing.Size(239, 17);
            this.chkDisableBowwowKids.TabIndex = 0;
            this.chkDisableBowwowKids.Text = "Disable kids conversation after D1 instrument";
            this.chkDisableBowwowKids.UseVisualStyleBackColor = true;
            this.chkDisableBowwowKids.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // chkPatchSignpostMaze
            // 
            this.chkPatchSignpostMaze.AutoSize = true;
            this.chkPatchSignpostMaze.Location = new System.Drawing.Point(6, 65);
            this.chkPatchSignpostMaze.Name = "chkPatchSignpostMaze";
            this.chkPatchSignpostMaze.Size = new System.Drawing.Size(266, 17);
            this.chkPatchSignpostMaze.TabIndex = 0;
            this.chkPatchSignpostMaze.Text = "Prevent signpost maze from becoming inaccessible";
            this.chkPatchSignpostMaze.UseVisualStyleBackColor = true;
            this.chkPatchSignpostMaze.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // chkPatchGhost
            // 
            this.chkPatchGhost.AutoSize = true;
            this.chkPatchGhost.Location = new System.Drawing.Point(6, 111);
            this.chkPatchGhost.Name = "chkPatchGhost";
            this.chkPatchGhost.Size = new System.Drawing.Size(264, 17);
            this.chkPatchGhost.TabIndex = 0;
            this.chkPatchGhost.Text = "Allow access to dungeon entrances with the ghost";
            this.chkPatchGhost.UseVisualStyleBackColor = true;
            this.chkPatchGhost.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // chkHouseWarp
            // 
            this.chkHouseWarp.AutoSize = true;
            this.chkHouseWarp.Location = new System.Drawing.Point(6, 19);
            this.chkHouseWarp.Name = "chkHouseWarp";
            this.chkHouseWarp.Size = new System.Drawing.Size(117, 17);
            this.chkHouseWarp.TabIndex = 0;
            this.chkHouseWarp.Text = "Enable house warp";
            this.chkHouseWarp.UseVisualStyleBackColor = true;
            this.chkHouseWarp.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // lblMask
            // 
            this.lblMask.AutoSize = true;
            this.lblMask.Location = new System.Drawing.Point(10, 9);
            this.lblMask.Name = "lblMask";
            this.lblMask.Size = new System.Drawing.Size(37, 13);
            this.lblMask.TabIndex = 8;
            this.lblMask.Text = "Value:";
            // 
            // txtValue
            // 
            this.txtValue.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValue.Location = new System.Drawing.Point(52, 5);
            this.txtValue.Name = "txtValue";
            this.txtValue.ReadOnly = true;
            this.txtValue.Size = new System.Drawing.Size(40, 23);
            this.txtValue.TabIndex = 9;
            this.txtValue.Text = "0";
            this.txtValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImport.Location = new System.Drawing.Point(6, 483);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 10;
            this.btnImport.Text = "Import...";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // chkDebugMode
            // 
            this.chkDebugMode.AutoSize = true;
            this.chkDebugMode.Enabled = false;
            this.chkDebugMode.Location = new System.Drawing.Point(99, 9);
            this.chkDebugMode.Name = "chkDebugMode";
            this.chkDebugMode.Size = new System.Drawing.Size(87, 17);
            this.chkDebugMode.TabIndex = 11;
            this.chkDebugMode.Text = "Debug mode";
            this.chkDebugMode.UseVisualStyleBackColor = true;
            this.chkDebugMode.Visible = false;
            this.chkDebugMode.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 511);
            this.ControlBox = false;
            this.Controls.Add(this.chkDebugMode);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.lblMask);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.tabControl1.ResumeLayout(false);
            this.tabWarps.ResumeLayout(false);
            this.tabWarps.PerformLayout();
            this.grpWarpOptions.ResumeLayout(false);
            this.grpWarpOptions.PerformLayout();
            this.tabItems.ResumeLayout(false);
            this.tabItems.PerformLayout();
            this.tabMisc.ResumeLayout(false);
            this.grpMiscScreenEdits.ResumeLayout(false);
            this.grpMiscScreenEdits.PerformLayout();
            this.grpMiscPatches.ResumeLayout(false);
            this.grpMiscPatches.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabWarps;
        private System.Windows.Forms.GroupBox grpWarpOptions;
        private System.Windows.Forms.CheckBox chkShuffleWarps;
        private System.Windows.Forms.TabPage tabItems;
        private System.Windows.Forms.GroupBox grpItemOptions;
        private System.Windows.Forms.CheckBox chkShuffleItems;
        private System.Windows.Forms.TabPage tabMisc;
        private System.Windows.Forms.Label lblMask;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.CheckBox chkPairWarps;
        private System.Windows.Forms.CheckBox chkExcludeEgg;
        private System.Windows.Forms.CheckBox chkExcludeHouse;
        private System.Windows.Forms.GroupBox grpMiscPatches;
        private System.Windows.Forms.CheckBox chkDisableLanmolasPit;
        private System.Windows.Forms.CheckBox chkDisableBirdKeyPits;
        private System.Windows.Forms.CheckBox chkPatchEggMaze;
        private System.Windows.Forms.CheckBox chkPatchSlimeKey;
        private System.Windows.Forms.CheckBox chkPatchWarpSaving;
        private System.Windows.Forms.CheckBox chkDisableBowwowKids;
        private System.Windows.Forms.CheckBox chkPatchSignpostMaze;
        private System.Windows.Forms.CheckBox chkPatchGhost;
        private System.Windows.Forms.CheckBox chkHouseWarp;
        private System.Windows.Forms.GroupBox grpMiscScreenEdits;
        private System.Windows.Forms.CheckBox chkRemoveOwls;
        private System.Windows.Forms.CheckBox chkPreventWaterSoftlocks;
        private System.Windows.Forms.CheckBox chkRemoveHouseMarin;
        private System.Windows.Forms.CheckBox chkCoverD7Pit;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.CheckBox chkDebugMode;
        private System.Windows.Forms.CheckBox chkPatchTrendy;
        private System.Windows.Forms.CheckBox chkPatchWaterfalls;
        private System.Windows.Forms.CheckBox chkPatchD0Entrance;
    }
}