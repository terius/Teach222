using Common;
using System.Drawing;
using System.Windows.Forms;

namespace SharedForms
{
    public partial class ChatTypePanel : Panel
    {
        Rectangle rectArea;
        Image headIcon = Resource1.私聊;
        SolidBrush brush = new SolidBrush(Color.FromArgb(55, 152, 249));
        Font titleFont = new Font("微软雅黑", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
        Color defaultBackColor = Color.FromArgb(250, 250, 250);
        Color defaultSelectColor = Color.FromArgb(235, 235, 236);
        //  ChatType _chatType;
        string title;
        public ChatType ChatType { get; set; }
        public bool Forbit { get; set; }
        public bool IsSelected { get; set; }
        
        public ChatTypePanel()
        {
            // _chatType = ChatType;

            InitializeComponent();

            SetChatPanelHover();
            this.MouseClick += ChatTypePanel_MouseClick;
            this.Cursor = Cursors.Hand;
            this.BackColor = Color.White;

        }

        private void ChatTypePanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.BackColor = defaultSelectColor;
                IsSelected = true;
            }
           
        }

        private void SetChatPanelHover()
        {
            this.MouseEnter += (sender, e) => ((Control)sender).BackColor = defaultSelectColor;
            this.MouseLeave += (sender, e) =>
            {
                if (!IsSelected)
                {
                    ((Control)sender).BackColor = defaultBackColor;
                }
            };

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            switch (ChatType)
            {
                case ChatType.PrivateChat:
                    headIcon = Resource1.私聊;
                    title = "私聊";
                    break;
                case ChatType.GroupChat:
                    headIcon = Resource1.所有人;
                    title = "所有人";
                    break;
                case ChatType.TeamChat:
                    headIcon = Resource1.群聊;
                    title = "群聊";
                    break;
                default:
                    break;
            }
            if (Forbit)
            {
                headIcon = Resource1.禁止;
            }
            var g = e.Graphics;
            int fontHeight = titleFont.Height;
            rectArea = new Rectangle(10, (this.Height - 40) / 2, 40, 40);
            g.DrawImage(headIcon, rectArea);
            g.DrawString(title, titleFont, brush, 60, (this.Height - fontHeight) / 2);
            //  g.FillRectangle(brush, ClientRectangle);
            //  g.DrawLine(Pens.Red, 0, ClientSize.Height - 1, ClientSize.Width - 1, ClientSize.Height - 1);

        }
    }
}
