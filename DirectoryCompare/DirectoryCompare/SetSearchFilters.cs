using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DirectoryCompare
{
    public class SetSearchFilters
    {
        public List<string> _lsFilter { get; private set; }
        private Timer _tTimeOut = new Timer();
        private TextBox _tbFilters;
        Label _lblMessages;

        public SetSearchFilters(TextBox TB_Filters, Label LBL_SetFilters)
        {
            _tbFilters = TB_Filters;
            _lblMessages = LBL_SetFilters;
            _tTimeOut.Interval = 5000;
            _tTimeOut.Tick += TIM_MessageReset_Tick;
            _tTimeOut.Enabled = true;
            _tTimeOut.Stop();

            _lsFilter = new List<string>(new string[] { ".wmv", ".mkv", ".avi", ".mp4", ".divx", ".xvid", ".mpeg", ".h264", ".x264", ".ts" });

            foreach (string s in _lsFilter)
                TB_Filters.Text += s + "|";
            TB_Filters.Text = TB_Filters.Text.Remove(TB_Filters.Text.Length - 1, 1);

            TB_Filters.KeyDown += new KeyEventHandler(TB_Filters_KeyDown);
        }

        void TB_Filters_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                SearchFilters();
        }

        #region Search_Filter_Features
        public void SearchFilters()
        {
            Regex reg = new Regex("^[.][a-zA-Z0-9]{2,4}$");
            string[] saTemp = _tbFilters.Text.Split('|');
            foreach (string s in saTemp)
            {
                if (!reg.IsMatch(s))
                {
                    if (s != "")
                        _lblMessages.Text = "Error on " + s;
                    else
                        _lblMessages.Text = "Blank Entry";

                    _tbFilters.Text = "";
                    foreach (string st in _lsFilter)
                        _tbFilters.Text += st + "|";
                    _tbFilters.Text = _tbFilters.Text.Remove(_tbFilters.Text.Length - 1, 1);

                    _tTimeOut.Start();
                    return;
                }
            }

            _lblMessages.Text = "Search Filters(+)";
            _lsFilter = new List<string>(saTemp);
        }

        private void TIM_MessageReset_Tick(object sender, EventArgs e)
        {
            if (_lsFilter.Count == 10)
                _lblMessages.Text = "Search Filters";
            else
                _lblMessages.Text = "Search Filters(+)";
            _tTimeOut.Stop();
        }
        #endregion
    }
}
