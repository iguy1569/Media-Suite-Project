namespace EpisodeRenamer
{
    partial class ManualConfig
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.useDB1 = new System.Windows.Forms.GroupBox();
            this.tb_showtitle = new System.Windows.Forms.TextBox();
            this.nonDB = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nud_actEpisode = new System.Windows.Forms.NumericUpDown();
            this.nud_actSeason = new System.Windows.Forms.NumericUpDown();
            this.UseDB2 = new System.Windows.Forms.GroupBox();
            this.dud_YearAired = new System.Windows.Forms.DomainUpDown();
            this.nonDB2 = new System.Windows.Forms.GroupBox();
            this.TB_Output = new System.Windows.Forms.TextBox();
            this.btn_commit = new System.Windows.Forms.Button();
            this.useDB3 = new System.Windows.Forms.GroupBox();
            this.cmbx_Episodes = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbx_ShowID = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LBL_Original = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btn_group = new System.Windows.Forms.Button();
            this.useDB1.SuspendLayout();
            this.nonDB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_actEpisode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_actSeason)).BeginInit();
            this.UseDB2.SuspendLayout();
            this.nonDB2.SuspendLayout();
            this.useDB3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // useDB1
            // 
            this.useDB1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.useDB1.Controls.Add(this.tb_showtitle);
            this.useDB1.Location = new System.Drawing.Point(3, 31);
            this.useDB1.Name = "useDB1";
            this.useDB1.Size = new System.Drawing.Size(264, 41);
            this.useDB1.TabIndex = 0;
            this.useDB1.TabStop = false;
            this.useDB1.Text = "Show Title";
            // 
            // tb_showtitle
            // 
            this.tb_showtitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_showtitle.Location = new System.Drawing.Point(6, 15);
            this.tb_showtitle.Name = "tb_showtitle";
            this.tb_showtitle.Size = new System.Drawing.Size(252, 20);
            this.tb_showtitle.TabIndex = 0;
            this.tb_showtitle.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_showtitle_KeyUp);
            // 
            // nonDB
            // 
            this.nonDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nonDB.Controls.Add(this.label2);
            this.nonDB.Controls.Add(this.label1);
            this.nonDB.Controls.Add(this.nud_actEpisode);
            this.nonDB.Controls.Add(this.nud_actSeason);
            this.nonDB.Location = new System.Drawing.Point(368, 31);
            this.nonDB.Name = "nonDB";
            this.nonDB.Size = new System.Drawing.Size(168, 41);
            this.nonDB.TabIndex = 1;
            this.nonDB.TabStop = false;
            this.nonDB.Text = "Shown in Label";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "E";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "S";
            // 
            // nud_actEpisode
            // 
            this.nud_actEpisode.Location = new System.Drawing.Point(107, 16);
            this.nud_actEpisode.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nud_actEpisode.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_actEpisode.Name = "nud_actEpisode";
            this.nud_actEpisode.Size = new System.Drawing.Size(55, 20);
            this.nud_actEpisode.TabIndex = 1;
            this.nud_actEpisode.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_actEpisode.ValueChanged += new System.EventHandler(this.nud_actEpisode_ValueChanged);
            // 
            // nud_actSeason
            // 
            this.nud_actSeason.Location = new System.Drawing.Point(26, 16);
            this.nud_actSeason.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nud_actSeason.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_actSeason.Name = "nud_actSeason";
            this.nud_actSeason.Size = new System.Drawing.Size(55, 20);
            this.nud_actSeason.TabIndex = 0;
            this.nud_actSeason.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_actSeason.ValueChanged += new System.EventHandler(this.nud_actSeason_ValueChanged);
            // 
            // UseDB2
            // 
            this.UseDB2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UseDB2.Controls.Add(this.dud_YearAired);
            this.UseDB2.Location = new System.Drawing.Point(542, 31);
            this.UseDB2.Name = "UseDB2";
            this.UseDB2.Size = new System.Drawing.Size(89, 41);
            this.UseDB2.TabIndex = 4;
            this.UseDB2.TabStop = false;
            this.UseDB2.Text = "First Aired";
            // 
            // dud_YearAired
            // 
            this.dud_YearAired.Location = new System.Drawing.Point(7, 15);
            this.dud_YearAired.Name = "dud_YearAired";
            this.dud_YearAired.Size = new System.Drawing.Size(74, 20);
            this.dud_YearAired.TabIndex = 0;
            this.dud_YearAired.SelectedItemChanged += new System.EventHandler(this.dud_YearAired_SelectedItemChanged);
            // 
            // nonDB2
            // 
            this.nonDB2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nonDB2.Controls.Add(this.TB_Output);
            this.nonDB2.Location = new System.Drawing.Point(3, 126);
            this.nonDB2.Name = "nonDB2";
            this.nonDB2.Size = new System.Drawing.Size(628, 42);
            this.nonDB2.TabIndex = 5;
            this.nonDB2.TabStop = false;
            this.nonDB2.Text = "Output";
            // 
            // TB_Output
            // 
            this.TB_Output.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Output.Location = new System.Drawing.Point(6, 16);
            this.TB_Output.Name = "TB_Output";
            this.TB_Output.Size = new System.Drawing.Size(614, 20);
            this.TB_Output.TabIndex = 0;
            // 
            // btn_commit
            // 
            this.btn_commit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_commit.Location = new System.Drawing.Point(637, 94);
            this.btn_commit.Name = "btn_commit";
            this.btn_commit.Size = new System.Drawing.Size(65, 76);
            this.btn_commit.TabIndex = 6;
            this.btn_commit.Text = "Commit";
            this.btn_commit.UseVisualStyleBackColor = true;
            this.btn_commit.Click += new System.EventHandler(this.btn_commit_Click);
            // 
            // useDB3
            // 
            this.useDB3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.useDB3.Controls.Add(this.cmbx_Episodes);
            this.useDB3.Location = new System.Drawing.Point(3, 78);
            this.useDB3.Name = "useDB3";
            this.useDB3.Size = new System.Drawing.Size(628, 42);
            this.useDB3.TabIndex = 6;
            this.useDB3.TabStop = false;
            this.useDB3.Text = "DB Episode Select";
            // 
            // cmbx_Episodes
            // 
            this.cmbx_Episodes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbx_Episodes.FormattingEnabled = true;
            this.cmbx_Episodes.Location = new System.Drawing.Point(5, 16);
            this.cmbx_Episodes.Name = "cmbx_Episodes";
            this.cmbx_Episodes.Size = new System.Drawing.Size(615, 21);
            this.cmbx_Episodes.TabIndex = 0;
            this.cmbx_Episodes.DropDown += new System.EventHandler(this.cmbx_Episodes_DropDown);
            this.cmbx_Episodes.SelectedIndexChanged += new System.EventHandler(this.cmbx_Episodes_SelectedIndexChanged);
            this.cmbx_Episodes.DropDownClosed += new System.EventHandler(this.cmbx_Episodes_DropDownClosed);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cmbx_ShowID);
            this.groupBox1.Location = new System.Drawing.Point(273, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(89, 41);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ShowID";
            // 
            // cmbx_ShowID
            // 
            this.cmbx_ShowID.FormattingEnabled = true;
            this.cmbx_ShowID.Location = new System.Drawing.Point(7, 14);
            this.cmbx_ShowID.Name = "cmbx_ShowID";
            this.cmbx_ShowID.Size = new System.Drawing.Size(76, 21);
            this.cmbx_ShowID.TabIndex = 0;
            this.cmbx_ShowID.SelectedIndexChanged += new System.EventHandler(this.cmbx_ShowID_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Originonal Title: ";
            // 
            // LBL_Original
            // 
            this.LBL_Original.AutoSize = true;
            this.LBL_Original.Location = new System.Drawing.Point(97, 12);
            this.LBL_Original.Name = "LBL_Original";
            this.LBL_Original.Size = new System.Drawing.Size(0, 13);
            this.LBL_Original.TabIndex = 8;
            // 
            // btn_group
            // 
            this.btn_group.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_group.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_group.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_group.Location = new System.Drawing.Point(637, 12);
            this.btn_group.Name = "btn_group";
            this.btn_group.Size = new System.Drawing.Size(65, 76);
            this.btn_group.TabIndex = 10;
            this.btn_group.Text = "Group";
            this.btn_group.UseVisualStyleBackColor = false;
            this.btn_group.Click += new System.EventHandler(this.btn_group_Click);
            // 
            // ManualConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btn_group);
            this.Controls.Add(this.LBL_Original);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.useDB3);
            this.Controls.Add(this.btn_commit);
            this.Controls.Add(this.nonDB2);
            this.Controls.Add(this.UseDB2);
            this.Controls.Add(this.nonDB);
            this.Controls.Add(this.useDB1);
            this.MaximumSize = new System.Drawing.Size(1920, 145);
            this.MinimumSize = new System.Drawing.Size(550, 175);
            this.Name = "ManualConfig";
            this.Size = new System.Drawing.Size(705, 173);
            this.Load += new System.EventHandler(this.ManualConfig_Load);
            this.useDB1.ResumeLayout(false);
            this.useDB1.PerformLayout();
            this.nonDB.ResumeLayout(false);
            this.nonDB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_actEpisode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_actSeason)).EndInit();
            this.UseDB2.ResumeLayout(false);
            this.nonDB2.ResumeLayout(false);
            this.nonDB2.PerformLayout();
            this.useDB3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox useDB1;
        private System.Windows.Forms.TextBox tb_showtitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nud_actEpisode;
        private System.Windows.Forms.NumericUpDown nud_actSeason;
        private System.Windows.Forms.GroupBox UseDB2;
        private System.Windows.Forms.GroupBox nonDB2;
        private System.Windows.Forms.TextBox TB_Output;
        private System.Windows.Forms.Button btn_commit;
        private System.Windows.Forms.GroupBox nonDB;
        private System.Windows.Forms.GroupBox useDB3;
        private System.Windows.Forms.ComboBox cmbx_Episodes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbx_ShowID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LBL_Original;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btn_group;
        private System.Windows.Forms.DomainUpDown dud_YearAired;
    }
}
