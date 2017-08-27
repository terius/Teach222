using System;
using System.Drawing;
using System.Windows.Forms;

namespace vlctest
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.panelEx1.DrawTime();
        }
    }
}
