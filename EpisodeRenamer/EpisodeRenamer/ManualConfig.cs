using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using EpisodeTitleParser;
using TvdbLib;
using System.IO;

namespace EpisodeRenamer
{
    public partial class ManualConfig : UserControl
    {
        private string                           _episodeName;   
        private List<TvdbLib.Data.TvdbSeries>    _ltvSeries;
        private Dictionary<string, List<string>> _EpisodesList;
        private bool _eventEnabled = true;

        private Dictionary<string, KeyValuePair<FileInfo, string>> _episodeFiles;

        private bool _toggleGroup;
        public bool ToggleGroup 
        {
            get { return _toggleGroup; } 
            set 
            {
                _toggleGroup = value;
                btn_group.Text = _toggleGroup ? "Ungroup" : "Group";

                if (_toggleGroup)
                    SetGrouping = SetAllGroupFields;
                else
                    SetGrouping = null;

                if (SetGrouping != null)
                    GetGrouping(tb_showtitle.Text, _ltvSeries, _EpisodesList, (int)nud_actSeason.Value,
                        (int)nud_actEpisode.Value, cmbx_ShowID.SelectedIndex, dud_YearAired.SelectedIndex, this);
            } 
        }

        public delegate void delVoidObject(object o);
        private delVoidObject RemoveEntry;

        public ListViewItem EpisodeDisplay { get; private set; }

        public delegate void delVoidStrLtsDslIntIntIntIntMC(string episodeName, List<TvdbLib.Data.TvdbSeries> ltvSeries,
            Dictionary<string, List<string>> EpisodesList, int season, int episode, int showID, int dbseason, ManualConfig mc);

        public delVoidStrLtsDslIntIntIntIntMC SetGrouping;
        private delVoidStrLtsDslIntIntIntIntMC GetGrouping;
            
        public ManualConfig()
        {
            InitializeComponent();
            _episodeName = string.Empty;
            _ltvSeries = new List<TvdbLib.Data.TvdbSeries>();
            _toggleGroup = false;
        }

        public ManualConfig(string showTitle, string originalTitle,
            ListViewItem episodeDisplay, Dictionary<string, KeyValuePair<FileInfo, string>> EpisodesInfo,
            delVoidObject RemoveEntryMethod, delVoidStrLtsDslIntIntIntIntMC UpdateGroupingsMethod) 
            : this()
        {
            LBL_Original.Text = originalTitle;
            tb_showtitle.Text = showTitle;

            SetSeries(EpisodeSeriesBuilder.GetSeriesFromTitle(tb_showtitle.Text));
            if (_ltvSeries.Count > 0)
                cmbx_ShowID.Text = _ltvSeries[0].SeriesName;

            nud_actSeason.Value  = 1;
            nud_actEpisode.Value = 1;

            EpisodeDisplay = episodeDisplay;
            _episodeFiles = EpisodesInfo;

            RemoveEntry = RemoveEntryMethod;
            GetGrouping = UpdateGroupingsMethod;
        }

        private void ManualConfig_Load(object sender, EventArgs e)
        {
            toolTip.SetToolTip(tb_showtitle, "* Used to search fro show series, Press enter to confirm search *");
        }

        private void SetAllGroupFields(string episodeSearch, List<TvdbLib.Data.TvdbSeries> ltvSeries,
            Dictionary<string, List<string>> EpisodesList, int season, int episode, 
            int showID, int dbseason, ManualConfig mc)
        {
            _ltvSeries = ltvSeries;
            _EpisodesList = EpisodesList;
            tb_showtitle.Text =episodeSearch;

            // turn normal events off
            cmbx_ShowID.SelectedIndexChanged -= cmbx_ShowID_SelectedIndexChanged;
            nud_actSeason.ValueChanged -= nud_actSeason_ValueChanged;
            nud_actEpisode.ValueChanged -= nud_actEpisode_ValueChanged;
            dud_YearAired.SelectedItemChanged -= dud_YearAired_SelectedItemChanged;

            #region do changes
            if (_ltvSeries.Count > 0)
                SetSeries(_ltvSeries);

            cmbx_ShowID.SelectedIndex = showID;

            BuildFirstAiredList(dbseason);
                       
            nud_actSeason.Value = season > nud_actSeason.Maximum ? nud_actSeason.Maximum :
                season < nud_actSeason.Minimum ? nud_actSeason.Minimum : season;

            nud_actEpisode.Value = episode > nud_actEpisode.Maximum ? nud_actEpisode.Maximum :
                episode < nud_actEpisode.Minimum ? nud_actEpisode.Minimum : episode;
            #endregion

            // turn normal events back on
            dud_YearAired.SelectedItemChanged += dud_YearAired_SelectedItemChanged;
            nud_actSeason.ValueChanged += nud_actSeason_ValueChanged;
            cmbx_ShowID.SelectedIndexChanged += cmbx_ShowID_SelectedIndexChanged;
            nud_actEpisode.ValueChanged += nud_actEpisode_ValueChanged;

            UpdateOutput();
        }

        private void tb_showtitle_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetSeries(EpisodeSeriesBuilder.GetSeriesFromTitle(tb_showtitle.Text));

                if (_ltvSeries.Count > 0)
                {
                    cmbx_ShowID.Text = _ltvSeries[0].SeriesName;
                    cmbx_ShowID_SelectedIndexChanged(sender, e);
                }

