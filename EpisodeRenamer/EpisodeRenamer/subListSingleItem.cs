using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CustomSortTitles
{
    public partial class subListSingleItem : UserControl, IComparable
    {
        public enum Operation { Up, Down };
        public bool Selected { get; set; }

        public delegate void MoveFunction(subListSingleItem item, Operation toDo);
        public MoveFunction runOperation;

        public delegate void SelectedItem(subListSingleItem item);
        public SelectedItem SetSelection;

        public delegate void SetHover(subListSingleItem item, bool check);
        public SetHover SetHovered;

        public int Index 
        { 
            get 
            { return int.Parse(LBL_Index.Text); }

            set
            { LBL_Index.Text = value.ToString(); }
        }

        public string Replacement { get {return TB_Title.Text;} }

        public subListSingleItem(int index, string sTitle, bool keepBtnVisible, bool readOnly)
        {
            InitializeComponent();
            Selected = false;

            if (!keepBtnVisible)
            {
                this.Controls.Remove(BTN_Up);
                this.Controls.Remove(BTN_Dn);
                this.TB_Title.Size = new Size(TB_Title.Size.Width + 70, TB_Title.Size.Height);
            }

            TB_Title.ReadOnly = readOnly;

            LBL_Index.Text = index.ToString();
            TB_Title.Text = sTitle;
        }

        public subListSingleItem(int index, string sTitle)
            : this(index, sTitle, true, false)
        {  }

        private void BTN_Up_Click(object sender, EventArgs e)
        {
            runOperation(this, Operation.Up);
        }

        private void BTN_Dn_Click(object sender, EventArgs e)
        {
            runOperation(this, Operation.Down);
        }

        public int CompareTo(object o)
        {
            if (!(o is subListSingleItem))
             throw new Exception("incorrect type or null");

            return this.Index - ( (subListSingleItem) o).Index;
        }

        private void subListSingleItem_Click(object sender, MouseEventArgs e)
        {
            Selected = !Selected;
            if (Selected)
                this.BackColor = Color.Olive;
            else
                this.BackColor = Color.White;

            if (SetSelection != null)
                SetSelection(this);
        }

        private void subListSingleItem_MouseEnter(object sender, EventArgs e)
        {
            if (SetHovered != null)
                SetHovered(this, true);
        }

        private void subListSingleItem_MouseLeave(object sender, EventArgs e)
        {
            if (SetHovered != null)
                SetHovered(this, false);
        }
    }
}
