namespace EpisodeRenamer
{
    partial class ManualCorrectionWindows
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
            this.manualPanel = new System.Windows.Forms.Panel();
            this.btn_groupAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // manualPanel
            // 
            this.manualPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.manualPanel.AutoScroll = true;
            this.manualPanel.BackColor = System.Drawing.SystemColors.Control;
            this.manualPanel.Location = new System.Drawing.Point(1, 31);
            this.manualPanel.Name = "manualPanel";
            this.manualPanel.Size = new System.Drawing.Size(730, 232);
            this.manualPanel.TabIndex = 0;
            // 
            // btn_groupAll
            // 
            this.btn_groupAll.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_groupAll.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_groupAll.Location = new System.Drawing.Point(647, 2);
            this.btn_groupAll.Name = "btn_groupAll";
            this.btn_groupAll.Size = new System.Drawing.Size(84, 23);
            this.btn_groupAll.TabIndex = 1;
            this.btn_groupAll.Text = "Group All";
            this.btn_groupAll.UseVisualStyleBackColor = false;
            this.btn_groupAll.Click += new System.EventHandler(this.btn_groupAll_Click);
            // 
            // ManualCorrectionWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 263);
            this.Controls.Add(this.btn_groupAll);
            this.Controls.Add(this.manualPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximumSize = new System.Drawing.Size(750, 1080);
            this.MinimumSize = new System.Drawing.Size(750, 245);
            this.Name = "ManualCorrectionWindows";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ManualCorrectionWindows";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManualCorrectionWindows_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel manualPanel;
        private System.Windows.Forms.Button btn_groupAll;
    }
}