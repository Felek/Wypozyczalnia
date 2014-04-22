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
    public partial class BaseForm : Form
    {
        protected IFormController controller;

        public BaseForm()
        {
            InitializeComponent();
        }

        public void SetController(IFormController controller)
        {
            this.controller = controller;
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

        public Boolean EnabledTextBox1
        {
            set { textBox1.Enabled = value; }
        }

        public Boolean ReadOnlyTextBox1
        {
            set { textBox1.ReadOnly = true; }
        }

        public string Title
        {
            set { title.Text = value; }
        }

        public virtual void DisableAllFields()
        {

        }

        private void ActionCancel(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ActionCorfirm(object sender, EventArgs e)
        {
            controller.Confirm();
        }

    }
}

