using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MediaDistributor
{
    public partial class AddFolderPermissions : Form
    {
        private string _ManipulateFolder ="",
                       _ConfigName = "";

        public AddFolderPermissions(string sManipulate, string sConfigName)
        {
            InitializeComponent();
            _ManipulateFolder = sManipulate;
            _ConfigName = sConfigName;
        }

        private void AddFolderPermissions_Load(object sender, EventArgs e)
        {
            BTN_OK.Enabled = false;
        }

        private void BTN_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTN_OK_Click(object sender, EventArgs e)
        {              
            if (_ManipulateFolder.ToLower().Equals("source"))
            {
                ToolKit_MediaSeperator.SourcePermissions = new ToolKit_MediaSeperator.PermissionsAccess(TB_Domain.Text, TB_User.Text, TB_Password.Text);
                ToolKit_MediaSeperator.SetSourcePermissions(_ConfigName);
            }
            else if (_ManipulateFolder.ToLower().Equals("destination"))
            {
                ToolKit_MediaSeperator.DestinationPermissions = new ToolKit_MediaSeperator.PermissionsAccess(TB_Domain.Text, TB_User.Text, TB_Password.Text);
                ToolKit_MediaSeperator.SetDestinationPermissions(_ConfigName);
            }
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void TB_Domain_TextChanged(object sender, EventArgs e)
        {
            if (TB_Domain.Text.Length > 0 && TB_User.Text.Length > 0)
                BTN_OK.Enabled = true;
            else
                BTN_OK.Enabled = false;
        }

        private void TB_User_TextChanged(object sender, EventArgs e)
        {
            if (TB_Domain.Text.Length > 0 && TB_User.Text.Length > 0)
                BTN_OK.Enabled = true;
            else
                BTN_OK.Enabled = false;
        }
    }
}
