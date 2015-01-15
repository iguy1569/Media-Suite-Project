namespace EpisodeRenamer
{
    partial class EpisodeNameConverter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EpisodeNameConverter));
            this.BTN_WikiPage = new System.Windows.Forms.Button();
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.TB_GetSeries = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.treeView = new System.Windows.Forms.TreeView();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.LBL_Status = new System.Windows.Forms.Label();
            this.TB_Filters = new System.Windows.Forms.TextBox();
            this.LBL_SetFilters = new System.Windows.Forms.Label();
            this.BTN_SetFilters = new System.Windows.Forms.Button();
            this.TIM_MessageReset = new System.Windows.Forms.Timer(this.components);
            this.TT_Help = new System.Windows.Forms.ToolTip(this.components);
            this.btn_ShowManual = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.BTN_RenameMedia = new System.Windows.Forms.Button();
            this.BTN_GetMedia = new System.Windows.Forms.Button();
            this.BTN_Manually = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN_WikiPage
            // 
            this.BTN_WikiPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_WikiPage.Location = new System.Drawing.Point(423, 9);
            this.BTN_WikiPage.Name = "BTN_WikiPage";
            this.BTN_WikiPage.Size = new System.Drawing.Size(200, 23);
            this.BTN_WikiPage.TabIndex = 0;
            this.BTN_WikiPage.Text = "Get Episode List";
            this.BTN_WikiPage.UseVisualStyleBackColor = true;
            this.BTN_WikiPage.Click += new System.EventHandler(this.BTN_WikiPage_Click);
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // TB_GetSeries
            // 
            this.TB_GetSeries.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_GetSeries.Location = new System.Drawing.Point(423, 39);
            this.TB_GetSeries.Name = "TB_GetSeries";
            this.TB_GetSeries.Size = new System.Drawing.Size(200, 20);
            this.TB_GetSeries.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(424, 322);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Check Series Episodes";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView.Location = new System.Drawing.Point(423, 65);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(200, 255);
            this.treeView.TabIndex = 4;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.BackColor = System.Drawing.Color.White;
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(13, 76);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(400, 244);
            this.listView1.TabIndex = 10;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView1_ItemChecked);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Previous Tag";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "New Tag";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox1.Location = new System.Drawing.Point(13, 323);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.checkBox1.Size = new System.Drawing.Size(200, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Select All";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // LBL_Status
            // 
            this.LBL_Status.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.LBL_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_Status.Location = new System.Drawing.Point(213, 322);
            this.LBL_Status.Name = "LBL_Status";
            this.LBL_Status.Size = new System.Drawing.Size(200, 17);
            this.LBL_Status.TabIndex = 12;
            this.LBL_Status.Text = "Awaiting";
            this.LBL_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TB_Filters
            // 
            this.TB_Filters.Location = new System.Drawing.Point(111, 50);
            this.TB_Filters.Name = "TB_Filters";
            this.TB_Filters.Size = new System.Drawing.Size(260, 20);
            this.TB_Filters.TabIndex = 13;
            this.TB_Filters.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_Filters_KeyDown);
            // 
            // LBL_SetFilters
            // 
            this.LBL_SetFilters.Location = new System.Drawing.Point(12, 50);
            this.LBL_SetFilters.Name = "LBL_SetFilters";
            this.LBL_SetFilters.Size = new System.Drawing.Size(98, 19);
            this.LBL_SetFilters.TabIndex = 14;
            this.LBL_SetFilters.Text = "Search Filters";
            this.LBL_SetFilters.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BTN_SetFilters
            // 
            this.BTN_SetFilters.Location = new System.Drawing.Point(378, 49);
            this.BTN_SetFilters.Name = "BTN_SetFilters";
            this.BTN_SetFilters.Size = new System.Drawing.Size(35, 20);
            this.BTN_SetFilters.TabIndex = 15;
            this.BTN_SetFilters.Text = "Set";
            this.BTN_SetFilters.UseVisualStyleBackColor = true;
            this.BTN_SetFilters.Click += new System.EventHandler(this.BTN_SetFilters_Click);
            // 
            // TIM_MessageReset
            // 
            this.TIM_MessageReset.Interval = 5000;
            this.TIM_MessageReset.Tick += new System.EventHandler(this.TIM_MessageReset_Tick);
            // 
            // TT_Help
            // 
            this.TT_Help.AutoPopDelay = 5000;
            this.TT_Help.InitialDelay = 500;
            this.TT_Help.ReshowDelay = 100;
            this.TT_Help.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.TT_Help.ToolTipTitle = "How To ";
            // 
            // btn_ShowManual
            // 
            this.btn_ShowManual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ShowManual.Location = new System.Drawing.Point(614, 336);
            this.btn_ShowManual.Name = "btn_ShowManual";
            this.btn_ShowManual.Size = new System.Drawing.Size(15, 15);
            this.btn_ShowManual.TabIndex = 16;
            this.btn_ShowManual.Text = "-";
            this.btn_ShowManual.UseVisualStyleBackColor = true;
            this.btn_ShowManual.Click += new System.EventHandler(this.btn_ShowManual_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.BTN_RenameMedia, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.BTN_GetMedia, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.BTN_Manually, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 9);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(400, 34);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // BTN_RenameMedia
            // 
            this.BTN_RenameMedia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_RenameMedia.Enabled = false;
            this.BTN_RenameMedia.Location = new System.Drawing.Point(136, 3);
            this.BTN_RenameMedia.Name = "BTN_RenameMedia";
            this.BTN_RenameMedia.Size = new System.Drawing.Size(127, 23);
            this.BTN_RenameMedia.TabIndex = 19;
            this.BTN_RenameMedia.Text = "Rename Media";
            this.BTN_RenameMedia.UseVisualStyleBackColor = true;
            this.BTN_RenameMedia.Click += new System.EventHandler(this.BTN_RenameMedia_Click);
            // 
            // BTN_GetMedia
            // 
            this.BTN_GetMedia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_GetMedia.Location = new System.Drawing.Point(3, 3);
            this.BTN_GetMedia.Name = "BTN_GetMedia";
            this.BTN_GetMedia.Size = new System.Drawing.Size(127, 23);
            this.BTN_GetMedia.TabIndex = 18;
            this.BTN_GetMedia.Text = "Select Media";
            this.BTN_GetMedia.UseVisualStyleBackColor = true;
            this.BTN_GetMedia.Click += new System.EventHandler(this.BTN_GetMedia_Click);
            // 
            // BTN_Manually
            // 
            this.BTN_Manually.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_Manually.Enabled = false;
            this.BTN_Manually.Location = new System.Drawing.Point(269, 3);
            this.BTN_Manually.Name = "BTN_Manually";
            this.BTN_Manually.Size = new System.Drawing.Size(128, 23);
            this.BTN_Manually.TabIndex = 20;
            this.BTN_Manually.Text = "Rename Manually";
            this.BTN_Manually.UseVisualStyleBackColor = true;
            this.BTN_Manually.Click += new System.EventHandler(this.btn_Manual_Click);
            // 
            // EpisodeNameConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 356);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btn_ShowManual);
            this.Controls.Add(this.BTN_SetFilters);
            this.Controls.Add(this.LBL_SetFilters);
            this.Controls.Add(this.TB_Filters);
            this.Controls.Add(this.LBL_Status);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TB_GetSeries);
            this.Controls.Add(this.BTN_WikiPage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(652, 394);
            this.Name = "EpisodeNameConverter";
            this.Text = "Automated Episode Renamer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EpisodeNameConverter_FormClosing);
            this.Load += new System.EventHandler(this.EpisodeNameConverter_Load);
            this.Resize += new System.EventHandler(this.EpisodeNameConverter_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTN_WikiPage;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private System.Windows.Forms.TextBox TB_GetSeries;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label LBL_Status;
        private System.Windows.Forms.TextBox TB_Filters;
        private System.Windows.Forms.Label LBL_SetFilters;
        private System.Windows.Forms.Button BTN_SetFilters;
        private System.Windows.Forms.Timer TIM_MessageReset;
        private System.Windows.Forms.ToolTip TT_Help;
        private System.Windows.Forms.Button btn_ShowManual;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button BTN_RenameMedia;
        private System.Windows.Forms.Button BTN_GetMedia;
        private System.Windows.Forms.Button BTN_Manually;

    }
}