                GetGrouping(tb_showtitle.Text, _ltvSeries, _EpisodesList, (int)nud_actSeason.Value,
                    (int)nud_actEpisode.Value, cmbx_ShowID.SelectedIndex, dud_YearAired.SelectedIndex, this);
            }
        }

        private void cmbx_ShowID_SelectedIndexChanged(object sender, EventArgs e)
        {
            TvdbLib.Data.TvdbSeries temp = GetSelectedSeries();
            if (temp == null)
                return;
            _EpisodesList = EpisodeSeriesBuilder.ContructEpisodeList(temp);

            dud_YearAired.Items.Clear();
            BuildFirstAiredList(0);
        }

        private void cmbx_Episodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            _episodeName = cmbx_Episodes.Text = (string)cmbx_Episodes.SelectedItem;
            UpdateOutput(); 
        }

        private void cmbx_Episodes_DropDown(object sender, EventArgs e)
        {
            cmbx_Episodes.TextChanged += cmbx_Episodes_TextChanged;
        }

        private void cmbx_Episodes_DropDownClosed(object sender, EventArgs e)
        {
            cmbx_Episodes.TextChanged -= cmbx_Episodes_TextChanged;
        }

        private void cmbx_Episodes_TextChanged(object sender, EventArgs e)
        {
            if (_EpisodesList.ContainsKey((string)dud_YearAired.SelectedItem))
            {
                if (cmbx_Episodes.Text.Equals(string.Empty))
                    RebuildList(_EpisodesList[(string)dud_YearAired.SelectedItem]);
                else
                {
                    cmbx_Episodes.Items.Clear();
                    foreach (string s in _EpisodesList[(string)dud_YearAired.SelectedItem])
                        if (Regex.IsMatch(s, "^" + cmbx_Episodes.Text, RegexOptions.IgnoreCase))
                            cmbx_Episodes.Items.Add(s);
                }
                cmbx_Episodes.SelectionStart = cmbx_Episodes.Text.Length;
            }
        }

        private void btn_commit_Click(object sender, EventArgs e)
        {
            EpisodeDisplay.SubItems[1].Text = TB_Output.Text;

            _episodeFiles[LBL_Original.Text]
                = new KeyValuePair<FileInfo, string>(_episodeFiles[LBL_Original.Text].Key, TB_Output.Text);
            RemoveEntry(this);
            this.Dispose();
        }
        private void btn_group_Click(object sender, EventArgs e)
        {
            ToggleGroup = !ToggleGroup;
        }

        #region numeric controls
        private void dud_YearAired_SelectedItemChanged(object sender, EventArgs e)
        {
            if (_EpisodesList != null && _EpisodesList.ContainsKey((string) dud_YearAired.SelectedItem))
                RebuildList(_EpisodesList[(string) dud_YearAired.SelectedItem]);

            if (SetGrouping != null)
                GetGrouping(tb_showtitle.Text, _ltvSeries, _EpisodesList, (int)nud_actSeason.Value,
                    (int)nud_actEpisode.Value, cmbx_ShowID.SelectedIndex, dud_YearAired.SelectedIndex, this);
        }

        private void nud_actSeason_ValueChanged(object sender, EventArgs e)
        {
            UpdateOutput();
            if (SetGrouping != null)
                GetGrouping(tb_showtitle.Text, _ltvSeries, _EpisodesList, (int)nud_actSeason.Value,
                    (int)nud_actEpisode.Value, cmbx_ShowID.SelectedIndex, dud_YearAired.SelectedIndex, this);
        }

        private void nud_actEpisode_ValueChanged(object sender, EventArgs e)
        {
            UpdateOutput();
            if (SetGrouping != null)
                GetGrouping(tb_showtitle.Text, _ltvSeries, _EpisodesList, (int)nud_actSeason.Value,
                    (int)nud_actEpisode.Value, cmbx_ShowID.SelectedIndex, dud_YearAired.SelectedIndex, this);
        }
        #endregion

        private void UpdateOutput()
        { 
            TB_Output.Text = string.Format("{0} S{1}E{2} - {3}"
                , tb_showtitle.Text
                , ((int)nud_actSeason.Value).ToString("00")
                , ((int)nud_actEpisode.Value).ToString("00")
                , _episodeName
            );
        }

        private void SetSeries(List<TvdbLib.Data.TvdbSeries> ltvseries)
        {
            _ltvSeries = ltvseries;
            cmbx_ShowID.Items.Clear();
            foreach (TvdbLib.Data.TvdbSeries series in _ltvSeries)
                cmbx_ShowID.Items.Add(series.SeriesName);
            if (_ltvSeries.Count > 0)
                cmbx_ShowID.SelectedText = _ltvSeries[0].SeriesName;      
        }
        private TvdbLib.Data.TvdbSeries GetSelectedSeries()
        {
            return _ltvSeries.Find(delegate(TvdbLib.Data.TvdbSeries ts)
            {
                if (ts.SeriesName == cmbx_ShowID.Text)
                    return true;
                return false;
            });
        }
        private void RebuildList(List<string> episodes)
        {
            cmbx_Episodes.Items.Clear();
            foreach (string episode in episodes)
                cmbx_Episodes.Items.Add(Regex.Replace(episode, "[0-9]{1,2}.?:.", ""));
        }

        private void BuildFirstAiredList(int dbseason)
        {
            dud_YearAired.Items.Clear();
            if (_EpisodesList != null)
            {
                List<string> episodeKeys = _EpisodesList.Keys.ToList();
                episodeKeys.Sort();
                foreach (string s in episodeKeys)
                    dud_YearAired.Items.Add(s);

                if (dud_YearAired.Items.Count > 0 && _EpisodesList.ContainsKey((string)dud_YearAired.Items[dbseason]))
                {
                    dud_YearAired.SelectedIndex = dbseason;
                    RebuildList(_EpisodesList[(string)dud_YearAired.SelectedItem]);
                }
            }
        }
    }
}
