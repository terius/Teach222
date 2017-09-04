using System.Drawing;
using System.Windows.Forms;
using Model;
using SharedForms;

namespace NewTeacher.Controls
{
    public partial class StudentScreen : UserControl
    {
        public string UserName { get; set; }
        public StudentScreen(ScreenCaptureInfo info)
        {
            InitializeComponent();
            this.picScreen.Image = info.Image;
            this.labName.Text = info.DisplayName;
            this.UserName = info.UserName;
            this.picScreen.DoubleClick += PicScreen_DoubleClick;
        
        }

        public StudentScreen()
        {
            InitializeComponent();
        }

        private void PicScreen_DoubleClick(object sender, System.EventArgs e)
        {
            GlobalVariable.client.Send_CallStudentShowForMySelf(UserName);
            labCallShow.Show();
        }

        public void HideCallShowLabel()
        {
            labCallShow.Hide();
        }

       

        public void UpdateScreen(Image img)
        {
            this.picScreen.Image = img;
        }

      
    }
}
