using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net;
using TvdbLib;
using System.Windows.Forms;
using EpisodeTitleParser;

namespace EpisodeRenamer
{
    public partial class EpisodeNameConverter : Form
    {
        private Thread _tGetWikiList,
                       _tGetMediaInfo;

        private ManualCorrectionWindows _mcwManualCorrections =  null;


        private Dictionary<string, KeyValuePair<FileInfo, string>> _dicSourceFiles = new Dictionary<string, KeyValuePair<FileInfo, string>>();
        private List<KeyValuePair<int, string>> _EpisodesList = new List<KeyValuePair<int, string>>();

        private ListViewItem lviListViewSwapA = null;

        private List<string> lsFilter = new List<string>(new string[] { ".wmv", ".mkv", ".avi", ".mp4", ".divx", ".xvid", ".mpeg", ".h264", ".x264", ".ts" });

        private EpisodeSeriesBuilder _esbDatabase;

        private delegate void delVoidDicStrLStr(Dictionary<string, List<string>> d);
        private delegate void delVoidStrStrStrDSK(string s1, string s2, string s3, 
            Dictionary<string, KeyValuePair<FileInfo,string>> dsk);
        private delegate void delVoidStr(string s);

        private event delVoidDicStrLStr _UpdateProgress;
        private event delVoidStrStrStrDSK _UpdateOutput;
        private event delVoidStr _UpdateStatus;

        private int _iLastSelected = -1;

        public EpisodeNameConverter()
        {
            InitializeComponent();

            listView1.Columns[0].Width = 200;
            listView1.Columns[1].Width = 200;

            listView1.HideSelection = false;

            _UpdateProgress = UpdateProgress;
            _UpdateOutput = UpdateOutputFiles;
            _UpdateStatus = UpdateStatus;

            foreach (string s in lsFilter)
                TB_Filters.Text += s + "|";
            TB_Filters.Text = TB_Filters.Text.Remove(TB_Filters.Text.Length - 1, 1);

            _esbDatabase = new EpisodeSeriesBuilder();
            _tGetMediaInfo = null;
        }

        private void EpisodeNameConverter_Load(object sender, EventArgs e)
        {
            TT_Help.SetToolTip(listView1, "* Populated list of episodes to rename *\n\n" +  
                                          "*** Instructions ***\n" + 
                                          "- Left Click Item ('Previous Tag' column) - Manual rename new title.\n" +
                                          "- Right Click Item ('Previous Tag' column) - Title swap between two selected episodes.\n" +
                                          "       1. Select episode (highlighted yellow)\n" +
                                          "       2. Select additional episode (highlights will disappear) and a submenu will appear.\n" +
                                          "          this submenu will allow you to reorder tiles in the range between the two selections.\n" +
                                          "- Check Item ('Previous Tag' column) - Select specific episodes to rename.\n" +
                                          "- Enables the 'Rename Media' functions.");

            TT_Help.SetToolTip(TB_Filters, "* Specified file extensions to search directory for *\n\n" +
                                           "*** Instructions ***\n" +
                                           "- All formatted as a period (.) followed by 2-4 extension letters.\n" +
                                           "  Uses a bar seperator (|) to indicate multiple extnsion checks.\n" +
                                           "            ex.  '.XX|.YYY|.ZZZZ'\n" + 
                                           "- Press 'Set' button to apply search additions/deductions.");
            
            TT_Help.SetToolTip(BTN_SetFilters, "* Specified file extensions to search directory for *\n\n" +
                                               "*** Instructions ***\n" +
                                               "- All formatted as a period (.) followed by 2-4 extension letters.\n" +
                                               "  Uses a bar seperator (|) to indicate multiple extnsion checks.\n" +
                                               "            ex.  '.XX|.YYY|.ZZZZ'\n" +
                                               "- Press 'Set' button to apply search additions/deductions.");

            TT_Help.SetToolTip(TB_GetSeries, "* Series name to search database for *\n\n" +
                                             "*** Instructions ***\n" +
                                             "- Press 'Get Episode List' button to get show reference.\n" + 
                                             "  Does not conflict with 'Select Media To Rename'.");

            TT_Help.SetToolTip(BTN_WikiPage, "* Series name to search database for *\n\n" +
                                             "*** Instructions ***\n" +
                                             "- Press 'Get Episode List' button to get show reference.\n" +
                                             "  Does not conflict with 'Select Media To Rename'.");

            TT_Help.SetToolTip(BTN_GetMedia, "* Populates a list of all episodes found in a select directory * \n\n" +
                                             "*** Instructions ***\n" +
                                             "- Select a folder directory to search database for.\n" +
                                             "  Displays the old title and a newly generated title to rename too.\n" +
                                             "- Does NOT actually rename the episodes");

            TT_Help.SetToolTip(BTN_RenameMedia, "* Renames all episodes in the directory based off which items are checked * \n\n" +
                                                "*** Instructions ***\n" +
                                                "- Check items (or select all) to choose for renaming.\n" +
                                                "  Press button to rename all selected files.\n" +
                                                "- THIS IS NOT REVERSABLE! Make sure everything looks correct.");

            TT_Help.SetToolTip(btn_ShowManual, "* Re-opens 'Manual Entry' panel on click, opens only if needed *"); 
        }

