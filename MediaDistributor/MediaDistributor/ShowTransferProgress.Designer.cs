namespace MediaDistributor
{
    partial class ShowTransferProgress
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
            this.LBL_TransferingFrom = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.TIM_CheckUp = new System.Windows.Forms.Timer(this.components);
            this.LBL_Close = new System.Windows.Forms.Label();
            this.LBL_TransferingTo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LBL_TransferingFrom
            // 
            this.LBL_TransferingFrom.Location = new System.Drawing.Point(13, 5);
            this.LBL_TransferingFrom.Name = "LBL_TransferingFrom";
            this.LBL_TransferingFrom.Size = new System.Drawing.Size(397, 20);
            this.LBL_TransferingFrom.TabIndex = 0;
            this.LBL_TransferingFrom.Text = " - ";
            this.LBL_TransferingFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(5, 52);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(405, 15);
            this.progressBar1.TabIndex = 1;
            // 
            // TIM_CheckUp
            // 
            this.TIM_CheckUp.Interval = 500;
            this.TIM_CheckUp.Tick += new System.EventHandler(this.TIM_CheckUp_Tick);
            // 
            // LBL_Close
            // 
            this.LBL_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_Close.Location = new System.Drawing.Point(416, 5);
            this.LBL_Close.Name = "LBL_Close";
            this.LBL_Close.Size = new System.Drawing.Size(10, 10);
            this.LBL_Close.TabIndex = 4;
            this.LBL_Close.Text = "X";
            this.LBL_Close.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LBL_Close.Click += new System.EventHandler(this.LBL_Close_Click);
            // 
            // LBL_TransferingTo
            // 
            this.LBL_TransferingTo.Location = new System.Drawing.Point(13, 29);
            this.LBL_TransferingTo.Name = "LBL_TransferingTo";
            this.LBL_TransferingTo.Size = new System.Drawing.Size(397, 20);
            this.LBL_TransferingTo.TabIndex = 5;
            this.LBL_TransferingTo.Text = " - ";
            this.LBL_TransferingTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ShowTransferProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(430, 79);
            this.Controls.Add(this.LBL_TransferingTo);
            this.Controls.Add(this.LBL_Close);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.LBL_TransferingFrom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ShowTransferProgress";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ShowTransferProgress";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ShowTransferProgress_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LBL_TransferingFrom;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer TIM_CheckUp;
        private System.Windows.Forms.Label LBL_Close;
        private System.Windows.Forms.Label LBL_TransferingTo;
    }
}