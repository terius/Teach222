using System.Drawing;
using System.Windows.Forms;

namespace StudentUser.Controls
{
    public partial class CustomTextBox : TextBox
    {
        public CustomTextBox()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor |
               ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.AllPaintingInWmPaint |
               ControlStyles.ResizeRedraw |
               ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;
        }
    }
}
