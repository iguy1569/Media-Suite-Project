namespace DirectoryCompare
{
    partial class DirectoryCompare
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirectoryCompare));
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.PB_CurrentTransfer = new System.Windows.Forms.ProgressBar();
            this.MessageTimer = new System.Windows.Forms.Timer(this.components);
            this.BTN_Cancel = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.TC_Destination = new System.Windows.Forms.TabControl();
            this.TP_Source = new System.Windows.Forms.TabPage();
            this.BTN_LoadSrc = new System.Windows.Forms.Button();
            this.BTN_SaveSrc = new System.Windows.Forms.Button();
            this.LBL_SFilterMessage = new System.Windows.Forms.Label();
            this.TB_SFilters = new System.Windows.Forms.TextBox();
            this.TV_Source = new System.Windows.Forms.TreeView();
            this.BTN_GetSource = new System.Windows.Forms.Button();
            this.TP_Destination = new System.Windows.Forms.TabPage();
            this.BTN_LoadDst = new System.Windows.Forms.Button();
            this.BTN_SaveDst = new System.Windows.Forms.Button();
            this.LBL_DFilterMessage = new System.Windows.Forms.Label();
            this.TB_DFilters = new System.Windows.Forms.TextBox();
            this.TV_Destination = new System.Windows.Forms.TreeView();
            this.BTN_GetDestination = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TP_SourceDestDiff = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.TV_SourceDiff = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.TV_DestinationDiff = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.TP_Combined = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TV_CombinedResults = new System.Windows.Forms.TreeView();
            this.BTN_PrintDiffDest = new System.Windows.Forms.Button();
            this.BTN_PrintDiffSource = new System.Windows.Forms.Button();
            this.LBL_Message = new System.Windows.Forms.Label();
            this.BTN_DestToSource = new System.Windows.Forms.Button();
            this.BTN_TransferSourceDest = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.TC_Destination.SuspendLayout();
            this.TP_Source.SuspendLayout();
            this.TP_Destination.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.TP_SourceDestDiff.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.TP_Combined.SuspendLayout();
            this.SuspendLayout();
            // 
            // PB_CurrentTransfer
            // 
            this.PB_CurrentTransfer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PB_CurrentTransfer.Location = new System.Drawing.Point(13, 481);
            this.PB_CurrentTransfer.Name = "PB_CurrentTransfer";
            this.PB_CurrentTransfer.Size = new System.Drawing.Size(599, 23);
            this.PB_CurrentTransfer.TabIndex = 14;
            // 
            // MessageTimer
            // 
            this.MessageTimer.Interval = 4000;
            this.MessageTimer.Tick += new System.EventHandler(this.MessageTimer_Tick);
            // 
            // BTN_Cancel
            // 
            this.BTN_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_Cancel.Enabled = false;
            this.BTN_Cancel.Location = new System.Drawing.Point(618, 481);
            this.BTN_Cancel.Name = "BTN_Cancel";
            this.BTN_Cancel.Size = new System.Drawing.Size(75, 23);
            this.BTN_Cancel.TabIndex = 15;
            this.BTN_Cancel.Text = "Cancel";
            this.BTN_Cancel.UseVisualStyleBackColor = true;
            this.BTN_Cancel.Click += new System.EventHandler(this.BTN_Cancel_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.splitContainer2.Location = new System.Drawing.Point(13, 14);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel1.Controls.Add(this.TC_Destination);
            this.splitContainer2.Panel1MinSize = 254;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Panel2.Controls.Add(this.BTN_PrintDiffDest);
            this.splitContainer2.Panel2.Controls.Add(this.BTN_PrintDiffSource);
            this.splitContainer2.Panel2.Controls.Add(this.LBL_Message);
            this.splitContainer2.Panel2.Controls.Add(this.BTN_DestToSource);
            this.splitContainer2.Panel2.Controls.Add(this.BTN_TransferSourceDest);
            this.splitContainer2.Size = new System.Drawing.Size(690, 461);
            this.splitContainer2.SplitterDistance = 254;
            this.splitContainer2.TabIndex = 16;
            // 
            // TC_Destination
            // 
            this.TC_Destination.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TC_Destination.Controls.Add(this.TP_Source);
            this.TC_Destination.Controls.Add(this.TP_Destination);
            this.TC_Destination.Location = new System.Drawing.Point(3, 9);
            this.TC_Destination.Name = "TC_Destination";
            this.TC_Destination.SelectedIndex = 0;
            this.TC_Destination.Size = new System.Drawing.Size(248, 443);
            this.TC_Destination.TabIndex = 3;
            // 
            // TP_Source
            // 
            this.TP_Source.Controls.Add(this.BTN_LoadSrc);
            this.TP_Source.Controls.Add(this.BTN_SaveSrc);
            this.TP_Source.Controls.Add(this.LBL_SFilterMessage);
            this.TP_Source.Controls.Add(this.TB_SFilters);
            this.TP_Source.Controls.Add(this.TV_Source);
            this.TP_Source.Controls.Add(this.BTN_GetSource);
            this.TP_Source.Location = new System.Drawing.Point(4, 22);
            this.TP_Source.Name = "TP_Source";
            this.TP_Source.Padding = new System.Windows.Forms.Padding(3);
            this.TP_Source.Size = new System.Drawing.Size(240, 417);
            this.TP_Source.TabIndex = 0;
            this.TP_Source.Text = "Source Directory";
            this.TP_Source.UseVisualStyleBackColor = true;
            // 
            // BTN_LoadSrc
            // 
            this.BTN_LoadSrc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_LoadSrc.Location = new System.Drawing.Point(124, 36);
            this.BTN_LoadSrc.Name = "BTN_LoadSrc";
            this.BTN_LoadSrc.Size = new System.Drawing.Size(110, 23);
            this.BTN_LoadSrc.TabIndex = 9;
            this.BTN_LoadSrc.Text = "Load Info";
            this.BTN_LoadSrc.UseVisualStyleBackColor = true;
            this.BTN_LoadSrc.Click += new System.EventHandler(this.BTN_LoadSrc_Click);
            // 
            // BTN_SaveSrc
            // 
            this.BTN_SaveSrc.Enabled = false;
            this.BTN_SaveSrc.Location = new System.Drawing.Point(7, 36);
            this.BTN_SaveSrc.Name = "BTN_SaveSrc";
            this.BTN_SaveSrc.Size = new System.Drawing.Size(110, 23);
            this.BTN_SaveSrc.TabIndex = 8;
            this.BTN_SaveSrc.Text = "Save Info";
            this.BTN_SaveSrc.UseVisualStyleBackColor = true;
            this.BTN_SaveSrc.Click += new System.EventHandler(this.BTN_SaveSrc_Click);
            // 
            // LBL_SFilterMessage
            // 
            this.LBL_SFilterMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LBL_SFilterMessage.Location = new System.Drawing.Point(145, 65);
            this.LBL_SFilterMessage.Name = "LBL_SFilterMessage";
            this.LBL_SFilterMessage.Size = new System.Drawing.Size(88, 20);
            this.LBL_SFilterMessage.TabIndex = 7;
            this.LBL_SFilterMessage.Text = "Set Filters";
            this.LBL_SFilterMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TB_SFilters
            // 
            this.TB_SFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_SFilters.Location = new System.Drawing.Point(7, 65);
            this.TB_SFilters.Name = "TB_SFilters";
            this.TB_SFilters.Size = new System.Drawing.Size(132, 20);
            this.TB_SFilters.TabIndex = 6;
            // 
            // TV_Source
            // 
            this.TV_Source.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TV_Source.Location = new System.Drawing.Point(7, 88);
            this.TV_Source.Name = "TV_Source";
            this.TV_Source.Size = new System.Drawing.Size(227, 323);
            this.TV_Source.TabIndex = 1;
            // 
            // BTN_GetSource
            // 
            this.BTN_GetSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_GetSource.Location = new System.Drawing.Point(7, 7);
            this.BTN_GetSource.Name = "BTN_GetSource";
            this.BTN_GetSource.Size = new System.Drawing.Size(227, 23);
            this.BTN_GetSource.TabIndex = 0;
            this.BTN_GetSource.Text = "Get Source Directory";
            this.BTN_GetSource.UseVisualStyleBackColor = true;
            this.BTN_GetSource.Click += new System.EventHandler(this.BTN_GetSource_Click);
            // 
            // TP_Destination
            // 
            this.TP_Destination.Controls.Add(this.BTN_LoadDst);
            this.TP_Destination.Controls.Add(this.BTN_SaveDst);
            this.TP_Destination.Controls.Add(this.LBL_DFilterMessage);
            this.TP_Destination.Controls.Add(this.TB_DFilters);
            this.TP_Destination.Controls.Add(this.TV_Destination);
            this.TP_Destination.Controls.Add(this.BTN_GetDestination);
            this.TP_Destination.Location = new System.Drawing.Point(4, 22);
            this.TP_Destination.Name = "TP_Destination";
            this.TP_Destination.Padding = new System.Windows.Forms.Padding(3);
            this.TP_Destination.Size = new System.Drawing.Size(240, 417);
            this.TP_Destination.TabIndex = 1;
            this.TP_Destination.Text = "Destination Directory";
            this.TP_Destination.UseVisualStyleBackColor = true;
            // 
            // BTN_LoadDst
            // 
            this.BTN_LoadDst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_LoadDst.Location = new System.Drawing.Point(124, 36);
            this.BTN_LoadDst.Name = "BTN_LoadDst";
            this.BTN_LoadDst.Size = new System.Drawing.Size(110, 23);
            this.BTN_LoadDst.TabIndex = 11;
            this.BTN_LoadDst.Text = "Load Info";
            this.BTN_LoadDst.UseVisualStyleBackColor = true;
            this.BTN_LoadDst.Click += new System.EventHandler(this.BTN_LoadDst_Click);
            // 
            // BTN_SaveDst
            // 
            this.BTN_SaveDst.Enabled = false;
            this.BTN_SaveDst.Location = new System.Drawing.Point(6, 36);
            this.BTN_SaveDst.Name = "BTN_SaveDst";
            this.BTN_SaveDst.Size = new System.Drawing.Size(110, 23);
            this.BTN_SaveDst.TabIndex = 10;
            this.BTN_SaveDst.Text = "Save Info";
            this.BTN_SaveDst.UseVisualStyleBackColor = true;
            this.BTN_SaveDst.Click += new System.EventHandler(this.BTN_SaveDst_Click);
            // 
            // LBL_DFilterMessage
            // 
            this.LBL_DFilterMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LBL_DFilterMessage.Location = new System.Drawing.Point(144, 65);
            this.LBL_DFilterMessage.Name = "LBL_DFilterMessage";
            this.LBL_DFilterMessage.Size = new System.Drawing.Size(88, 20);
            this.LBL_DFilterMessage.TabIndex = 5;
            this.LBL_DFilterMessage.Text = "Set Filters";
            this.LBL_DFilterMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TB_DFilters
            // 
            this.TB_DFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_DFilters.Location = new System.Drawing.Point(6, 65);
            this.TB_DFilters.Name = "TB_DFilters";
            this.TB_DFilters.Size = new System.Drawing.Size(132, 20);
            this.TB_DFilters.TabIndex = 4;
            // 
            // TV_Destination
            // 
            this.TV_Destination.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TV_Destination.Location = new System.Drawing.Point(6, 88);
            this.TV_Destination.Name = "TV_Destination";
            this.TV_Destination.Size = new System.Drawing.Size(227, 323);
            this.TV_Destination.TabIndex = 3;
            // 
            // BTN_GetDestination
            // 
            this.BTN_GetDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_GetDestination.Location = new System.Drawing.Point(6, 7);
            this.BTN_GetDestination.Name = "BTN_GetDestination";
            this.BTN_GetDestination.Size = new System.Drawing.Size(227, 23);
            this.BTN_GetDestination.TabIndex = 2;
            this.BTN_GetDestination.Text = "Get Destination Directory";
            this.BTN_GetDestination.UseVisualStyleBackColor = true;
            this.BTN_GetDestination.Click += new System.EventHandler(this.BTN_GetDestination_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.TP_SourceDestDiff);
            this.tabControl1.Controls.Add(this.TP_Combined);
            this.tabControl1.Location = new System.Drawing.Point(9, 9);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(413, 359);
            this.tabControl1.TabIndex = 20;
            // 
            // TP_SourceDestDiff
            // 
            this.TP_SourceDestDiff.BackColor = System.Drawing.Color.Transparent;
            this.TP_SourceDestDiff.Controls.Add(this.splitContainer1);
            this.TP_SourceDestDiff.Location = new System.Drawing.Point(4, 22);
            this.TP_SourceDestDiff.Name = "TP_SourceDestDiff";
            this.TP_SourceDestDiff.Padding = new System.Windows.Forms.Padding(3);
            this.TP_SourceDestDiff.Size = new System.Drawing.Size(405, 333);
            this.TP_SourceDestDiff.TabIndex = 0;
            this.TP_SourceDestDiff.Text = "Source Destination Differences";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.Black;
            this.splitContainer1.Location = new System.Drawing.Point(6, 7);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.TV_SourceDiff);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this.TV_DestinationDiff);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(393, 320);
            this.splitContainer1.SplitterDistance = 163;
            this.splitContainer1.TabIndex = 18;
            // 
            // TV_SourceDiff
            // 
            this.TV_SourceDiff.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TV_SourceDiff.CheckBoxes = true;
            this.TV_SourceDiff.Location = new System.Drawing.Point(0, 26);
            this.TV_SourceDiff.Name = "TV_SourceDiff";
            this.TV_SourceDiff.Size = new System.Drawing.Size(390, 134);
            this.TV_SourceDiff.TabIndex = 5;
            this.TV_SourceDiff.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.TV_SourceDiff_BeforeCheck);
            this.TV_SourceDiff.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TV_SourceDiff_AfterCheck);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(390, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Source Items Missing From Destination Directory";
            // 
            // TV_DestinationDiff
            // 
            this.TV_DestinationDiff.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TV_DestinationDiff.CheckBoxes = true;
            this.TV_DestinationDiff.Location = new System.Drawing.Point(2, 28);
            this.TV_DestinationDiff.Name = "TV_DestinationDiff";
            this.TV_DestinationDiff.Size = new System.Drawing.Size(388, 124);
            this.TV_DestinationDiff.TabIndex = 6;
            this.TV_DestinationDiff.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.TV_DestinationDiff_BeforeCheck);
            this.TV_DestinationDiff.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TV_DestinationDiff_AfterCheck);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(388, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Destination Items Missing From Source Directory";
            // 
            // TP_Combined
            // 
            this.TP_Combined.Controls.Add(this.label5);
            this.TP_Combined.Controls.Add(this.label4);
            this.TP_Combined.Controls.Add(this.label3);
            this.TP_Combined.Controls.Add(this.TV_CombinedResults);
            this.TP_Combined.Location = new System.Drawing.Point(4, 22);
            this.TP_Combined.Name = "TP_Combined";
            this.TP_Combined.Padding = new System.Windows.Forms.Padding(3);
            this.TP_Combined.Size = new System.Drawing.Size(405, 333);
            this.TP_Combined.TabIndex = 1;
            this.TP_Combined.Text = "All Content Combined";
            this.TP_Combined.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(106, 314);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Matches";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(160, 314);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Destination Difference";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Green;
            this.label3.Location = new System.Drawing.Point(7, 314);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Source Difference";
            // 
            // TV_CombinedResults
            // 
            this.TV_CombinedResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TV_CombinedResults.Location = new System.Drawing.Point(3, 3);
            this.TV_CombinedResults.Name = "TV_CombinedResults";
            this.TV_CombinedResults.Size = new System.Drawing.Size(399, 306);
            this.TV_CombinedResults.TabIndex = 0;
            // 
            // BTN_PrintDiffDest
            // 
            this.BTN_PrintDiffDest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_PrintDiffDest.Enabled = false;
            this.BTN_PrintDiffDest.Location = new System.Drawing.Point(225, 374);
            this.BTN_PrintDiffDest.Name = "BTN_PrintDiffDest";
            this.BTN_PrintDiffDest.Size = new System.Drawing.Size(197, 23);
            this.BTN_PrintDiffDest.TabIndex = 19;
            this.BTN_PrintDiffDest.Text = "Print Destination Differences";
            this.BTN_PrintDiffDest.UseVisualStyleBackColor = true;
            this.BTN_PrintDiffDest.Click += new System.EventHandler(this.BTN_PrintDiffDest_Click);
            // 
            // BTN_PrintDiffSource
            // 
            this.BTN_PrintDiffSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BTN_PrintDiffSource.Enabled = false;
            this.BTN_PrintDiffSource.Location = new System.Drawing.Point(9, 374);
            this.BTN_PrintDiffSource.Name = "BTN_PrintDiffSource";
            this.BTN_PrintDiffSource.Size = new System.Drawing.Size(191, 23);
            this.BTN_PrintDiffSource.TabIndex = 18;
            this.BTN_PrintDiffSource.Text = "Print Source Differences";
            this.BTN_PrintDiffSource.UseVisualStyleBackColor = true;
            this.BTN_PrintDiffSource.Click += new System.EventHandler(this.BTN_PrintDiffSource_Click);
            // 
            // LBL_Message
            // 
            this.LBL_Message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LBL_Message.BackColor = System.Drawing.Color.White;
            this.LBL_Message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LBL_Message.Location = new System.Drawing.Point(9, 429);
            this.LBL_Message.Name = "LBL_Message";
            this.LBL_Message.Size = new System.Drawing.Size(413, 23);
            this.LBL_Message.TabIndex = 16;
            this.LBL_Message.Text = "Select a option";
            this.LBL_Message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BTN_DestToSource
            // 
            this.BTN_DestToSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_DestToSource.Enabled = false;
            this.BTN_DestToSource.Location = new System.Drawing.Point(225, 403);
            this.BTN_DestToSource.Name = "BTN_DestToSource";
            this.BTN_DestToSource.Size = new System.Drawing.Size(197, 23);
            this.BTN_DestToSource.TabIndex = 15;
            this.BTN_DestToSource.Text = "Transfer From Dest to Source";
            this.BTN_DestToSource.UseVisualStyleBackColor = true;
            this.BTN_DestToSource.Click += new System.EventHandler(this.BTN_DestToSource_Click);
            // 
            // BTN_TransferSourceDest
            // 
            this.BTN_TransferSourceDest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BTN_TransferSourceDest.Enabled = false;
            this.BTN_TransferSourceDest.Location = new System.Drawing.Point(9, 403);
            this.BTN_TransferSourceDest.Name = "BTN_TransferSourceDest";
            this.BTN_TransferSourceDest.Size = new System.Drawing.Size(191, 23);
            this.BTN_TransferSourceDest.TabIndex = 14;
            this.BTN_TransferSourceDest.Text = "Transfer From Source to Dest";
            this.BTN_TransferSourceDest.UseVisualStyleBackColor = true;
            this.BTN_TransferSourceDest.Click += new System.EventHandler(this.BTN_TransferSourceDest_Click);
            // 
            // DirectoryCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 516);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.BTN_Cancel);
            this.Controls.Add(this.PB_CurrentTransfer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(731, 500);
            this.Name = "DirectoryCompare";
            this.Text = "Windows Directory Copy";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DirectoryCompare_FormClosing);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.TC_Destination.ResumeLayout(false);
            this.TP_Source.ResumeLayout(false);
            this.TP_Source.PerformLayout();
            this.TP_Destination.ResumeLayout(false);
            this.TP_Destination.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.TP_SourceDestDiff.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.TP_Combined.ResumeLayout(false);
            this.TP_Combined.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ProgressBar PB_CurrentTransfer;
        private System.Windows.Forms.Timer MessageTimer;
        private System.Windows.Forms.Button BTN_Cancel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button BTN_PrintDiffDest;
        private System.Windows.Forms.Button BTN_PrintDiffSource;
        private System.Windows.Forms.Label LBL_Message;
        private System.Windows.Forms.Button BTN_DestToSource;
        private System.Windows.Forms.Button BTN_TransferSourceDest;
        private System.Windows.Forms.TabControl TC_Destination;
        private System.Windows.Forms.TabPage TP_Source;
        private System.Windows.Forms.Button BTN_LoadSrc;
        private System.Windows.Forms.Button BTN_SaveSrc;
        private System.Windows.Forms.Label LBL_SFilterMessage;
        private System.Windows.Forms.TextBox TB_SFilters;
        private System.Windows.Forms.TreeView TV_Source;
        private System.Windows.Forms.Button BTN_GetSource;
        private System.Windows.Forms.TabPage TP_Destination;
        private System.Windows.Forms.Button BTN_LoadDst;
        private System.Windows.Forms.Button BTN_SaveDst;
        private System.Windows.Forms.Label LBL_DFilterMessage;
        private System.Windows.Forms.TextBox TB_DFilters;
        private System.Windows.Forms.TreeView TV_Destination;
        private System.Windows.Forms.Button BTN_GetDestination;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TP_SourceDestDiff;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView TV_SourceDiff;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView TV_DestinationDiff;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage TP_Combined;
        private System.Windows.Forms.TreeView TV_CombinedResults;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}

