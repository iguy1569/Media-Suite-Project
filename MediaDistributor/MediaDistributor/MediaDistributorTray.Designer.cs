namespace MediaDistributor
{
    partial class SysTrayApp
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
            this.BTN_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BTN_Close
            // 
            this.BTN_Close.Location = new System.Drawing.Point(288, 12);
            this.BTN_Close.Name = "BTN_Close";
            this.BTN_Close.Size = new System.Drawing.Size(28, 21);
            this.BTN_Close.TabIndex = 0;
            this.BTN_Close.Text = "X";
            this.BTN_Close.UseVisualStyleBackColor = false;
            this.BTN_Close.Click += new System.EventHandler(this.BTN_Close_Click);
            // 
            // SysTrayApp
            // 
            this.ClientSize = new System.Drawing.Size(328, 252);
            this.Controls.Add(this.BTN_Close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SysTrayApp";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BTN_Close;
    }
}

