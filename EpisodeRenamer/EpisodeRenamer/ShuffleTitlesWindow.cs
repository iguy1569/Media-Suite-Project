using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace EpisodeRenamer
{
    public partial class ShuffleTitlesWindow : Form
    {
        public ListView AugmentedList { get; private set; }
        public ShuffleTitlesWindow()
        {
            InitializeComponent();
        }

        public ShuffleTitlesWindow(ListView list, int iStart, int iEnd)
            :this()
        {
            customTitleSorter1.InitializeExtraComponents(list, iStart, iEnd);

            toolTip1.SetToolTip(this, "This window is used to rename seasons where the current order differs from the DB order.\n" + 
                                                    "   The 'Origional File Name' Column:\n" + 
                                                    "       - Indicates origional file name, read-only\n" +
                                                    "       - usful if origional file has title\n\n" +
                                                    "   The 'Mockup File Name' Column:\n" + 
                                                    "       - Indicates newly created database title, read-only\n" +
                                                    "       - used to make comparisions to first column\n\n" +
                                                    "   The 'Shuffle File Name' Column:\n" + 
                                                    "       - Indicates newly created database title, Full edit\n" +
                                                    "       - The Up/Down button will swap the title with the title of the one above or below it\n" + 
                                                    "       - Additional long swaps can be made by clicking on a title (which will highlight olive), \n" +
                                                    "         then clicking on a different title (which will highlight light green) \n" +
                                                    "       - used to make comparisions to first column\n\n");

            toolTip1.SetToolTip(BTN_Commit, "Proceed forward with newly orrdered titles.");

            toolTip1.SetToolTip(BTN_Cancel, "Proceed forward with titles that were previous.");
            toolTip1.Active = true;
        }

        private void BTN_Okay_Click(object sender, EventArgs e)
        {
            AugmentedList = customTitleSorter1.GetAugmentList();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void BTN_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}
