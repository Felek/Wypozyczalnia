using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wypozyczalnia.View
{
    public partial class DatabaseSettingsForm : Form
    {
        private DatabaseSettingsController contoller;

        public DatabaseSettingsForm()
        {
            InitializeComponent();
            InitTextBoxes();
        }

        public void SetController(DatabaseSettingsController controller)
        {
            this.contoller = controller;
        }

        public void InitTextBoxes()
        {
            textBox1.Text = ConfigurationManager.AppSettings["server"];
            textBox2.Text = ConfigurationManager.AppSettings["database"];
            textBox3.Text = ConfigurationManager.AppSettings["user"];
            textBox4.Text = ConfigurationManager.AppSettings["password"];
        }

        public string TextBox1
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public string TextBox2
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }

        public string TextBox3
        {
            get { return textBox3.Text; }
            set { textBox3.Text = value; }
        }

        public string TextBox4
        {
            get { return textBox4.Text; }
            set { textBox4.Text = value; }
        }

        private void ActionConfirm(object sender, EventArgs e)
        {
            contoller.Confirm();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
