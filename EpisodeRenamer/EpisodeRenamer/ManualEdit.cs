using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EpisodeRenamer
{
    public partial class ManualEdit : Form
    {
        private ListViewItem _lviItem;

        public ManualEdit(ListViewItem lvi)
        {
            InitializeComponent();
            _lviItem = lvi;
            textBox1.Text = lvi.SubItems[lvi.SubItems.Count - 1].Text;
        }

        private void BTN_Set_Click(object sender, EventArgs e)
        {
            _lviItem.SubItems[_lviItem.SubItems.Count - 1].Text = textBox1.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
