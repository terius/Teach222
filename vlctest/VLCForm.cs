using AxAXVLC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vlctest
{
    public partial class VLCForm : Form
    {
        private AxVLCPlugin2 axVLCPlugin21;
        public VLCForm()
        {
            InitializeComponent();
        }

        private void VLCForm_Load(object sender, EventArgs e)
        {
            axVLCPlugin21 = new AxVLCPlugin2();
            axVLCPlugin21.Dock = DockStyle.Fill;
            this.Controls.Add(axVLCPlugin21);
        }
    }
}
