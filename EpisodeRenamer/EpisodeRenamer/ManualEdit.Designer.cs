namespace EpisodeRenamer
{
    partial class ManualEdit
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.BTN_Set = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(235, 20);
            this.textBox1.TabIndex = 0;
            // 
            // BTN_Set
            // 
            this.BTN_Set.Location = new System.Drawing.Point(253, 10);
            this.BTN_Set.Name = "BTN_Set";
            this.BTN_Set.Size = new System.Drawing.Size(44, 23);
            this.BTN_Set.TabIndex = 1;
            this.BTN_Set.Text = "Set";
            this.BTN_Set.UseVisualStyleBackColor = true;
            this.BTN_Set.Click += new System.EventHandler(this.BTN_Set_Click);
            // 
            // ManualEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 41);
            this.Controls.Add(this.BTN_Set);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ManualEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ManualEdit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button BTN_Set;
    }
}