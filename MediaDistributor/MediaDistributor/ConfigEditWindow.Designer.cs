namespace MediaDistributor
{
    partial class ConfigEditWindow
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
            this.LB_ConfigList = new System.Windows.Forms.ListBox();
            this.BTN_AddConfig = new System.Windows.Forms.Button();
            this.NUD_CheckChanges = new System.Windows.Forms.NumericUpDown();
            this.BTN_RemoveConfig = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.LBL_Status = new System.Windows.Forms.Label();
            this.BTN_Edit = new System.Windows.Forms.Button();
            this.TIM_Clock = new System.Windows.Forms.Timer(this.components);
            this.BTN_RunSort = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_CheckChanges)).BeginInit();
            this.SuspendLayout();
            // 
            // LB_ConfigList
            // 
            this.LB_ConfigList.FormattingEnabled = true;
            this.LB_ConfigList.Location = new System.Drawing.Point(13, 13);
            this.LB_ConfigList.Name = "LB_ConfigList";
            this.LB_ConfigList.Size = new System.Drawing.Size(309, 82);
            this.LB_ConfigList.TabIndex = 0;
            // 
            // BTN_AddConfig
            // 
            this.BTN_AddConfig.Location = new System.Drawing.Point(13, 99);
            this.BTN_AddConfig.Name = "BTN_AddConfig";
            this.BTN_AddConfig.Size = new System.Drawing.Size(56, 20);
            this.BTN_AddConfig.TabIndex = 1;
            this.BTN_AddConfig.Text = "Add";
            this.BTN_AddConfig.UseVisualStyleBackColor = true;
            this.BTN_AddConfig.Click += new System.EventHandler(this.BTN_AddConfig_Click);
            // 
            // NUD_CheckChanges
            // 
            this.NUD_CheckChanges.Location = new System.Drawing.Point(275, 125);
            this.NUD_CheckChanges.Maximum = new decimal(new int[] {
            168,
            0,
            0,
            0});
            this.NUD_CheckChanges.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUD_CheckChanges.Name = "NUD_CheckChanges";
            this.NUD_CheckChanges.Size = new System.Drawing.Size(47, 20);
            this.NUD_CheckChanges.TabIndex = 3;
            this.NUD_CheckChanges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NUD_CheckChanges.Value = new decimal(new int[] {
            168,
            0,
            0,
            0});
            this.NUD_CheckChanges.ValueChanged += new System.EventHandler(this.NUD_CheckChanges_ValueChanged);
            // 
            // BTN_RemoveConfig
            // 
            this.BTN_RemoveConfig.Location = new System.Drawing.Point(75, 99);
            this.BTN_RemoveConfig.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
            this.BTN_RemoveConfig.Name = "BTN_RemoveConfig";
            this.BTN_RemoveConfig.Size = new System.Drawing.Size(56, 20);
            this.BTN_RemoveConfig.TabIndex = 4;
            this.BTN_RemoveConfig.Text = "Remove";
            this.BTN_RemoveConfig.UseVisualStyleBackColor = true;
            this.BTN_RemoveConfig.Click += new System.EventHandler(this.BTN_RemoveConfig_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(212, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Interval (h)";
            // 
            // LBL_Status
            // 
            this.LBL_Status.Location = new System.Drawing.Point(12, 127);
            this.LBL_Status.Name = "LBL_Status";
            this.LBL_Status.Size = new System.Drawing.Size(181, 19);
            this.LBL_Status.TabIndex = 6;
            this.LBL_Status.Text = "-";
            this.LBL_Status.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // BTN_Edit
            // 
            this.BTN_Edit.Location = new System.Drawing.Point(137, 99);
            this.BTN_Edit.Name = "BTN_Edit";
            this.BTN_Edit.Size = new System.Drawing.Size(56, 20);
            this.BTN_Edit.TabIndex = 7;
            this.BTN_Edit.Text = "Edit";
            this.BTN_Edit.UseVisualStyleBackColor = true;
            this.BTN_Edit.Click += new System.EventHandler(this.BTN_Edit_Click);
            // 
            // TIM_Clock
            // 
            this.TIM_Clock.Enabled = true;
            this.TIM_Clock.Interval = 1000;
            // 
            // BTN_RunSort
            // 
            this.BTN_RunSort.Location = new System.Drawing.Point(266, 99);
            this.BTN_RunSort.Name = "BTN_RunSort";
            this.BTN_RunSort.Size = new System.Drawing.Size(56, 20);
            this.BTN_RunSort.TabIndex = 8;
            this.BTN_RunSort.Text = "Run";
            this.BTN_RunSort.UseVisualStyleBackColor = true;
            // 
            // ConfigEditWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 147);
            this.Controls.Add(this.BTN_RunSort);
            this.Controls.Add(this.BTN_Edit);
            this.Controls.Add(this.LBL_Status);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BTN_RemoveConfig);
            this.Controls.Add(this.NUD_CheckChanges);
            this.Controls.Add(this.BTN_AddConfig);
            this.Controls.Add(this.LB_ConfigList);
            this.MaximumSize = new System.Drawing.Size(350, 185);
            this.MinimumSize = new System.Drawing.Size(300, 185);
            this.Name = "ConfigEditWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ConfigEditWindow";
            this.Load += new System.EventHandler(this.ConfigEditWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NUD_CheckChanges)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LB_ConfigList;
        private System.Windows.Forms.Button BTN_AddConfig;
        private System.Windows.Forms.NumericUpDown NUD_CheckChanges;
        private System.Windows.Forms.Button BTN_RemoveConfig;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LBL_Status;
        private System.Windows.Forms.Button BTN_Edit;
        private System.Windows.Forms.Timer TIM_Clock;
        public System.Windows.Forms.Button BTN_RunSort;
    }
}