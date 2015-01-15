using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CustomSortTitles
{
    public partial class CustomTitleSorter : UserControl
    {
        public CustomTitleSorter()
        {
            InitializeComponent();
        }

        private List<subListSingleItem> _lslsiShuffle = new List<subListSingleItem>();
        private List<string> _lsRebuildList = new List<string>();
        private ListView _lvAugmentList;
        private int iStart,
                    iEnd;

        private subListSingleItem ssiDropTitle = null;
        private bool bSelectionMode = false;

        public void InitializeExtraComponents(ListView inputList, int iStart, int iEnd)
        {
            _lvAugmentList = inputList;
            this.iStart = iStart;
            this.iEnd = iEnd;

            if (iStart > iEnd)
            {
                int iSwap = iEnd;
                iEnd = iStart;
                iStart = iSwap;
            }

            List<subListSingleItem> lslsiOrigin = new List<subListSingleItem>();
            List<subListSingleItem> lslsiSet = new List<subListSingleItem>();

            int iCount = 1;
            for (int i = iStart; i <= iEnd; i++)
            {
                ListViewItem lviItem = inputList.Items[i];
               
                subListSingleItem slsiTempOrigin = new subListSingleItem(iCount, lviItem.SubItems[0].Text, false, true);               

                Regex r = new Regex("^.*[0-9] - ");
                string sReplace = r.Replace(lviItem.SubItems[1].Text, "");
                _lsRebuildList.Add(lviItem.SubItems[1].Text.Replace(sReplace.Equals(string.Empty) ? lviItem.SubItems[1].Text : sReplace, ""));
                subListSingleItem slsiTempShuffle = new subListSingleItem(iCount, sReplace);

                subListSingleItem slsiTempSet = new subListSingleItem(iCount, lviItem.SubItems[1].Text, false, true);

                slsiTempShuffle.Location = new Point(0, iCount * (slsiTempShuffle.Size.Height + 5));
                slsiTempShuffle.Anchor = ((AnchorStyles)((AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top)));

                slsiTempSet.Location = new Point(0, iCount * (slsiTempShuffle.Size.Height + 5));
                slsiTempSet.Anchor = ((AnchorStyles)((AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top)));

                slsiTempOrigin.Location = new Point(0, iCount * (slsiTempShuffle.Size.Height + 5));
                slsiTempOrigin.Anchor = ((AnchorStyles)((AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top)));

                slsiTempShuffle.runOperation += new subListSingleItem.MoveFunction(DealWithOperations);
                slsiTempShuffle.SetSelection += new subListSingleItem.SelectedItem(DetermineDropPoint);
                slsiTempShuffle.SetHovered += new subListSingleItem.SetHover(Determinehover);

                _lslsiShuffle.Add(slsiTempShuffle);

                lslsiOrigin.Add(slsiTempOrigin);
                lslsiSet.Add(slsiTempSet);
                iCount++;
            }

            splitContainer1.Panel1.Controls.AddRange(lslsiOrigin.ToArray());
            splitContainer2.Panel1.Controls.AddRange(lslsiSet.ToArray());

            ResetContainer();
            this.Refresh();
        }

        public ListView GetAugmentList()
        {
            for (int i = iStart; i <= iEnd; i++)
                _lvAugmentList.Items[i].SubItems[_lvAugmentList.Items[i].SubItems.Count -1].Text = 
                    _lsRebuildList[i - iStart] + _lslsiShuffle[i - iStart].Replacement;

            return _lvAugmentList;
        }

        private void ResetContainer()
        {
            for (int i = splitContainer2.Panel2.Controls.Count - 1; i > -1; i--)               
                if (splitContainer2.Panel2.Controls[i] is subListSingleItem)
                    splitContainer2.Controls.Remove(splitContainer2.Panel2.Controls[i]);

            _lslsiShuffle.Sort();
            for (int i = 0; i < _lslsiShuffle.Count; i++)
            {
                _lslsiShuffle[i].Location = new Point(0, (i + 1) * (_lslsiShuffle[i].Size.Height + 5));
                splitContainer2.Panel2.Controls.Add(_lslsiShuffle[i]);
            }
        }

        private void DealWithOperations(subListSingleItem swapper, subListSingleItem.Operation toDo)
        {
            int i = _lslsiShuffle.IndexOf(swapper);

            if (toDo.Equals(subListSingleItem.Operation.Down))
            {
                if (i.Equals(_lslsiShuffle.Count - 1))
                    return;

                subListSingleItem swappee = _lslsiShuffle[i + 1];

                swappee.Index = swappee.Index - 1;
                swapper.Index = swapper.Index + 1;

                _lslsiShuffle[i] = swappee;
                _lslsiShuffle[i + 1] = swapper; 
            }

            else if (toDo.Equals(subListSingleItem.Operation.Up))
            {
                if (i.Equals(0))
                    return;

                subListSingleItem swappee = _lslsiShuffle[i - 1];

                swappee.Index = swappee.Index + 1;
                swapper.Index = swapper.Index - 1;

                _lslsiShuffle[i] = swappee;
                _lslsiShuffle[i - 1] = swapper; 
            }

            ResetContainer();
            this.Refresh();
        }

        private void DetermineDropPoint(subListSingleItem dropper)
        {
            if (!bSelectionMode)
            {
                ssiDropTitle = dropper;
                bSelectionMode = true;
            }
            else
            {
                ssiDropTitle.Selected = dropper.Selected = false;
                ssiDropTitle.BackColor = dropper.BackColor = Color.White;

                int i = _lslsiShuffle.IndexOf(ssiDropTitle);
                int j = _lslsiShuffle.IndexOf(dropper);

                int iIndexSwap = ssiDropTitle.Index;
                ssiDropTitle.Index = dropper.Index;
                dropper.Index = iIndexSwap;

                _lslsiShuffle[i] = dropper;
                _lslsiShuffle[j] = ssiDropTitle;

                bSelectionMode = false;
                ResetContainer();
            }
        }

        private void Determinehover(subListSingleItem item, bool hovered)
        {
            if (bSelectionMode)
            {
                if (item.Selected)
                    item.BackColor = Color.Olive;
                else if (hovered)
                    item.BackColor = Color.LightSeaGreen;
                else
                    item.BackColor = Color.White;

                this.Refresh();
            }
        }
    }
}
