using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using EventManageWPF.View;
using EventManageWPF.Util;

namespace EventManageWPF
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Application.UserAppDataRegistry.GetValue("remeberMe") != null)
            {
                ForgotPassword forgotPassword = new ForgotPassword();
                forgotPassword.Show();
            }
            else
            {
                Login login = new Login();
                login.Show();
            }
            DatabaseConnectivity databaseConnectivity = new DatabaseConnectivity();
            databaseConnectivity.connectionValidator();
            Application.Run();

           
           
        }

    }
}
