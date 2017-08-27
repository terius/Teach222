using System;
using System.Drawing;
using System.Windows.Forms;
using vlctest.Properties;

namespace vlctest
{
    public partial class PanelEx : UserControl
    {
        Rectangle rectArea;
        Image headIcon = Resources.学生;
        SolidBrush brush = new SolidBrush(Color.DarkGreen);
        Font titleFont = new Font("微软雅黑", 10F, FontStyle.Bold, GraphicsUnit.Point, 134);
        public PanelEx()
        {
            InitializeComponent();
            SetPanelHover(this);

        }

        private void SetPanelHover(Control control)
        {
            control.MouseEnter += (sender, e) => ((Control)sender).BackColor = Color.Red;
            control.MouseLeave += (sender, e) => ((Control)sender).BackColor = Color.FromArgb(255, 255, 255);
        }

        public void DrawTime()
        {
           // this.Invalidate();
            var g = this.CreateGraphics();
            g.DrawString(DateTime.Now.Ticks.ToString(), titleFont, Brushes.Black, 100, 0);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
   
            var g = e.Graphics;
            rectArea = new Rectangle(0, 0, 32, 32);
            g.DrawImage(headIcon, rectArea);

          //  g.FillRectangle(brush, ClientRectangle);
            g.DrawRectangle(Pens.Red, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
            DrawTime();
        }
    }
}
