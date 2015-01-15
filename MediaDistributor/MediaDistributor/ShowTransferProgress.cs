using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MediaDistributor
{
    public partial class ShowTransferProgress : Form
    {
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void ShowTransferProgress_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public ShowTransferProgress()
        {
            InitializeComponent();
            TIM_CheckUp.Enabled = true;
        }

        private void LBL_Close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void TIM_CheckUp_Tick(object sender, EventArgs e)
        {
            if (ToolKit_MediaSeperator._TransferCurrent != null)
            {
                string[] saTransfer = ToolKit_MediaSeperator._TransferCurrent.Split(',');
                if (saTransfer.Length == 2)
                {
                    LBL_TransferingFrom.Text = string.Format("{0}", "Moving:  ").PadLeft(7) + saTransfer[0];
                    LBL_TransferingTo.Text = string.Format("{0}", "To:  ").PadLeft(7) + saTransfer[1];
                }
                else
                {
                    LBL_TransferingFrom.Text = ToolKit_MediaSeperator._TransferCurrent;
                    LBL_TransferingTo.Text = string.Empty;
                }
            }
            progressBar1.Value = ToolKit_MediaSeperator._TransferProgress;
        }
    }
}
