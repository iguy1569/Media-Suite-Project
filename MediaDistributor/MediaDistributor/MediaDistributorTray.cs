using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Text;
using System.Windows.Forms;

namespace MediaDistributor
{
    public partial class SysTrayApp : Form
    {
        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;
        
        private Thread tTransfer;
        private System.Windows.Forms.Timer timReset = new System.Windows.Forms.Timer();
        private ShowTransferProgress stp = new ShowTransferProgress();

        public delegate void HideTransferMenu();
        public event HideTransferMenu hideTrans;
        public delegate void UpdateClock(string s);
        public event UpdateClock DisplayClock;

        private int _CurrentTime = 0,
                    _ResetTime = 604800;

        private bool bCurrentTransfer = false;
        
        public SysTrayApp()
        {
            // Create a simple tray menu with only one item.
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Exit", OnExit);

            // Create a tray icon. In this example we use a
            // standard system icon for simplicity, but you
            // can of course use your own custom icon too.
            trayIcon = new NotifyIcon();
            trayIcon.Text = "Media Distribution Automator";
            trayIcon.Icon = new Icon(Properties.Resources.TrayIcon, 64, 64);

            // Add menu to tray icon and show it.
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;
            trayIcon.Click += ShowOptions;
            ToolKit_MediaSeperator.TransferStarted += new ToolKit_MediaSeperator.PromptStart(PauseFunctions);
            ToolKit_MediaSeperator.TransferFinished += new ToolKit_MediaSeperator.PromptFinish(ResumeFunctions);            
        }

        private void ResumeFunctions(object o, EventArgs e)
        {
            _CurrentTime = 0;
            bCurrentTransfer = false;
        }

        private void PauseFunctions(object o, EventArgs e)
        {
            bCurrentTransfer = true;
        }

        #region Sort Media Functions
        private void SortMediaFolders(object o, EventArgs e)
        {
            if (_CurrentTime >= _ResetTime && !bCurrentTransfer)
            {
                _CurrentTime = 0;
                SortMediaBaseCall(new object(), new EventArgs());
            }
            else
                ++_CurrentTime;

            if (DisplayClock != null)
            {
                TimeSpan tsTemp = TimeSpan.FromSeconds(_ResetTime - _CurrentTime);
                DisplayClock(string.Format("Next Media Check {0}:{1}:{2}", (int)tsTemp.TotalHours, tsTemp.Minutes.ToString("00"), tsTemp.Seconds.ToString("00")));
            }
        }

        private void SortMediaBaseCall(object o, EventArgs e)
        {
            stp.Show();
            hideTrans += HideTransferMenuCall;
            tTransfer = new Thread(delegate() { SortThreadFunction(); });
            tTransfer.IsBackground = true;
            tTransfer.Start();
        }

        private void SortThreadFunction()
        {
            List<FileInfo> lfiConfigFiles = ToolKit_MediaSeperator.GetConfigFileList();
            foreach (FileInfo fi in lfiConfigFiles)
                RunConfigChecks(fi);
            Thread.Sleep(5000);
            Invoke(hideTrans);
        }

        private void HideTransferMenuCall()
        {
            stp.Hide();
            hideTrans -= HideTransferMenuCall;
        }

        private void RunConfigChecks(FileInfo fiConfigName)
        {
            if (!Directory.Exists(ToolKit_MediaSeperator.GetSourceDirectory(fiConfigName.Name).FullName))
                return;
            if (ToolKit_MediaSeperator._SourceFolderConfig.FullName.Contains(@"\\") && !ToolKit_MediaSeperator.GetSourcePermissions(fiConfigName.Name))
                return;

            if (!Directory.Exists(ToolKit_MediaSeperator.GetDesinationDirectory(fiConfigName.Name).FullName))
                return;
            if (ToolKit_MediaSeperator._DestinationFolderConfig.FullName.Contains(@"\\") && !ToolKit_MediaSeperator.GetDesinationPermissions(fiConfigName.Name))
                return;

            ToolKit_MediaSeperator.GetMediaExtensionFiltersFromConfig(fiConfigName.Name);

            ToolKit_MediaSeperator.GetAlphaRangeFromConfig(fiConfigName.Name);

            List<FileInfo> lfiFiles = ToolKit_MediaSeperator.GetMediaFromFolders(ToolKit_MediaSeperator._SourceFolderConfig, true);

            ToolKit_MediaSeperator.GetDesinationPermissions(fiConfigName.Name);

            switch (ToolKit_MediaSeperator.GetMediaTypeFromConfig(fiConfigName.Name))
            {
                case 0:
                    ToolKit_MediaSeperator.MoveMediaContentOneToMany(lfiFiles, ToolKit_MediaSeperator.GetTransferFromConfig(fiConfigName.Name));
                    break;
                case 1:
                    ToolKit_MediaSeperator.MoveMediaContentOneToOne(lfiFiles, ToolKit_MediaSeperator.GetTransferFromConfig(fiConfigName.Name),
                                                              ToolKit_MediaSeperator.GetSubDirectoryFromConfig(fiConfigName.Name), true);
                    break;
                case 2:
                    ToolKit_MediaSeperator.MoveMediaContentOneToOne(lfiFiles, ToolKit_MediaSeperator.GetTransferFromConfig(fiConfigName.Name),
                                          ToolKit_MediaSeperator.GetSubDirectoryFromConfig(fiConfigName.Name), false);
                    break;
                default:
                    break;
            }   

            ToolKit_MediaSeperator.GetTransferFromConfig(fiConfigName.Name);
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            Visible = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.
            timReset.Interval = (int) TimeSpan.FromSeconds(1).TotalMilliseconds;
            timReset.Enabled = true;
            timReset.Tick += SortMediaFolders;
            timReset.Enabled = true;

            base.OnLoad(e);
        }

        private void OnExit(object sender, EventArgs e)
        {
            timReset.Enabled = false;
            Application.Exit();
        }

        private void ShowOptions(object o, EventArgs e)
        {
            ConfigEditWindow cew = new ConfigEditWindow((int) TimeSpan.FromSeconds(_ResetTime).TotalHours);
            DisplayClock += new UpdateClock(cew.GetClockValue);
            cew.BTN_RunSort.Click += SortMediaBaseCall;
            cew.ShowDialog();
            DisplayClock -= new UpdateClock(cew.GetClockValue);
            cew.BTN_RunSort.Click -= SortMediaBaseCall;
          
            if (TimeSpan.FromHours(cew._TimerHours).TotalMilliseconds > 0)
                _ResetTime = (int) TimeSpan.FromHours(cew._TimerHours).TotalSeconds;
        }

        private void BTN_Close_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.ShowInTaskbar = false;
        }
    }
}
