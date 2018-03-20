using Common;
using EasyScreenLiveLib;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace libEasyScreenLive
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        EasyScreen es;




        private void Form1_Load(object sender, System.EventArgs e)
        {
            es = EasyScreen.Init();
        }

        private void btnStartPush_Click(object sender, System.EventArgs e)
        {
            if (btnStartPush.Text == "开始推送")
            {
                es.StartPush("terius");
                btnStartPush.Text = "停止推送";
            }
            else
            {
                es.StopPush();
                btnStartPush.Text = "开始推送";
            }

        }

        private void btnStartCapture_Click(object sender, EventArgs e)
        {
            if (this.btnStartCapture.Text == "开始采集")
            {
                es.StartCapture(this.pictureBox1.Handle, CaptureType.SCREEN_CAPTURE);
                this.btnStartCapture.Text = "停止采集";
            }
            else
            {
                if (btnStartPush.Text == "停止推送")
                {
                    es.StopPush();
                    btnStartPush.Text = "开始推送";
                }
                es.StopCaptureAndPush();
                this.btnStartCapture.Text = "开始采集";
            }

        }
    }
}
