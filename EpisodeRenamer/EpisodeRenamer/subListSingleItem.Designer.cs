namespace CustomSortTitles
{
    partial class subListSingleItem
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
            this.TB_Title = new System.Windows.Forms.TextBox();
            this.LBL_Index = new System.Windows.Forms.Label();
            this.BTN_Up = new System.Windows.Forms.Button();
            this.BTN_Dn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TB_Title
            // 
            this.TB_Title.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Title.BackColor = System.Drawing.Color.White;
            this.TB_Title.Location = new System.Drawing.Point(34, 1);
            this.TB_Title.Name = "TB_Title";
            this.TB_Title.Size = new System.Drawing.Size(78, 20);
            this.TB_Title.TabIndex = 0;
            this.TB_Title.MouseClick += new System.Windows.Forms.MouseEventHandler(this.subListSingleItem_Click);
            // 
            // LBL_Index
            // 
            this.LBL_Index.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LBL_Index.BackColor = System.Drawing.Color.White;
            this.LBL_Index.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LBL_Index.Location = new System.Drawing.Point(3, 2);
            this.LBL_Index.Name = "LBL_Index";
            this.LBL_Index.Size = new System.Drawing.Size(25, 18);
            this.LBL_Index.TabIndex = 1;
            this.LBL_Index.Text = "_";
            this.LBL_Index.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LBL_Index.MouseClick += new System.Windows.Forms.MouseEventHandler(this.subListSingleItem_Click);
            // 
            // BTN_Up
            // 
            this.BTN_Up.AccessibleName = "MoveUpInOrder";
            this.BTN_Up.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_Up.Location = new System.Drawing.Point(118, 1);
            this.BTN_Up.Name = "BTN_Up";
            this.BTN_Up.Size = new System.Drawing.Size(31, 20);
            this.BTN_Up.TabIndex = 2;
            this.BTN_Up.Text = "Up";
            this.BTN_Up.UseVisualStyleBackColor = true;
            this.BTN_Up.Click += new System.EventHandler(this.BTN_Up_Click);
            // 
            // BTN_Dn
            // 
            this.BTN_Dn.AccessibleName = "MoveDownInOrder";
            this.BTN_Dn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_Dn.Location = new System.Drawing.Point(155, 1);
            this.BTN_Dn.Name = "BTN_Dn";
            this.BTN_Dn.Size = new System.Drawing.Size(31, 20);
            this.BTN_Dn.TabIndex = 3;
            this.BTN_Dn.Text = "Dn";
            this.BTN_Dn.UseVisualStyleBackColor = true;
            this.BTN_Dn.Click += new System.EventHandler(this.BTN_Dn_Click);
            // 
            // subListSingleItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.BTN_Dn);
            this.Controls.Add(this.BTN_Up);
            this.Controls.Add(this.LBL_Index);
            this.Controls.Add(this.TB_Title);
            this.Name = "subListSingleItem";
            this.Size = new System.Drawing.Size(189, 23);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.subListSingleItem_Click);
            this.MouseEnter += new System.EventHandler(this.subListSingleItem_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.subListSingleItem_MouseLeave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TB_Title;
        private System.Windows.Forms.Label LBL_Index;
        protected System.Windows.Forms.Button BTN_Up;
        protected System.Windows.Forms.Button BTN_Dn;
    }
}
