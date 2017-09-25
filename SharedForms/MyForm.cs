using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SharedForms
{
    public partial class MyForm : Form
    {
        private bool m_aeroEnabled;
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;


        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
            (
                int nLeftRect,
                int nTopRect,
                int nRightRect,
                int nBottomRect,
                int nWidthEllipse,
                int nHeightEllipse
             );

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        //[Browsable(true)]
        //public string Title
        //{
        //    get { return this.labTitile.Text; }
        //    set { this.Text = this.labTitile.Text = value; }
        //}

        bool _hideCloseBtn;
        [Browsable(true)]
        public bool HideCloseBtn
        {
            get { return _hideCloseBtn; }
            set { _hideCloseBtn = value; this.picClose.Visible = !_hideCloseBtn; }
        }
        bool _hideMaxBtn;
        [Browsable(true)]
        public bool HideMaxBtn
        {
            get { return _hideMaxBtn; }
            set { _hideMaxBtn = value; this.picMax.Visible = !_hideMaxBtn; }
        }
        bool _hideMinBtn;
        [Browsable(true)]
        public bool HideMinBtn
        {
            get { return _hideMinBtn; }
            set { _hideMinBtn = value; this.picMin.Visible = !_hideMinBtn; }
        }

        [Browsable(true)]
        public Color FormBorderColor
        {
            get { return this.BackColor; }
            set { this.BackColor = value; }
        }

        public MyForm()
        {
            m_aeroEnabled = false; //窗体四边阴影
            InitializeComponent();
            this.MaximizedBounds = Screen.FromControl(this).WorkingArea;
            panelTitle.MouseDown += PanelTitle_MouseDown;
            picClose.Click += (obj, e) => { this.Close(); };
            picClose.SetButtonHoverLeave();
            picMax.Click += (obj, e) =>
            {
                if (WindowState == FormWindowState.Normal)
                {
                    this.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    this.WindowState = FormWindowState.Normal;
                }
            };
            picMax.SetButtonHoverLeave();
            picMin.SetButtonHoverLeave();
            picMin.Click += (obj, e) => { this.WindowState = FormWindowState.Minimized; };
            this.picClose.Visible = !_hideCloseBtn;
            this.picMax.Visible = !_hideMaxBtn;
            this.picMin.Visible = !_hideMinBtn;

            this.Load += (obj, e) =>
            {
                this.labTitile.Text = this.Text;
            };
            this.panelTitle.DoubleClick += (obj, e) =>
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    this.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    this.WindowState = FormWindowState.Normal;
                }
            };
        }

      

        private void PanelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 1)
                {
                    ReleaseCapture(); //释放鼠标捕捉
                    SendMessage(Handle, 0xA1, 0x02, 0);
                }
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();

                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;

                return cp;
            }
        }

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        const int CS_DROPSHADOW = 0x20000;
        //        CreateParams cp = base.CreateParams;
        //        cp.ClassStyle |= CS_DROPSHADOW;
        //        return cp;
        //    }
        //}


        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }



        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 1,
                            rightWidth = 1,
                            topHeight = 1
                        };
                        DwmExtendFrameIntoClientArea(this.Handle, ref margins);

                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);
            //全窗体点击：最大化、最小化、移动窗体、双击最大化

            //if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)
            //    m.Result = (IntPtr)HTCAPTION;

        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);

        //    Graphics g = e.Graphics;
        //    Rectangle rect = new Rectangle(new Point(0, 0), new Size(this.Width - 1, this.Height - 1));
        //    Pen pen = new Pen(Color.Black);

        //    g.DrawRectangle(pen, rect);
        //}



    }
}
