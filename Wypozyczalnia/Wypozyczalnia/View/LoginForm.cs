using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wypozyczalnia.View
{
    public partial class LoginForm : Form
    {
        private LoginController controller;

        public LoginForm()
        {
            InitializeComponent();
        }

        public void SetController(LoginController controller)
        {
            this.controller = controller;
        }

        public string UserName
        {
            get { return textBox1.Text; }
        }

        public string Password
        {
            get { return textBox2.Text; }
        }

        private void ActionConfirm(object sender, EventArgs e)
        {
            controller.Login();
        }

        private void ActionExit(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

