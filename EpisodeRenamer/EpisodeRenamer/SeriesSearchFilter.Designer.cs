namespace EpisodeRenamer
{
    partial class SeriesSearchFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SeriesSearchFilter));
            this.LB_Results = new System.Windows.Forms.ListBox();
            this.LBL_ShowSynop = new System.Windows.Forms.Label();
            this.BTN_AcceptIndex = new System.Windows.Forms.Button();
            this.Tim_Count = new System.Windows.Forms.Timer(this.components);
            this.CB_RememberChoice = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // LB_Results
            // 
            this.LB_Results.FormattingEnabled = true;
            this.LB_Results.Location = new System.Drawing.Point(12, 23);
            this.LB_Results.Name = "LB_Results";
            this.LB_Results.Size = new System.Drawing.Size(260, 121);
            this.LB_Results.TabIndex = 0;
            this.LB_Results.SelectedValueChanged += new System.EventHandler(this.LB_Results_SelectedValueChanged);
            // 
            // LBL_ShowSynop
            // 
            this.LBL_ShowSynop.Location = new System.Drawing.Point(12, 151);
            this.LBL_ShowSynop.Name = "LBL_ShowSynop";
            this.LBL_ShowSynop.Size = new System.Drawing.Size(260, 102);
            this.LBL_ShowSynop.TabIndex = 1;
            this.LBL_ShowSynop.Text = "Show info here.";
            // 
            // BTN_AcceptIndex
            // 
            this.BTN_AcceptIndex.Location = new System.Drawing.Point(12, 256);
            this.BTN_AcceptIndex.Name = "BTN_AcceptIndex";
            this.BTN_AcceptIndex.Size = new System.Drawing.Size(260, 23);
            this.BTN_AcceptIndex.TabIndex = 2;
            this.BTN_AcceptIndex.Text = "Select Series";
            this.BTN_AcceptIndex.UseVisualStyleBackColor = true;
            this.BTN_AcceptIndex.Click += new System.EventHandler(this.BTN_AcceptIndex_Click);
            // 
            // Tim_Count
            // 
            this.Tim_Count.Interval = 1000;
            this.Tim_Count.Tick += new System.EventHandler(this.Tim_Count_Tick);
            // 
            // CB_RememberChoice
            // 
            this.CB_RememberChoice.AutoSize = true;
            this.CB_RememberChoice.Location = new System.Drawing.Point(13, 286);
            this.CB_RememberChoice.Name = "CB_RememberChoice";
            this.CB_RememberChoice.Size = new System.Drawing.Size(113, 17);
            this.CB_RememberChoice.TabIndex = 3;
            this.CB_RememberChoice.Text = "Remember Choice";
            this.CB_RememberChoice.UseVisualStyleBackColor = true;
            this.CB_RememberChoice.CheckedChanged += new System.EventHandler(this.CB_RememberChoice_CheckedChanged);
            // 
            // SeriesSearchFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 305);
            this.Controls.Add(this.CB_RememberChoice);
            this.Controls.Add(this.BTN_AcceptIndex);
            this.Controls.Add(this.LBL_ShowSynop);
            this.Controls.Add(this.LB_Results);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SeriesSearchFilter";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SeriesSearchFilter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SeriesSearchFilter_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LB_Results;
        private System.Windows.Forms.Label LBL_ShowSynop;
        private System.Windows.Forms.Button BTN_AcceptIndex;
        private System.Windows.Forms.Timer Tim_Count;
        private System.Windows.Forms.CheckBox CB_RememberChoice;

    }
}