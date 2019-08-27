using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M120Projekt
{
    public partial class PingPongControl : UserControl
    {
        private int _xRichtung = 5;
        private int _yRichtung = 2;

        public PingPongControl()
        {
            InitializeComponent();
        }

        private void DialogButton_Load(object sender, EventArgs e)
        {

        }

        private void TmrGame_Tick(object sender, EventArgs e)
        {
            pctBall.Location = new Point(pctBall.Location.X + _xRichtung, pctBall.Location.Y + _yRichtung);

            if (pctBall.Location.Y >= pnlField.Height - pctBall.Width) _yRichtung = -_yRichtung;

            if (pctBall.Location.X >= pnlField.Width - pctBall.Width) _xRichtung = -_xRichtung;

            if (pctBall.Location.X <= 0) _xRichtung = 5;

            if (pctBall.Location.Y <= 0) _yRichtung = 2;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            tmrGame.Enabled = true;
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            tmrGame.Enabled = false;
        }
    }
}
