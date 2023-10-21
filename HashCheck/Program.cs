using System;
using System.Windows.Forms;

namespace HashCheck
{
    class Program
    {
        [STAThread] 
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmHashCheck());
        }
    }
}
