using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MediaDistributor
{
    public partial class EditConfigFileWindow : Form
    {
        private string _sConfigName;

        public EditConfigFileWindow(string sConfigName)
        {
            InitializeComponent();
            _sConfigName = sConfigName;
        }

        private void EditConfigFileWindow_Load(object sender, EventArgs e)
        {
            // Transfer Operation Type
            if (ToolKit_MediaSeperator.GetTransferFromConfig(_sConfigName))
                RB_Copy.Checked = true;
            else
                RB_Move.Checked = true;

            // Set Directory Locations
            DirectoryInfo diTemp = ToolKit_MediaSeperator.GetSourceDirectory(_sConfigName);
            if (diTemp != null)
                TB_Source.Text = diTemp.FullName;

            diTemp = ToolKit_MediaSeperator.GetDesinationDirectory(_sConfigName);
            if (diTemp != null)
                TB_Destination.Text = diTemp.FullName;

            // Media Filters
            ToolKit_MediaSeperator.GetMediaExtensionFiltersFromConfig(_sConfigName);
            TB_Filters.Text = ToolKit_MediaSeperator.MediaFilter;

            // Transfer Operation Type
            if (ToolKit_MediaSeperator.GetMediaTypeFromConfig(_sConfigName) == 0)
                RB_Series.Checked = true;

            else if (ToolKit_MediaSeperator.GetMediaTypeFromConfig(_sConfigName) == 1)
                RB_Standalone.Checked = true;

            else if (ToolKit_MediaSeperator.GetMediaTypeFromConfig(_sConfigName) == 2)
                RB_Any.Checked = true;

            char[] caRange = ToolKit_MediaSeperator.GetAlphaRangeFromConfig(_sConfigName);
            if (caRange.Contains('0') && caRange.Contains('Z'))
            {
                RB_Default.Checked = true;
                BTN_SetRange.Enabled = false;
            }
            else
            {
                if (caRange.Length == 2)
                {
                    TB_SR.Text = caRange[0].ToString();
                    TB_ER.Text = caRange[1].ToString();
                    RB_Range.Checked = true;
                }
            }

            CB_KeepSubs.Checked = ToolKit_MediaSeperator.GetSubDirectoryFromConfig(_sConfigName);
        }

        private void BTN_Source_Click(object sender, EventArgs e)
        {
            if (FBD_Selection.ShowDialog() == DialogResult.OK)
            {
                if (FBD_Selection.SelectedPath.Contains(@"\\"))
                {
                    AddFolderPermissions afp = new AddFolderPermissions("source", _sConfigName);
                    afp.Location = new Point(this.Location.X + (this.Width / 2) - (afp.Width / 2), this.Location.Y + this.Height);
                    if (afp.ShowDialog() == DialogResult.OK)
                    {
                        TB_Source.Text = FBD_Selection.SelectedPath;
                        ToolKit_MediaSeperator.SetSourceDirectory(new DirectoryInfo(FBD_Selection.SelectedPath), _sConfigName);
                    }
                    else
                        TB_Source.Text = "";
                }
                else
                {
                    TB_Source.Text = FBD_Selection.SelectedPath;
                    ToolKit_MediaSeperator.SetSourceDirectory(new DirectoryInfo(FBD_Selection.SelectedPath), _sConfigName);
                }
            }
        }

        private void BTN_Destination_Click(object sender, EventArgs e)
        {
            if (FBD_Selection.ShowDialog() == DialogResult.OK)
            {
                if (FBD_Selection.SelectedPath.Contains(@"\\"))
                {
                    AddFolderPermissions afp = new AddFolderPermissions("destination", _sConfigName);
                    afp.Location = new Point(this.Location.X + (this.Width/2) - (afp.Width / 2) , this.Location.Y + this.Height);
                    if (afp.ShowDialog() == DialogResult.OK)
                    {
                        TB_Destination.Text = FBD_Selection.SelectedPath;
                        ToolKit_MediaSeperator.SetDestinationDirectory(new DirectoryInfo(FBD_Selection.SelectedPath), _sConfigName);
                    }
                    else
                        TB_Destination.Text = "";
                }
                else
                {
                    TB_Destination.Text = FBD_Selection.SelectedPath;
                    ToolKit_MediaSeperator.SetDestinationDirectory(new DirectoryInfo(FBD_Selection.SelectedPath), _sConfigName);
                }

            }
        }

        private void BTN_SetFilter_Click(object sender, EventArgs e)
        {
            ToolKit_MediaSeperator.MediaFilter = TB_Filters.Text;
            ToolKit_MediaSeperator.SetMediaExtensionFiltersForConfig(_sConfigName);
        }

        #region Media Type Transfer Functions 
        private void RB_Series_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_Series.Checked)
                ToolKit_MediaSeperator.SetMediaTypeForConfig(_sConfigName, 0);
        }

        private void RB_Standalone_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_Standalone.Checked)
                ToolKit_MediaSeperator.SetMediaTypeForConfig(_sConfigName, 1);
        }

        private void RB_Any_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_Any.Checked)
                ToolKit_MediaSeperator.SetMediaTypeForConfig(_sConfigName, 2);
        }

        #endregion

        private void RB_Move_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_Move.Checked)
                ToolKit_MediaSeperator.SetTransferForConfig(_sConfigName, false);
        }

        private void RB_Copy_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_Copy.Checked)
                ToolKit_MediaSeperator.SetTransferForConfig(_sConfigName, true);
        }

        private void RB_Default_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_Default.Checked)
            {
                ToolKit_MediaSeperator.SetAlphaRangeTypeForConfig(_sConfigName, new char[] { '0', 'Z' });
                BTN_SetRange.Enabled = false;
            }
            else
                BTN_SetRange.Enabled = true;
        }

        private void TB_SR_TextChanged(object sender, EventArgs e)
        {
            TB_SR.Text = TB_SR.Text.ToUpper();
            TB_ER.Text = TB_ER.Text.ToUpper();
            if (TB_SR.Text.Length > 0 && TB_ER.Text.Length > 0)
                if (Char.ConvertToUtf32(TB_SR.Text, 0) > Char.ConvertToUtf32(TB_ER.Text, 0))
                    TB_SR.Text = TB_ER.Text;
        }

        private void TB_ER_TextChanged(object sender, EventArgs e)
        {
            TB_SR.Text = TB_SR.Text.ToUpper();
            TB_ER.Text = TB_ER.Text.ToUpper();
            if (TB_SR.Text.Length > 0 && TB_ER.Text.Length > 0)
                if (Char.ConvertToUtf32(TB_SR.Text, 0) > Char.ConvertToUtf32(TB_ER.Text, 0))
                    TB_ER.Text = TB_SR.Text;
        }

        private void BTN_SetRange_Click(object sender, EventArgs e)
        {
            if (RB_Range.Checked && TB_SR.Text.Length > 0 && TB_ER.Text.Length > 0)
                ToolKit_MediaSeperator.SetAlphaRangeTypeForConfig(_sConfigName, new char[] { TB_SR.Text[0], TB_ER.Text[0] });
        }

        private void CB_KeepSubs_CheckedChanged(object sender, EventArgs e)
        {
            ToolKit_MediaSeperator.SetSubDirectoryForConfig(_sConfigName, CB_KeepSubs.Checked);
        }


    }
}
