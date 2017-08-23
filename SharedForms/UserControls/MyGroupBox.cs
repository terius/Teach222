using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SharedForms
{
    //public partial class MyGroupBox : UserControl
    //{
    //    public MyGroupBox()
    //    {
    //        InitializeComponent();
    //    }
    //}

    public partial class MyGroupBox : GroupBox
    {
        private string _Text = "";
        public MyGroupBox()
        {
          //  InitializeComponent();
            //set the base text to empty 
            //base class will draw empty string
            //in such way we see only text what we draw
            base.Text = "";
           
        }



        //create a new property a
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue("GroupBoxText")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new string Text
        {
            get
            {

                return _Text;
            }
            set
            {

                _Text = value;
                this.Invalidate();
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {

            //first let the base class to draw the control 
            base.OnPaint(e);

            var g = e.Graphics;



            var box = this;
            // Brush textBrush = new SolidBrush(this.ForeColor);
            Brush borderBrush = new SolidBrush(this.ForeColor);
            Pen borderPen = new Pen(borderBrush);
            SizeF strSize = g.MeasureString(box.Text, box.Font);
            Rectangle rect = new Rectangle(box.ClientRectangle.X,
                                           box.ClientRectangle.Y + (int)(strSize.Height / 2),
                                           box.ClientRectangle.Width - 1,
                                           box.ClientRectangle.Height - (int)(strSize.Height / 2) - 1);
            g.Clear(this.BackColor);
            // g.DrawString(box.Text, box.Font, textBrush, box.Padding.Left, 0);
            // Drawing Border
            //Left
            g.DrawLine(borderPen, rect.Location, new Point(rect.X, rect.Y + rect.Height));
            //Right
            g.DrawLine(borderPen, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height));
            //Bottom
            g.DrawLine(borderPen, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));
            //Top1
            //   g.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + box.Padding.Left, rect.Y));
            //Top2
            // g.DrawLine(borderPen, new Point(rect.X + box.Padding.Left + (int)(strSize.Width), rect.Y), new Point(rect.X + rect.Width, rect.Y));
            g.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + rect.Width, rect.Y));

            //create a brush with fore color
            SolidBrush colorBrush = new SolidBrush(this.ForeColor);
            //create a brush with back color
            var backColor = new SolidBrush(this.BackColor);
            //measure the text size
            //   var size = TextRenderer.MeasureText(this.Text, this.Font);
            // evaluate the postiong of text from left;
            var left = (this.Width - strSize.Width) / 2;
            //draw a fill rectangle in order to remove the border
            e.Graphics.FillRectangle(backColor, left, 0, strSize.Width, strSize.Height);
            //draw the text Now
            e.Graphics.DrawString(this.Text, this.Font, colorBrush, new PointF(left, 0));
           // this.Text = DateTime.Now.Ticks.ToString();

        }
    }
}
