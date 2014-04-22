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
    public partial class EmployeesView : BaseView
    {
        private List<string> functions = null;
        private string everyFunction = "Każda";

        public EmployeesView()
        {
            InitializeComponent();
            filterFunction.Items.Add(everyFunction);
            filterFunction.SelectedItem = everyFunction;
        }

        public string FilterSurname
        {
            get { return filterSurname.Text; }
        }

        public string FilterFunction
        {
            get { return filterFunction.SelectedItem.ToString(); }
        }

        // Ustawienie szerokosci kolumn oraz naglowkow
        public override void SetColumnsWidth()
        {
            try
            {
                double width = dataGridView1.Width - 20;
                dataGridView1.Columns[0].Width = (int)(0.07 * width);
                dataGridView1.Columns[1].Width = (int)(0.2 * width);
                dataGridView1.Columns[2].Width = (int)(0.2 * width);
                dataGridView1.Columns[3].Width = (int)(0.13 * width);
                dataGridView1.Columns[4].Width = (int)(0.2 * width);
                dataGridView1.Columns[5].Width = (int)(0.1 * width);
                dataGridView1.Columns[6].Width = (int)(0.1 * width);
            }
            catch (ArgumentOutOfRangeException ex)
            {

            }
        }

        public void FillFunctionsList(List<string> functions)
        {
            this.functions = functions;
            filterFunction.Items.Clear();
            filterFunction.Items.Add(everyFunction);
            filterFunction.SelectedItem = everyFunction;
            foreach (string function in functions)
            {
                filterFunction.Items.Add(function);
            }
        }

        /// <summary>
        /// Pobranie z tabeli danych i utworzenie obiektu Pracownik
        /// </summary>
        /// <returns></returns>
        public Pracownik GetActiveElement()
        {
            try
            {
                int index = dataGridView1.CurrentRow.Index;

                return new Pracownik()
                {
                    Pracownik_ID = Convert.ToInt32(dataGridView1[0, index].Value),
                    Imię = dataGridView1[1, index].Value.ToString(),
                    Nazwisko = dataGridView1[2, index].Value.ToString(),
                    Data_urodzenia = Convert.ToDateTime(dataGridView1[3, index].Value),
                    Miejsce_urodzenia = dataGridView1[4, index].Value.ToString(),
                    Pensja = Convert.ToSingle(dataGridView1[5, index].Value),
                    Funkcja = new Funkcja() {
                        Nazwa_funkcji = dataGridView1[6, index].Value.ToString()
                    }
                };
            }
            catch (FormatException ex)
            {
                return null;
            }      
        }

        private void ActionAdd(object sender, EventArgs e)
        {
            controller.ShowEmployeeAddForm();
        }

        private void ActionEdit(object sender, EventArgs e)
        {
            controller.ShowEmployeeEditForm();
        }

        private void ActionDelete(object sender, EventArgs e)
        {
            controller.ShowEmployeeDeleteForm();
        }

        private void ActionResized(object sender, EventArgs e)
        {
            this.SetColumns();
        }

        private void ActionSearchBySurname(object sender, EventArgs e)
        {
            controller.SelectEmployeeBySurname();
        }

        private void ActionSearchByFunction(object sender, EventArgs e)
        {
            try
            {
                controller.SelectEmployeeByFunction();
            }
            catch (NullReferenceException ex)
            {

            }
        }
    }
}
