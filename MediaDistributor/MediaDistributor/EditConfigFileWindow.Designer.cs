namespace MediaDistributor
{
    partial class EditConfigFileWindow
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CB_KeepSubs = new System.Windows.Forms.CheckBox();
            this.RB_Copy = new System.Windows.Forms.RadioButton();
            this.RB_Move = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TB_Destination = new System.Windows.Forms.TextBox();
            this.BTN_Destination = new System.Windows.Forms.Button();
            this.BTN_Source = new System.Windows.Forms.Button();
            this.TB_Source = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BTN_SetFilter = new System.Windows.Forms.Button();
            this.TB_Filters = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.RB_Standalone = new System.Windows.Forms.RadioButton();
            this.RB_Series = new System.Windows.Forms.RadioButton();
            this.FBD_Selection = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.BTN_SetRange = new System.Windows.Forms.Button();
            this.RB_Range = new System.Windows.Forms.RadioButton();
            this.RB_Default = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_ER = new System.Windows.Forms.TextBox();
            this.TB_SR = new System.Windows.Forms.TextBox();
            this.RB_Any = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CB_KeepSubs);
            this.groupBox1.Controls.Add(this.RB_Copy);
            this.groupBox1.Controls.Add(this.RB_Move);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 43);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transfer Operation Type";
            // 
            // CB_KeepSubs
            // 
            this.CB_KeepSubs.AutoSize = true;
            this.CB_KeepSubs.Location = new System.Drawing.Point(190, 21);
            this.CB_KeepSubs.Name = "CB_KeepSubs";
            this.CB_KeepSubs.Size = new System.Drawing.Size(118, 17);
            this.CB_KeepSubs.TabIndex = 2;
            this.CB_KeepSubs.Text = "Keep Sub Directory";
            this.CB_KeepSubs.UseVisualStyleBackColor = true;
            this.CB_KeepSubs.CheckedChanged += new System.EventHandler(this.CB_KeepSubs_CheckedChanged);
            // 
            // RB_Copy
            // 
            this.RB_Copy.AutoSize = true;
            this.RB_Copy.Location = new System.Drawing.Point(90, 20);
            this.RB_Copy.Name = "RB_Copy";
            this.RB_Copy.Size = new System.Drawing.Size(49, 17);
            this.RB_Copy.TabIndex = 1;
            this.RB_Copy.Text = "Copy";
            this.RB_Copy.UseVisualStyleBackColor = true;
            this.RB_Copy.CheckedChanged += new System.EventHandler(this.RB_Copy_CheckedChanged);
            // 
            // RB_Move
            // 
            this.RB_Move.AutoSize = true;
            this.RB_Move.Checked = true;
            this.RB_Move.Location = new System.Drawing.Point(26, 20);
            this.RB_Move.Name = "RB_Move";
            this.RB_Move.Size = new System.Drawing.Size(52, 17);
            this.RB_Move.TabIndex = 0;
            this.RB_Move.TabStop = true;
            this.RB_Move.Text = "Move";
            this.RB_Move.UseVisualStyleBackColor = true;
            this.RB_Move.CheckedChanged += new System.EventHandler(this.RB_Move_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TB_Destination);
            this.groupBox2.Controls.Add(this.BTN_Destination);
            this.groupBox2.Controls.Add(this.BTN_Source);
            this.groupBox2.Controls.Add(this.TB_Source);
            this.groupBox2.Location = new System.Drawing.Point(13, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(314, 76);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Set Directory Locations";
            // 
            // TB_Destination
            // 
            this.TB_Destination.Location = new System.Drawing.Point(88, 47);
            this.TB_Destination.Name = "TB_Destination";
            this.TB_Destination.Size = new System.Drawing.Size(220, 20);
            this.TB_Destination.TabIndex = 3;
            // 
            // BTN_Destination
            // 
            this.BTN_Destination.Location = new System.Drawing.Point(7, 45);
            this.BTN_Destination.Name = "BTN_Destination";
            this.BTN_Destination.Size = new System.Drawing.Size(75, 23);
            this.BTN_Destination.TabIndex = 2;
            this.BTN_Destination.Text = "Destination";
            this.BTN_Destination.UseVisualStyleBackColor = true;
            this.BTN_Destination.Click += new System.EventHandler(this.BTN_Destination_Click);
            // 
            // BTN_Source
            // 
            this.BTN_Source.Location = new System.Drawing.Point(7, 16);
            this.BTN_Source.Name = "BTN_Source";
            this.BTN_Source.Size = new System.Drawing.Size(75, 23);
            this.BTN_Source.TabIndex = 1;
            this.BTN_Source.Text = "Source";
            this.BTN_Source.UseVisualStyleBackColor = true;
            this.BTN_Source.Click += new System.EventHandler(this.BTN_Source_Click);
            // 
            // TB_Source
            // 
            this.TB_Source.Location = new System.Drawing.Point(88, 18);
            this.TB_Source.Name = "TB_Source";
            this.TB_Source.Size = new System.Drawing.Size(220, 20);
            this.TB_Source.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BTN_SetFilter);
            this.groupBox3.Controls.Add(this.TB_Filters);
            this.groupBox3.Location = new System.Drawing.Point(13, 146);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(314, 46);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Media Filters";
            // 
            // BTN_SetFilter
            // 
            this.BTN_SetFilter.Location = new System.Drawing.Point(7, 19);
            this.BTN_SetFilter.Name = "BTN_SetFilter";
            this.BTN_SetFilter.Size = new System.Drawing.Size(46, 20);
            this.BTN_SetFilter.TabIndex = 1;
            this.BTN_SetFilter.Text = "Set";
            this.BTN_SetFilter.UseVisualStyleBackColor = true;
            this.BTN_SetFilter.Click += new System.EventHandler(this.BTN_SetFilter_Click);
            // 
            // TB_Filters
            // 
            this.TB_Filters.Location = new System.Drawing.Point(59, 19);
            this.TB_Filters.Name = "TB_Filters";
            this.TB_Filters.Size = new System.Drawing.Size(249, 20);
            this.TB_Filters.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.RB_Any);
            this.groupBox4.Controls.Add(this.RB_Standalone);
            this.groupBox4.Controls.Add(this.RB_Series);
            this.groupBox4.Location = new System.Drawing.Point(13, 198);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(314, 43);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Transfer Operation Type";
            // 
            // RB_Standalone
            // 
            this.RB_Standalone.AutoSize = true;
            this.RB_Standalone.Location = new System.Drawing.Point(120, 19);
            this.RB_Standalone.Name = "RB_Standalone";
            this.RB_Standalone.Size = new System.Drawing.Size(79, 17);
            this.RB_Standalone.TabIndex = 1;
            this.RB_Standalone.Text = "Standalone";
            this.RB_Standalone.UseVisualStyleBackColor = true;
            this.RB_Standalone.CheckedChanged += new System.EventHandler(this.RB_Standalone_CheckedChanged);
            // 
            // RB_Series
            // 
            this.RB_Series.AutoSize = true;
            this.RB_Series.Checked = true;
            this.RB_Series.Location = new System.Drawing.Point(61, 19);
            this.RB_Series.Name = "RB_Series";
            this.RB_Series.Size = new System.Drawing.Size(54, 17);
            this.RB_Series.TabIndex = 0;
            this.RB_Series.Text = "Series";
            this.RB_Series.UseVisualStyleBackColor = true;
            this.RB_Series.CheckedChanged += new System.EventHandler(this.RB_Series_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.BTN_SetRange);
            this.groupBox5.Controls.Add(this.RB_Range);
            this.groupBox5.Controls.Add(this.RB_Default);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.TB_ER);
            this.groupBox5.Controls.Add(this.TB_SR);
            this.groupBox5.Location = new System.Drawing.Point(13, 248);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(314, 46);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "File Folder Alpha Limit";
            // 
            // BTN_SetRange
            // 
            this.BTN_SetRange.Location = new System.Drawing.Point(268, 17);
            this.BTN_SetRange.Name = "BTN_SetRange";
            this.BTN_SetRange.Size = new System.Drawing.Size(40, 20);
            this.BTN_SetRange.TabIndex = 5;
            this.BTN_SetRange.Text = "Set";
            this.BTN_SetRange.UseVisualStyleBackColor = true;
            this.BTN_SetRange.Click += new System.EventHandler(this.BTN_SetRange_Click);
            // 
            // RB_Range
            // 
            this.RB_Range.AutoSize = true;
            this.RB_Range.Location = new System.Drawing.Point(90, 20);
            this.RB_Range.Name = "RB_Range";
            this.RB_Range.Size = new System.Drawing.Size(112, 17);
            this.RB_Range.TabIndex = 4;
            this.RB_Range.Text = "Name Range Limit";
            this.RB_Range.UseVisualStyleBackColor = true;
            // 
            // RB_Default
            // 
            this.RB_Default.AutoSize = true;
            this.RB_Default.Checked = true;
            this.RB_Default.Location = new System.Drawing.Point(8, 20);
            this.RB_Default.Name = "RB_Default";
            this.RB_Default.Size = new System.Drawing.Size(74, 17);
            this.RB_Default.TabIndex = 3;
            this.RB_Default.TabStop = true;
            this.RB_Default.Text = "Any Name";
            this.RB_Default.UseVisualStyleBackColor = true;
            this.RB_Default.CheckedChanged += new System.EventHandler(this.RB_Default_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(229, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "-";
            // 
            // TB_ER
            // 
            this.TB_ER.Location = new System.Drawing.Point(243, 18);
            this.TB_ER.MaxLength = 1;
            this.TB_ER.Name = "TB_ER";
            this.TB_ER.Size = new System.Drawing.Size(19, 20);
            this.TB_ER.TabIndex = 1;
            this.TB_ER.Text = "Z";
            this.TB_ER.TextChanged += new System.EventHandler(this.TB_ER_TextChanged);
            // 
            // TB_SR
            // 
            this.TB_SR.Location = new System.Drawing.Point(208, 18);
            this.TB_SR.MaxLength = 1;
            this.TB_SR.Name = "TB_SR";
            this.TB_SR.Size = new System.Drawing.Size(19, 20);
            this.TB_SR.TabIndex = 0;
            this.TB_SR.Text = "0";
            this.TB_SR.TextChanged += new System.EventHandler(this.TB_SR_TextChanged);
            // 
            // RB_Any
            // 
            this.RB_Any.AutoSize = true;
            this.RB_Any.Location = new System.Drawing.Point(205, 19);
            this.RB_Any.Name = "RB_Any";
            this.RB_Any.Size = new System.Drawing.Size(43, 17);
            this.RB_Any.TabIndex = 2;
            this.RB_Any.Text = "Any";
            this.RB_Any.UseVisualStyleBackColor = true;
            this.RB_Any.CheckedChanged += new System.EventHandler(this.RB_Any_CheckedChanged);
            // 
            // EditConfigFileWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 306);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "EditConfigFileWindow";
            this.Text = "EditConfigFileWindow";
            this.Load += new System.EventHandler(this.EditConfigFileWindow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RB_Copy;
        private System.Windows.Forms.RadioButton RB_Move;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button BTN_Destination;
        private System.Windows.Forms.Button BTN_Source;
        private System.Windows.Forms.TextBox TB_Source;
        private System.Windows.Forms.TextBox TB_Destination;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox TB_Filters;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton RB_Standalone;
        private System.Windows.Forms.RadioButton RB_Series;
        private System.Windows.Forms.FolderBrowserDialog FBD_Selection;
        private System.Windows.Forms.Button BTN_SetFilter;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_ER;
        private System.Windows.Forms.TextBox TB_SR;
        private System.Windows.Forms.RadioButton RB_Range;
        private System.Windows.Forms.RadioButton RB_Default;
        private System.Windows.Forms.Button BTN_SetRange;
        private System.Windows.Forms.CheckBox CB_KeepSubs;
        private System.Windows.Forms.RadioButton RB_Any;
    }
}