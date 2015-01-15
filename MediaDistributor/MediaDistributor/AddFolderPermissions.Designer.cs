namespace MediaDistributor
{
    partial class AddFolderPermissions
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
            this.TB_Domain = new System.Windows.Forms.TextBox();
            this.TB_User = new System.Windows.Forms.TextBox();
            this.TB_Password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BTN_Cancel = new System.Windows.Forms.Button();
            this.BTN_OK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TB_Domain
            // 
            this.TB_Domain.Location = new System.Drawing.Point(136, 12);
            this.TB_Domain.Name = "TB_Domain";
            this.TB_Domain.Size = new System.Drawing.Size(195, 20);
            this.TB_Domain.TabIndex = 0;
            this.TB_Domain.TextChanged += new System.EventHandler(this.TB_Domain_TextChanged);
            // 
            // TB_User
            // 
            this.TB_User.Location = new System.Drawing.Point(136, 38);
            this.TB_User.Name = "TB_User";
            this.TB_User.Size = new System.Drawing.Size(195, 20);
            this.TB_User.TabIndex = 1;
            this.TB_User.TextChanged += new System.EventHandler(this.TB_User_TextChanged);
            // 
            // TB_Password
            // 
            this.TB_Password.Location = new System.Drawing.Point(136, 64);
            this.TB_Password.Name = "TB_Password";
            this.TB_Password.PasswordChar = '*';
            this.TB_Password.Size = new System.Drawing.Size(195, 20);
            this.TB_Password.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Domain";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "User Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password";
            // 
            // BTN_Cancel
            // 
            this.BTN_Cancel.Location = new System.Drawing.Point(136, 91);
            this.BTN_Cancel.Name = "BTN_Cancel";
            this.BTN_Cancel.Size = new System.Drawing.Size(93, 23);
            this.BTN_Cancel.TabIndex = 6;
            this.BTN_Cancel.Text = "Cancel";
            this.BTN_Cancel.UseVisualStyleBackColor = true;
            this.BTN_Cancel.Click += new System.EventHandler(this.BTN_Cancel_Click);
            // 
            // BTN_OK
            // 
            this.BTN_OK.Location = new System.Drawing.Point(238, 91);
            this.BTN_OK.Name = "BTN_OK";
            this.BTN_OK.Size = new System.Drawing.Size(93, 23);
            this.BTN_OK.TabIndex = 7;
            this.BTN_OK.Text = "OK";
            this.BTN_OK.UseVisualStyleBackColor = true;
            this.BTN_OK.Click += new System.EventHandler(this.BTN_OK_Click);
            // 
            // AddFolderPermissions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 120);
            this.Controls.Add(this.BTN_OK);
            this.Controls.Add(this.BTN_Cancel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TB_Password);
            this.Controls.Add(this.TB_User);
            this.Controls.Add(this.TB_Domain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddFolderPermissions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AddFolderPermissions";
            this.Load += new System.EventHandler(this.AddFolderPermissions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TB_Domain;
        private System.Windows.Forms.TextBox TB_User;
        private System.Windows.Forms.TextBox TB_Password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BTN_Cancel;
        private System.Windows.Forms.Button BTN_OK;
    }
}