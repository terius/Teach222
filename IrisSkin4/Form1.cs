using System.Windows.Forms;

namespace IrisSkin4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            skinEngine1.SkinFile = System.Environment.CurrentDirectory + "\\Skins\\office2007.ssk";　　//选择皮肤文件
        }
    }
}
