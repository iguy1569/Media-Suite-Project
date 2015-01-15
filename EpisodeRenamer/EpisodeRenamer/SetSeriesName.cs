using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EpisodeRenamer
{
    public partial class SetSeriesName : Form
    {
        public SetSeriesName()
        {
            InitializeComponent();
        }

        public string TitleOverride { get; private set; }

        private void BTN_SetSeries_Click(object sender, EventArgs e)
        {
            TitleOverride = textBox1.Text.TrimEnd();
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TitleOverride = textBox1.Text.TrimEnd();
                this.Close();
            }
        }
    }
}
