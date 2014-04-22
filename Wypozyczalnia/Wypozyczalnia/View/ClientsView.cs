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
    public partial class ClientsView : BaseView
    {

        public ClientsView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Pobranie z tabeli danych i utworzenie obiektu Klient
        /// </summary>
        /// <returns></returns>
        public Klient GetActiveElement()
        {
            try
            {
                int index = dataGridView1.CurrentRow.Index;

                return new Klient()
                {
                    Klient_ID = Convert.ToInt32(dataGridView1[0, index].Value),
                    Imię = dataGridView1[1, index].Value.ToString(),
                    Nazwisko = dataGridView1[2, index].Value.ToString(),
                    Nr_dowodu = dataGridView1[3, index].Value.ToString()
                };
            }
            catch (FormatException ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Ustawienie szerokosci kolumn oraz naglowkow
        /// </summary>
        public override void SetColumnsWidth()
        {
            try
            {
                double width = dataGridView1.Width - 20;
                dataGridView1.Columns[0].Width = (int)(0.1 * width);
                dataGridView1.Columns[1].Width = (int)(0.3 * width);
                dataGridView1.Columns[2].Width = (int)(0.3 * width);
                dataGridView1.Columns[3].Width = (int)(0.3 * width);
            }
            catch (ArgumentOutOfRangeException ex)
            {

            }
        }

        public string FilterSurname
        {
            get { return filterSurname.Text; }
        }

        // Wywolanie funkcji obslugujacych zdarzenia

        private void ActionAdd(object sender, EventArgs e)
        {
            controller.ShowClientAddForm();
        }

        private void ActionEdit(object sender, EventArgs e)
        {
            controller.ShowClientEditForm();
        }

        private void ActionDelete(object sender, EventArgs e)
        {
            controller.ShowClientDeleteForm();
        }

        private void ActionResized(object sender, EventArgs e)
        {
            SetColumns();
        }

        private void ActionReservations(object sender, EventArgs e)
        {
            // TODO
        }

        private void ActionSearchBySurname(object sender, EventArgs e)
        {
            controller.SearchClientBySurname();
        }

    }
}
