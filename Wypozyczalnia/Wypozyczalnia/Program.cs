using System;
using System.Windows.Forms;
using Wypozyczalnia.View;

namespace Wypozyczalnia
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
            
            LoginForm loginForm = new LoginForm();
            LoginController controller = new LoginController(loginForm);
            Application.Run(loginForm);
        }
    }
}
