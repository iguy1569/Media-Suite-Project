using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EpisodeRenamer
{
    public partial class ManualCorrectionWindows : Form
    {
        private List<ManualConfig> _lmcConfigs = new List<ManualConfig>();
        private readonly int ciConfigHeight;
        private bool _toggleAll;
        public ManualCorrectionWindows()
        {
            InitializeComponent();
            ciConfigHeight = new ManualConfig().Height;
            manualPanel.VerticalScroll.Visible = true;
            _toggleAll = false;
        }

        public void ClearAllFields()
        {
            _lmcConfigs.Clear();
            SetPanel(_lmcConfigs);
        }

        public void AddEntry(string showTitle, string originalTitle, ListViewItem lvi, 
            Dictionary<string, KeyValuePair<FileInfo, string>> dsk)
        {
            foreach (ManualConfig current in _lmcConfigs)
                if (current.EpisodeDisplay == lvi)
                    return;

            ManualConfig mc 
                = new ManualConfig(showTitle, originalTitle, lvi, dsk, RemoveEntry, UpdateGroupings);
            mc.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            _lmcConfigs.Add(mc);
            mc.Show();
        }

        public void RemoveEntry(object entry)
        {
            ManualConfig item = (ManualConfig) entry;
            int itemIndex = _lmcConfigs.FindIndex(delegate (ManualConfig mc)
            {
                if (mc == item)
                    return true;
                return false;
            });

            if (itemIndex != -1)
                _lmcConfigs.RemoveAt(itemIndex);
            SetPanel(_lmcConfigs);
        }

        public void SetPanel()
        {
            SetPanel(_lmcConfigs);
        }

        private void SetPanel(List<ManualConfig> lmc)
        {
            manualPanel.Controls.Clear();
            int multiplier = 0;
            foreach (ManualConfig mc in lmc)
            {
                mc.Location = new Point(0, multiplier++ * ciConfigHeight);
                manualPanel.Controls.Add(mc);
                mc.Show();
            }
        }

        private void UpdateGroupings(string episodeName, List<TvdbLib.Data.TvdbSeries> ltvSeries,
            Dictionary<string, List<string>> EpisodesList, int season, int episode, int showID, int dbseason,
            ManualConfig mcCalled)
        {
            int offset = 0;
            foreach (ManualConfig mc in _lmcConfigs)
            {
                if (mc == mcCalled)
                    break;
                if (mc.SetGrouping != null)
                    offset++;
            }

            int icount = 0;
            foreach (ManualConfig mc in _lmcConfigs)
                if (mc.SetGrouping != null)
                {
                    int episodeNum = episode - offset + icount;
                    mc.SetGrouping(episodeName, ltvSeries, EpisodesList, season, episodeNum, showID, dbseason, null);
                    icount++;
                }
        }

        private void btn_groupAll_Click(object sender, EventArgs e)
        {
            _toggleAll = !_toggleAll;
            btn_groupAll.Text = _toggleAll ? "Ungroup All" : "Group All";

            foreach (ManualConfig mc in _lmcConfigs)
                mc.ToggleGroup = _toggleAll;
        }

        private void ManualCorrectionWindows_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
