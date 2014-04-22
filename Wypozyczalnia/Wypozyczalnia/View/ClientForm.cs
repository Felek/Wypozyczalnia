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
    public partial class ClientForm : BaseForm
    {
        public ClientForm()
        {
            InitializeComponent();
        }

        public ClientForm(Klient client)
        {
            InitializeComponent();

            textBox1.Text = "" + client.Klient_ID;
            textBox2.Text = client.Imię;
            textBox3.Text = client.Nazwisko;
            textBox4.Text = client.Nr_dowodu;
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

        public override void DisableAllFields()
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
        }

    }
}

