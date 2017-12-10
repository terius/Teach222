using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace StudentUser
{
    public partial class DisableMouseAndKeyboardForm : Form
    {
        bool preventClose = true;
        public DisableMouseAndKeyboardForm()
        {
            InitializeComponent();
         
        }

    

        public void SetDisable(bool setBlack)
        {
            if (setBlack)
            {
                this.Opacity = 1.00;
                this.BackColor = Color.Black;
            }
            else
            {
                this.Opacity = 0.1;
                this.BackColor = SystemColors.Control;
            }
            this.timer1.Start();
            preventClose = true;
        }

        public void Release()
        {
            timer1.Stop();
            preventClose = false;
            this.Close();
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            Process proc = new Process();
            this.Text = DateTime.Now.ToLongTimeString();
            foreach (Process runningProcess in Process.GetProcesses())
            {
                if (runningProcess.ProcessName == "taskmgr")
                {
                    proc = runningProcess;
                    ProcessStartInfo pStart = runningProcess.StartInfo;
                    proc.Kill();
                }
            }
        }

     

        private void DisableMouseAndKeyboardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (preventClose == true)
                e.Cancel = true;
        }
    }
}
