using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Wypozyczalnia.Database;
using Wypozyczalnia.View;

namespace Wypozyczalnia
{
    public class ClientFormController : IFormController
    {
        private ClientForm form;
        private Operation operation;
        private QueriesClient qc;
        
        public ClientFormController(ClientForm form)
        {
            this.form = form;
            form.SetController(this);
        }

        public ClientFormController(ClientForm form, Operation operation)
        {
            this.form = form;
            form.SetController(this);
            this.operation = operation;
            SetFormTitle();
            SetTextBoxesState();
        }

        public QueriesClient Queries
        {
            set { this.qc = value; }
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
                Klient client = new Klient
                {
                    Imię = form.TextBox2,
                    Nazwisko = form.TextBox3,
                    Nr_dowodu = form.TextBox4
                };
                qc.Insert(client);
                // zamkniecie formularza
                form.DialogResult = DialogResult.OK;
                form.Dispose();
            }
            catch (DataIncorrect ex)
            {
                MessageBox.Show(ex.Message, "Błąd");
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
                Klient client = new Klient
                {
                    Klient_ID = Convert.ToInt32(form.TextBox1),
                    Imię = form.TextBox2,
                    Nazwisko = form.TextBox3,
                    Nr_dowodu = form.TextBox4
                };
                qc.Edit(client);
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

        public void Delete()
        {
            form.DialogResult = DialogResult.None;
            try
            {
                // LINQ
                Klient client = new Klient
                {
                    Klient_ID = Convert.ToInt32(form.TextBox1),
                    Imię = form.TextBox2,
                    Nazwisko = form.TextBox3,
                    Nr_dowodu = form.TextBox4
                };
                qc.Delete(client);
                // zamkniecie formularza
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
            if ((form.TextBox2.Length <= 0) || (form.TextBox3.Length <= 0) || (form.TextBox4.Length <= 0))
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
                    form.Title = "Dodawanie nowego klienta";
                    break;
                case Operation.Edit:
                    form.Title = "Edycja klienta";
                    break;
                case Operation.Delete:
                    form.Title = "Usuwanie klienta";
                    break;
            }
        }
    }
}
