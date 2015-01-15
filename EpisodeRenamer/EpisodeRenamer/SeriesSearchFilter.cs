using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EpisodeRenamer
{
    public partial class SeriesSearchFilter : Form
    {
        public int SeriesIndex { get; private set; }
        public bool SeriesMem { get; private set; }

        private List<TvdbLib.Data.TvdbSeries> _lTvSeriesResults = new List<TvdbLib.Data.TvdbSeries>();
        private int iTimeOut = 5;

        public SeriesSearchFilter(List<TvdbLib.Data.TvdbSeries> lTvSeries) 
            : this(lTvSeries, lTvSeries[0].Id)
        { }

        public SeriesSearchFilter(List<TvdbLib.Data.TvdbSeries> lTvSeries, int iLastId)
        {
            InitializeComponent();
            LB_Results.SelectedIndex = -1;
            _lTvSeriesResults = lTvSeries;
            for(int i = 0; i < lTvSeries.Count; i++)
            {
                LB_Results.Items.Add(lTvSeries[i].SeriesName);

                if (lTvSeries[i].Id.Equals(iLastId))
                    LB_Results.SelectedIndex = i;
            }

            // added last as a failsafe
            LB_Results.Items.Add("Series Not Found");

            if (LB_Results.SelectedIndex != -1)
            {
                Tim_Count.Enabled = true;
                BTN_AcceptIndex.Text = "Select Series (" + iTimeOut.ToString() + ")";
            }
        }

        private void LB_Results_SelectedValueChanged(object sender, EventArgs e)
        {
            if (LB_Results.SelectedIndex < _lTvSeriesResults.Count && LB_Results.SelectedIndex > -1)
            {
                LBL_ShowSynop.Text = _lTvSeriesResults[LB_Results.SelectedIndex].Overview;
                Tim_Count.Enabled = false;
            }
        }

        private void BTN_AcceptIndex_Click(object sender, EventArgs e)
        {
            ClosingProcedure();
        }

        private void Tim_Count_Tick(object sender, EventArgs e)
        {
            if (iTimeOut > 0)
            {
                iTimeOut--;
                BTN_AcceptIndex.Text = "Select Series (" + iTimeOut.ToString() + ")"; 
            }
            else
                ClosingProcedure();
        }

        private void ClosingProcedure()
        {
            if (LB_Results.SelectedIndex == LB_Results.Items.Count - 1)
                SeriesIndex = -1;
            else
                SeriesIndex = _lTvSeriesResults[LB_Results.SelectedIndex].Id;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SeriesSearchFilter_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
                e.Cancel = true;
        }

        private void CB_RememberChoice_CheckedChanged(object sender, EventArgs e)
        {
            SeriesMem = CB_RememberChoice.Checked;
        }
    }
}
