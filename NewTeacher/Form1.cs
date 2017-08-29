using SharedForms;
using System.Windows.Forms;

namespace NewTeacher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            GlobalVariable.CreateTestLoginInfo();
            ChatForm frm = new ChatForm();
            frm.Show();
        }
    }
}
