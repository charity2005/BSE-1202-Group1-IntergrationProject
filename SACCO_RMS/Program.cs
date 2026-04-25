using System;
using System.Windows.Forms;
using SACCO_RMS.Database;
using SACCO_RMS.Forms;

namespace SACCO_RMS
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DatabaseHelper.Initialise();
            Application.Run(new LoginForm());
        }
    }
}
