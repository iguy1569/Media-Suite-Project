using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MediaDistributor
{
    public partial class ConfigEditWindow : Form
    {
        public ConfigEditWindow(int iHours)
        {
            InitializeComponent();
            Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - Size.Width, Screen.PrimaryScreen.WorkingArea.Bottom - Size.Height);
            _TimerHours = iHours;
        }

        public int _TimerHours { get; private set; }

        private void ConfigEditWindow_Load(object sender, EventArgs e)
        {
            CreateConfigList();
            NUD_CheckChanges.Value = _TimerHours;
        }

        private void CreateConfigList()
        {
            LB_ConfigList.Items.Clear();
            List<FileInfo> fiTemp = ToolKit_MediaSeperator.GetConfigFileList();
            foreach (FileInfo fi in fiTemp)
                LB_ConfigList.Items.Add(fi.Name.Replace(fi.Extension, ""));

            if (fiTemp.Count <= 0)
            {
                BTN_Edit.Enabled = false;
                BTN_RemoveConfig.Enabled = false;
            }
            else
            {
                BTN_Edit.Enabled = true;
                BTN_RemoveConfig.Enabled = true;
                LB_ConfigList.SelectedIndex = 0;
            }
        }

        private void BTN_AddConfig_Click(object sender, EventArgs e)
        {
            string sConfigName = "MediaConfig00" + ToolKit_MediaSeperator.ConfigExtension;
            int iTemp = 0;

            while (new FileInfo(sConfigName).Exists)
                sConfigName = "MediaConfig" + (++iTemp).ToString("00") + ToolKit_MediaSeperator.ConfigExtension;


            LBL_Status.Text = ToolKit_MediaSeperator.BuildDefaultConfigFile(sConfigName);
            CreateConfigList();

            LB_ConfigList.SetSelected(LB_ConfigList.Items.Count - 1, true);
            SetUpConfig();
        }

        private void BTN_RemoveConfig_Click(object sender, EventArgs e)
        {
            if (LB_ConfigList.SelectedIndex != -1)
            {
                LBL_Status.Text = ToolKit_MediaSeperator.DeleteConfigFile(LB_ConfigList.SelectedItem.ToString() + ToolKit_MediaSeperator.ConfigExtension);
                CreateConfigList();
            }
        }

        private void BTN_Edit_Click(object sender, EventArgs e)
        {
            SetUpConfig();
        }

        private void SetUpConfig()
        {
            if (LB_ConfigList.SelectedIndex != -1)
            {
                EditConfigFileWindow ecfw = new EditConfigFileWindow(LB_ConfigList.SelectedItem.ToString() + ToolKit_MediaSeperator.ConfigExtension);
                ecfw.ShowDialog();
            }
        }

        private void NUD_CheckChanges_ValueChanged(object sender, EventArgs e)
        {
            _TimerHours = (int)NUD_CheckChanges.Value;
        }

        public void GetClockValue(string s)
        {
            LBL_Status.Text = s;
        }
    }
}
