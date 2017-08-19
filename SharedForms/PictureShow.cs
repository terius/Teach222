using Helpers;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharedForms
{
    public partial class PictureShow : Form
    {
        public PictureShow()
        {
            InitializeComponent();
        }

     

        public void ShowPic(string fileName)
        {
            Image img = Image.FromFile(fileName);
            this.pictureBox2.Image = img;
        }
    }
}