        #region GetFiles

        private void BTN_GetMedia_Click(object sender, EventArgs e)
        {
            if (_tGetMediaInfo != null)
            {
                _tGetMediaInfo.Abort();
                BTN_SetFilters.Enabled = true;
                BTN_GetMedia.Text = "Select Media";
                LBL_Status.Text = "Waiting";
                _tGetMediaInfo = null;
                return;
            }

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {          
                BTN_SetFilters.Enabled = false;
                BTN_GetMedia.Text = "Cancel";
                LBL_Status.Text = "Pending";

                lviListViewSwapA = null;

                _dicSourceFiles.Clear();
                listView1.Items.Clear();
                if (_mcwManualCorrections != null)
                    _mcwManualCorrections.ClearAllFields();

                _tGetMediaInfo = new Thread(delegate() { CreateDirectoryInfo(new DirectoryInfo(folderBrowserDialog1.SelectedPath)); } );
                _tGetMediaInfo.IsBackground = true;
                _tGetMediaInfo.Start();
            }
        }

        private void btn_Manual_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView1.CheckedItems)
                ManualEntry(lvi.SubItems[0].Text, lvi.Text, lvi, _dicSourceFiles);
            _mcwManualCorrections.SetPanel();
        }

        private void CreateDirectoryInfo(DirectoryInfo directoryInfo)
        {
            string sTemp = string.Empty;
            TitleParser temp = new TitleParser();
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var file in directoryInfo.GetFiles("*.*", SearchOption.AllDirectories).Where(s => lsFilter.Contains(s.Extension)))
            {
                // manipulate data
                EpisodeInfo eiTemp = temp.GetEpisodeInfo(file.Name);
                string series = "";
                try { sTemp = _esbDatabase.EpisodeNameReturn(eiTemp, SeriesSelect); }
                catch (Exception) { sTemp = "NOTFOUND";  }
               
                Invoke(_UpdateOutput, file.Name, sTemp, series, _dicSourceFiles);
                _dicSourceFiles.Add(file.Name, new KeyValuePair<FileInfo, string>(file, sTemp));
            }

            Invoke(_UpdateStatus, "Complete");
        }

        private void UpdateOutputFiles(string sI, string sO, string sA, 
            Dictionary<string, KeyValuePair<FileInfo, string>> dskE)
        {
            ListViewItem lvi = new ListViewItem(sI);
            lvi.SubItems.Add(sO);
            if (sO.Equals("NOTFOUND"))
                ManualEntry(sA, sI, lvi, dskE);
            listView1.Items.Add(lvi);
            alternateRowColor();
        }

        private void ManualEntry(string showTitle, string originalTitle, ListViewItem listindex,
            Dictionary<string, KeyValuePair<FileInfo, string>> dskE)
        {
            if (_mcwManualCorrections == null)
            {
                _mcwManualCorrections = new ManualCorrectionWindows();
                _mcwManualCorrections.Location = GetWorkingScreenArea(this.Size, this.Location,
                    _mcwManualCorrections.Size, "DN");
                _mcwManualCorrections.Show();
            }
            _mcwManualCorrections.AddEntry(showTitle, originalTitle, listindex, dskE);
        }

        private void UpdateStatus(string s)
        {
            LBL_Status.Text = s;
            BTN_SetFilters.Enabled = true;
            BTN_GetMedia.Text = "Select Media";
            _tGetMediaInfo = null;

            if (_mcwManualCorrections != null)
            {
                _mcwManualCorrections.SetPanel();
                _mcwManualCorrections.Show();
            }
        }

        #endregion

        #region Find_Episode_Names_From_TvDb

        private void BTN_WikiPage_Click(object sender, EventArgs e)
        {
            treeView.Nodes.Clear();
            if (!TB_GetSeries.Text.Trim().Equals(string.Empty))
            {
                lock (_EpisodesList)
                    _EpisodesList.Clear();

                _tGetWikiList = new Thread(delegate() {
                    Dictionary<string, List<string>> episodes 
                        = _esbDatabase.SeriesEpisodes(TB_GetSeries.Text.Trim(), SeriesSelect);
                    if (episodes == null)
                    {
                        episodes = new Dictionary<string, List<string>>();
                        episodes.Add("No series by that name", new List<string>());
                    }
                    Invoke(_UpdateProgress, episodes);

                });
                _tGetWikiList.IsBackground = true;
                _tGetWikiList.Start();
            }
        }

        private void UpdateProgress(Dictionary<string, List<string>> items)
        {
            treeView.Nodes.Clear();
            foreach (string seas in items.Keys)
            {
                List<TreeNode> episodes = new List<TreeNode>();
                foreach (string ep in items[seas])
                    episodes.Add(new TreeNode(ep));

                treeView.Nodes.Add(new TreeNode(seas, episodes.ToArray()));
            }
        }

        private int CompareKeys(KeyValuePair<int, string> a, KeyValuePair<int, string> b)
        {
            return a.Key.CompareTo(b.Key);
        }

        /// <summary>
        /// Takes string options for locations around main window
        ///     right : "RT"
        ///     left  : "LT"
        ///     Up    : "UP"
        ///     down  : "DN"
        /// </summary>
        /// <param name="sParent"></param>
        /// <param name="pParent"></param>
        /// <param name="sSecondary"></param>
        /// <param name="sRelativeLoc"></param>
        /// <returns></returns>
        private Point GetWorkingScreenArea(Size sParent, Point pParent, Size sSecondary, string sRelativeLoc)
        {
            Rectangle rScreenDimensions = Screen.PrimaryScreen.Bounds;
            Point pDefault = new Point(pParent.X + 5, pParent.Y + 5);

            if (this.WindowState == FormWindowState.Maximized)
                return pDefault;

            switch(sRelativeLoc.ToUpper())
            {
                case "UP":
                    return (pParent.Y - sSecondary.Height > rScreenDimensions.Top ? 
                        new Point(pParent.X, pParent.Y - sSecondary.Height) :
                        pDefault);

                case "LT":
                    return (pParent.X - sSecondary.Width > rScreenDimensions.Left ?
                        new Point(pParent.X - sSecondary.Width, pParent.Y) :
                        pDefault);

                case "DN":
                    return (pParent.Y + sParent.Height + sSecondary.Height < rScreenDimensions.Bottom ?
                        new Point(pParent.X, pParent.Y + sParent.Height) :
                        pDefault);

                case "RT":
                    return (pParent.X + sParent.Width + sSecondary.Width < rScreenDimensions.Right ?
                        new Point(pParent.X + sParent.Width, pParent.Y) :
                        pDefault);

                default:
                    return pDefault;
            }
        }
        #endregion

        #region Determine Series

        private KeyValuePair<int, bool> SeriesSelect(List<TvdbLib.Data.TvdbSeries> lTvSeries, int seriesId)
        {
            SeriesSearchFilter ssf = new SeriesSearchFilter(lTvSeries, seriesId);
            ssf.Location = GetWorkingScreenArea(this.Size, this.Location, ssf.Size, "RT");

            if (ssf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return new KeyValuePair<int, bool>(ssf.SeriesIndex, ssf.SeriesMem);
            else
                return new KeyValuePair<int, bool>(lTvSeries[0].Id, false);
        }

        #endregion

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
                listView1.Items[i].Checked = checkBox1.Checked;
        }

        private void BTN_RenameMedia_Click(object sender, EventArgs e)
        {
            List<ListViewItem> removal = new List<ListViewItem>();
            LBL_Status.Text = "Pending";
            foreach (ListViewItem lvi in listView1.CheckedItems)
            {
                try
                {
                    KeyValuePair<FileInfo, string> kvp = _dicSourceFiles[lvi.Text];
                    string regexSearch = string.Format("{0}{1}", new string(System.IO.Path.GetInvalidFileNameChars()), new string(System.IO.Path.GetInvalidPathChars()));
                    Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
                    string sFileName = r.Replace(kvp.Value, "");
                    System.IO.File.Move(kvp.Key.FullName, Path.Combine(kvp.Key.DirectoryName, sFileName + kvp.Key.Extension));
                    removal.Add(lvi);
                }
                catch (NotSupportedException ex)
                {
                    Console.Write(ex.Message);
                }
                catch (AccessViolationException ex)
                {
                    Console.Write(ex.Message);
                }

                catch (FileNotFoundException ex)
                {
                    Console.Write(ex.Message);
                }
                catch (IOException ex)
                {
                    Console.Write(ex.Message);
                }
            }
            for (int i = removal.Count - 1; i >= 0; i--)
                listView1.Items.Remove(removal[i]);

            LBL_Status.Text = "Rename Done";
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (listView1.CheckedItems.Count > 0)
            {
                BTN_RenameMedia.Enabled = true;
                BTN_Manually.Enabled = true;
            }
            else
            {
                BTN_RenameMedia.Enabled = false;
                BTN_Manually.Enabled = false;
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                ManualChangeTitle();
            else if (e.Button == MouseButtons.Right)
                ManualTitleSwap();

            if (listView1.SelectedIndices.Count > 0)
                for (int i = 0; i < listView1.SelectedIndices.Count; i++)
                    listView1.Items[listView1.SelectedIndices[i]].Selected = false;
        }

        private void ManualChangeTitle()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem lvi = listView1.SelectedItems[listView1.SelectedItems.Count - 1];

                if (lvi.SubItems.Count > 0)
                {
                    ManualEdit me = new ManualEdit(lvi);
                    me.Location = GetWorkingScreenArea(this.Size, this.Location, me.Size, "DN");
                    if (me.ShowDialog() == DialogResult.OK)
                        _dicSourceFiles[lvi.Text] =
                            new KeyValuePair<FileInfo, string>(_dicSourceFiles[lvi.Text].Key, lvi.SubItems[lvi.SubItems.Count - 1].Text);
                }
            }
        }

        private void ManualTitleSwap()
        {
            if (lviListViewSwapA == null)
            {
                lviListViewSwapA = listView1.SelectedItems[listView1.SelectedIndices.Count - 1];
                lviListViewSwapA.ForeColor = Color.Red;
            }
            else
            {
                ListViewItem lviListViewSwapB = listView1.SelectedItems[listView1.SelectedIndices.Count - 1];
                lviListViewSwapA.ForeColor = Color.Black;

                ShuffleTitlesWindow stf = new ShuffleTitlesWindow(listView1, lviListViewSwapA.Index, lviListViewSwapB.Index);
                stf.Location = GetWorkingScreenArea(this.Size, this.Location, stf.Size, "RT");

                if (stf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    listView1 = stf.AugmentedList;

                foreach (ListViewItem lvi in listView1.Items)
                {
                    KeyValuePair<FileInfo, string> kvpTemp = _dicSourceFiles[lvi.Text];
                    _dicSourceFiles[lvi.Text] = new KeyValuePair<FileInfo, string>(kvpTemp.Key, lvi.SubItems[lvi.SubItems.Count - 1].Text);
                }

                lviListViewSwapA = null;
            }
        }

        private string GetEditedString(string sUnconverted, string sSwap)
        {
            string sConverted = "";
            Regex reg = new Regex("^.+S[0-9][0-9]E[0-9][0-9] - ");

            if (reg.Match(sUnconverted).Success)
                sConverted = reg.Replace(sSwap, reg.Match(sUnconverted).Value);

            return sConverted;
        }

        #region Search_Filter_Features

        private void BTN_SetFilters_Click(object sender, EventArgs e)
        {
            SetSearchFilters();
        }

        private void TB_Filters_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SetSearchFilters();
        }

        private void TIM_MessageReset_Tick(object sender, EventArgs e)
        {
            if (lsFilter.Count == 10)
                LBL_SetFilters.Text = "Search Filters";
            else
                LBL_SetFilters.Text = "Search Filters(+)";
            TIM_MessageReset.Enabled = false;
        }

        private void SetSearchFilters()
        {
            Regex reg = new Regex("^[.][a-zA-Z0-9]{2,4}$");
            string[] saTemp = TB_Filters.Text.Split('|');
            foreach (string s in saTemp)
            {
                if (!reg.IsMatch(s))
                {
                    TIM_MessageReset.Enabled = true;
                    if (s != "")
                        LBL_SetFilters.Text = "Error on " + s;
                    else
                        LBL_SetFilters.Text = "Blank Entry";

                    TB_Filters.Text = "";
                    foreach (string st in lsFilter)
                        TB_Filters.Text += st + "|";
                    TB_Filters.Text = TB_Filters.Text.Remove(TB_Filters.Text.Length - 1, 1);

                    return;
                }
            }

            LBL_SetFilters.Text = "Search Filters(+)";
            lsFilter = new List<string>(saTemp);
        }

        #endregion

        private void EpisodeNameConverter_Resize(object sender, EventArgs e)
        {
            listView1.Columns[0].Width = listView1.Width / 2;
            listView1.Columns[1].Width = listView1.Width / 2;

            int iConstOffset = (listView1.Width / 2) - 5;
            BTN_GetMedia.Width = iConstOffset ;
            BTN_RenameMedia.Width = iConstOffset;
            LBL_Status.Width = iConstOffset;

            BTN_SetFilters.Location = new Point(listView1.Location.X + listView1.Width - BTN_SetFilters.Width, BTN_SetFilters.Location.Y);
            TB_Filters.Width = listView1.Width - (TB_Filters.Location.X + 29); 
        }

        private void btn_ShowManual_Click(object sender, EventArgs e)
        {
            if (_mcwManualCorrections != null)
                _mcwManualCorrections.Show();
        }

        private void alternateRowColor()
        {
            int index = listView1.Items.Count - 1;
            if (index % 2 == 1)
            {
                listView1.Items[index].BackColor = Color.LightGray;
                listView1.Items[index].UseItemStyleForSubItems = true;
            }
        }

        private void EpisodeNameConverter_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_tGetMediaInfo != null)
                _tGetMediaInfo.Abort();

            if (_tGetWikiList != null)
                _tGetWikiList.Abort();
        }
    }  
}
