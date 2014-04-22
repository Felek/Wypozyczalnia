using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wypozyczalnia.Database;
using Wypozyczalnia.View;

namespace Wypozyczalnia
{
    public class EmployeeFormController : IFormController
    {
        private EmployeeForm form;
        private Operation operation;
        private QueriesEmployee qe;

        public EmployeeFormController(EmployeeForm form)
        {
            this.form = form;
            form.SetController(this);
        }

        public EmployeeFormController(EmployeeForm form, Operation operation)
        {
            this.form = form;
            form.SetController(this);
            this.operation = operation;
            SetFormTitle();
            SetTextBoxesState();
        }

        public QueriesEmployee Queries
        {
            set { this.qe = value; }
        }

        public void Confirm()
        {
            switch (operation)
            {
                case Operation.Add:
                    Add();
                    break;
                case Operation.Edit:
                    Edit();
                    break;
                case Operation.Delete:
                    Delete();
                    break;
            }
        }

        public void Add()
        {
            form.DialogResult = DialogResult.None;
            try
            {
                // sprawdzenie poprawnosci danych
                IsDataCorrect();
                // LINQ
                Pracownik employee = new Pracownik
                {
                    Imię = form.TextBox2,
                    Nazwisko = form.TextBox3,
                    Data_urodzenia = Convert.ToDateTime(form.TextBox4),
                    Miejsce_urodzenia = form.TextBox5,
                    Pensja = Convert.ToSingle(form.TextBox6),
                    Funkcja = qe.GetFunction(form.ComboBox1)
                };
                qe.Insert(employee);
                // zamkniecie formularza
                form.DialogResult = DialogResult.OK;
                form.Dispose();
            }
            catch (DataIncorrect ex)
            {
                MessageBox.Show(ex.Message, "Błąd");
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Błędny format danych.", "Błąd");
            }
            catch (SqlException ex)
            {
                //nie udalo sie polaczyc/bledna skladnia zapytania/bledne dane w zapytaniu/?
                MessageBox.Show("Błąd komunikacji z bazą danych", "Błąd");
            }
        }

        public void Edit()
        {
            form.DialogResult = DialogResult.None;
            try
            {
                // sprawdzenie poprawnosci danych
                IsDataCorrect();
                // LINQ
                Pracownik employee = new Pracownik
                {
                    Pracownik_ID = Convert.ToInt32(form.TextBox1),
                    Imię = form.TextBox2,
                    Nazwisko = form.TextBox3,
                    Data_urodzenia = Convert.ToDateTime(form.TextBox4),
                    Miejsce_urodzenia = form.TextBox5,
                    Pensja = Convert.ToSingle(form.TextBox6),
                    Funkcja_Funkcja_ID = qe.GetFunction(form.ComboBox1).Funkcja_ID
                };
                qe.Edit(employee);
                // zamkniecie formularza
                form.DialogResult = DialogResult.OK; ;
                form.Dispose();
            }
            catch (DataIncorrect ex)
            {
                MessageBox.Show(ex.Message, "Błąd");
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Błędny format danych.", "Błąd");
            }
            catch (SqlException ex)
            {
                //nie udalo sie polaczyc/bledna skladnia zapytania/bledne dane w zapytaniu/?
                MessageBox.Show("Błąd komunikacji z bazą danych", "Błąd");
            }
        }

        public void Delete()
        {
            form.DialogResult = DialogResult.None;
            try
            {
                // LINQ
                Pracownik employee = new Pracownik
                {
                    Pracownik_ID = Convert.ToInt32(form.TextBox1),
                    Imię = form.TextBox2,
                    Nazwisko = form.TextBox3,
                    Data_urodzenia = Convert.ToDateTime(form.TextBox4),
                    Miejsce_urodzenia = form.TextBox5,
                    Pensja = Convert.ToSingle(form.TextBox6),
                    Funkcja_Funkcja_ID = qe.GetFunction(form.ComboBox1).Funkcja_ID
                };
                qe.Delete(employee);
                // zakmniecie formularza
                form.DialogResult = DialogResult.OK;
                form.Dispose();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Błędne dane.", "Błąd");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Błąd komunikacji z bazą danych", "Błąd");
            }
        }

        private void IsDataCorrect()
        {
            string message = "Pole nie może być puste.";
            if ((form.TextBox2.Length <= 0) || (form.TextBox3.Length <= 0) || (form.TextBox4.Length <= 0) ||
                (form.TextBox5.Length <= 0) || (form.TextBox6.Length <= 0))
            {
                throw new DataIncorrect(message);
            }
        }

        private void SetTextBoxesState()
        {
            switch (operation)
            {
                case Operation.Add:
                    form.EnabledTextBox1 = false;
                    break;
                case Operation.Edit:
                    form.EnabledTextBox1 = false;
                    break;
                case Operation.Delete:
                    form.DisableAllFields();
                    break;
            }
        }

        private void SetFormTitle()
        {
            switch (operation)
            {
                case Operation.Add:
                    form.Title = "Dodawanie nowego pracownika";
                    break;
                case Operation.Edit:
                    form.Title = "Edycja pracownika";
                    break;
                case Operation.Delete:
                    form.Title = "Usuwanie pracownika";
                    break;
            }
        }
    }
}
