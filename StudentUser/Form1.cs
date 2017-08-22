using DevExpress.XtraEditors;
using Model;
using SharedForms;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace StudentUser
{
    public partial class Form1 : MyForm

    {

        public Form1()
        {
            InitializeComponent();
        }

        //引入API函数

        [DllImport("user32 ")]//这个是调用windows的系统锁定
        public static extern bool LockWorkStation();

        [DllImport("user32.dll")]
        static extern void BlockInput(bool Block);



        private void lockTaskmgr()//锁定任务管理器

        {

            FileStream fs =

                new FileStream(Environment.ExpandEnvironmentVariables(

                    "%windir%\\system32\\taskmgr.exe"), FileMode.Open);

            //byte[] Mybyte = new byte[(int)MyFs.Length];

            //MyFs.Write(Mybyte, 0, (int)MyFs.Length);

            //MyFs.Close();

            //用文件流打开任务管理器应用程序而不关闭文件流就会阻止打开任务管理器

        }



        private void lockAll()

        {

            BlockInput(true);//锁定鼠标及键盘

        }



        private void Form1_Load(object sender, EventArgs e)

        {

            //this.lockAll();



        }



        private void btnUnlock_Click(object sender, EventArgs e)

        {

            //if (txtPwd.Text == "19880210")

            //{

            //    BlockInput(false);

            //    Application.Exit();

            //}

            //else

            //{

            //    MessageBox.Show("密码错误！", "消息",

            //        MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    txtPwd.Text = "";

            //    txtPwd.Focus();

            //}

        }



        private void Form1_Load_1(object sender, EventArgs e)
        {
            //  this.panel1.Height = 2000;
            //  sms2 sms;
            // for (int i = 0; i < 10; i++)
            //  {
            //sms2 sms = new sms2(this.Width);
            //sms.Location = new Point(this.Width - sms.Width - 20, 0);
            //this.panel1.Controls.Add(sms);


            //sms2 sms22 = new sms2(this.Width);
            //sms22.Location = new Point(0, sms22.Height + 20);
            //this.panel1.Controls.Add(sms22);

            //sms2 sms23 = new sms2(this.Width);
            //sms23.Location = new Point(0, sms22.Location.Y + sms22.Height + 20);
            //this.panel1.Controls.Add(sms23);

            // }
            //  rightSJ.Location = new System.Drawing.Point(sms.Location.X + sms.Width - 2, sms.Location.Y + (sms.Height - rightSJ.Height) / 2);

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Point[] points2 = { new Point(this.Width-50, this.Height / 2 - 50),
            //    new Point(this.Width-50, this.Height / 2 + 50), new Point(this.Width, this.Height/2) };
            //e.Graphics.FillPolygon(new SolidBrush(Color.Green), points2);
        }

        bool isMySelf;
        private void button1_Click(object sender, EventArgs e)
        {
            ChatMessage message = new ChatMessage();
            message.Message = textBox1.Text.Trim();
            message.MessageType = Common.MessageType.String;
            message.ReceieveUserName = "d001";
            message.SendDisplayName = "王菲";
            message.SendTime = DateTime.Now;
            message.SendUserName = "teach001";
            message.UserType = Common.ClientRole.Teacher;
            message.ChatType = Common.ChatType.PrivateChat;
            if (isMySelf)
            {
                isMySelf = false;
                message.UserType = Common.ClientRole.Student;

            }
            else
            {
                isMySelf = true;
                message.UserType = Common.ClientRole.Teacher;
            }
            sms2 sms = new sms2(message, isMySelf);

            sms.Location = GetNewPoint(panel1, sms.Width, isMySelf);
            panel1.Controls.Add(sms);
            this.Text = panel1.Width.ToString() + "    " + panel1.DisplayRectangle.Width.ToString();

        }

        private Point GetNewPoint(Control panel, int smsWidth, bool isMySelf)
        {

            if (isMySelf)
            {
                if (panel.Controls.Count == 0)
                {
                    return new Point(panel.Width - smsWidth - 20, 0);
                }
                else
                {
                    var control = panel.Controls[panel.Controls.Count - 1];
                    return new Point(panel.Width - smsWidth - 20, control.Location.Y + control.Height + 20);
                }
            }
            else
            {
                if (panel.Controls.Count == 0)
                {
                    return new Point(0, 0);
                }
                else
                {
                    var control = panel.Controls[panel.Controls.Count - 1];
                    return new Point(0, control.Location.Y + control.Height + 20);
                }
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            // var control = panel1.Controls[panel1.Controls.Count - 1];
            //  MessageBox.Show("width:" + control.Width + "   height:" + control.Height);
        }

        private void panel1_ControlAdded(object sender, ControlEventArgs e)
        {
            this.panel1.ScrollControlIntoView(e.Control);
          //  panel1.VerticalScroll.Value = panel1.VerticalScroll.Maximum;
        }

        private void panel1_Resize(object sender, EventArgs e)
        {

        }
    }
}
