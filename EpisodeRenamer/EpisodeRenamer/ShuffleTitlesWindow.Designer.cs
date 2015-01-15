using System.Windows.Forms;
namespace EpisodeRenamer
{
    partial class ShuffleTitlesWindow
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
            this.BTN_Cancel = new System.Windows.Forms.Button();
            this.BTN_Commit = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.customTitleSorter1 = new CustomSortTitles.CustomTitleSorter();
            this.SuspendLayout();
            // 
            // BTN_Cancel
            // 
            this.BTN_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BTN_Cancel.Location = new System.Drawing.Point(4, 262);
            this.BTN_Cancel.Name = "BTN_Cancel";
            this.BTN_Cancel.Size = new System.Drawing.Size(300, 23);
            this.BTN_Cancel.TabIndex = 1;
            this.BTN_Cancel.Text = "Cancel";
            this.BTN_Cancel.UseVisualStyleBackColor = true;
            this.BTN_Cancel.Click += new System.EventHandler(this.BTN_Cancel_Click);
            // 
            // BTN_Commit
            // 
            this.BTN_Commit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_Commit.Location = new System.Drawing.Point(310, 262);
            this.BTN_Commit.Name = "BTN_Commit";
            this.BTN_Commit.Size = new System.Drawing.Size(300, 23);
            this.BTN_Commit.TabIndex = 2;
            this.BTN_Commit.Text = "Commit";
            this.BTN_Commit.UseVisualStyleBackColor = true;
            this.BTN_Commit.Click += new System.EventHandler(this.BTN_Okay_Click);
            // 
            // customTitleSorter1
            // 
            this.customTitleSorter1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customTitleSorter1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.customTitleSorter1.Location = new System.Drawing.Point(5, 4);
            this.customTitleSorter1.MinimumSize = new System.Drawing.Size(605, 255);
            this.customTitleSorter1.Name = "customTitleSorter1";
            this.customTitleSorter1.Size = new System.Drawing.Size(605, 255);
            this.customTitleSorter1.TabIndex = 0;
            // 
            // ShuffleTitlesWindow
            // 
            this.ClientSize = new System.Drawing.Size(615, 288);
            this.Controls.Add(this.BTN_Commit);
            this.Controls.Add(this.BTN_Cancel);
            this.Controls.Add(this.customTitleSorter1);
            this.Name = "ShuffleTitlesWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.ResumeLayout(false);

        }

        #endregion
        private CustomSortTitles.CustomTitleSorter customTitleSorter1;
        private Button BTN_Cancel;
        private Button BTN_Commit;
        private ToolTip toolTip1;
    }
}