using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DirectoryCompare
{
    public partial class Loading : Form
    {
        private Timer tAnimation;

        private bool bKillForm;

        private float fSweep;
        private int iColorSwap;
        private Color[] caLoadColor;

        public Loading()
        {
            InitializeComponent();
            tAnimation = new Timer();
            tAnimation.Interval = 15;
            tAnimation.Tick += new EventHandler(tAnimation_Tick);
            tAnimation.Start();

            fSweep = 0;
            
            caLoadColor = new Color[] {Color.Black, Color.White};
            bKillForm = false;
        }

        public void KillForm()
        {
            bKillForm = true;
        }

        void tAnimation_Tick(object sender, EventArgs e)
        {
            if (bKillForm)
                this.Close();


            fSweep++;
            if (fSweep > 360)
            {
                iColorSwap = (++iColorSwap % caLoadColor.Length);
                fSweep = 0;
            }

            using (Graphics gr = CreateGraphics())
            {
                // create a context for the double-buffering
                BufferedGraphicsContext bgc = new BufferedGraphicsContext();
                BufferedGraphics bg = bgc.Allocate(gr, this.DisplayRectangle);

                // clear the back-buffer
                bg.Graphics.Clear(System.Drawing.Color.Gray);
                bg.Graphics.FillEllipse(new SolidBrush(caLoadColor[(iColorSwap + 1) % caLoadColor.Length]), 10, 10, 80, 80);

                bg.Graphics.FillPie(new SolidBrush(caLoadColor[iColorSwap]), 10, 10, 80, 80, -90, fSweep);

                bg.Render();
            }
        }
    }
}
