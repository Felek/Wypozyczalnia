using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wypozyczalnia.Database;
using Wypozyczalnia.View;

namespace Wypozyczalnia
{
    // Typ operacji formularza
    public enum Operation { Add, Edit, Delete }

    // Glowny kontroler programu
    //
    public class Controller
    {
        // okienka
        private ClientsView clients;
        private EmployeesView employees;
        private WarehouseView warehouse;
        // referencja do aktywnego obiektu
        private BaseView activeView;
        // informacja czy aplikacja ma zostac zamknieta
        public Boolean IsClosing { get; set; }
        // lista funkcji pracownika
        private List<string> functions = null;
        private List<string> statuses = null;
        // wynik dzialania forularza
        private DialogResult dr;
        // mapowanie bd
        private WypozyczalniaDataClassesDataContext dbContext;
        private QueriesClient queriesClient;
        private QueriesEmployee queriesEmployee;
        private QueriesWarehouse queriesWarehouse;

        public Controller(WypozyczalniaDataClassesDataContext dbContext, BaseView initForm)
        {
            activeView = initForm;
            this.dbContext = dbContext;
            // TODO: sprawdzenie typu przekazanego parametru
            clients = (ClientsView)initForm;

            // zainicjalizowanie pozostalych okienek
            employees = new EmployeesView();
            warehouse = new WarehouseView();
            clients.SetController(this);
            employees.SetController(this);
            warehouse.SetController(this);
            IsClosing = false;

            // inicjalizacja obiektow dbContext
            queriesClient = new QueriesClient(dbContext);
            queriesEmployee = new QueriesEmployee(dbContext);
            queriesWarehouse = new QueriesWarehouse(dbContext);

            // inicjalizacja DialogResult
            dr = DialogResult.None;

            // inicjalizacja danych w domyslnym okienku
            SelectAllAtActiveWindow();
            UpdateDBStatus();
        }

        // --- --- --- --- --- ZMIANA AKTYWNEGO OKNA --- --- --- --- --- // 
        #region Zmiana aktywnego okna

        public void ShowClientsView()
        {
            if (activeView != clients)
            {
                activeView.Hide();
                clients.CopyWindowState(activeView);
                activeView = clients;
                clients.Show();
                SelectAllAtActiveWindow();
                UpdateDBStatus();
            }
        }

        public void ShowEmployeesView()
        {
            if (activeView != employees)
            {
                activeView.Hide();
                employees.CopyWindowState(activeView);
                activeView = employees;
                // lista funkcji
                if (functions == null)
                {
                    try
                    {
                        functions = queriesEmployee.GetAllFunctions();
                        employees.FillFunctionsList(functions);
                    }
                    catch (SqlException ex)
                    {
                        // TODO: co teraz?
                    }
                }
                employees.Show();
                SelectAllAtActiveWindow();
                UpdateDBStatus();
            }
        }

        public void ShowWarehouseView()
        {
            if (activeView != warehouse)
            {
                activeView.Hide();
                warehouse.CopyWindowState(activeView);
                activeView = warehouse;
                // lista funkcji
                if (statuses == null)
                {
                    try
                    {
                        statuses = queriesWarehouse.GetAllFunctions();
                        warehouse.FillStatusList(statuses);
                    }
                    catch (SqlException ex)
                    {
                        // TODO: co teraz?
                    }
                }
                warehouse.Show();
                SelectAllAtActiveWindow();
                UpdateDBStatus();
            }
        }
        #endregion

        // --- --- --- --- --- OBSLUGA BD --- --- --- --- --- // 
        #region Obsluga BD

        /// <summary>
        /// Wywolania okna dialogowego do zmiany ustawien bazy danych i zastosowanie ich
        /// </summary>
        public void ChangeDBSettings()
        {
            DatabaseSettingsForm form = new DatabaseSettingsForm();
            DatabaseSettingsController formController = new DatabaseSettingsController(form);
            formController.DbContext = dbContext;
            DialogResult dr = form.ShowDialog();
            if (dr == DialogResult.OK)
            {
                UpdateDBStatus();
            }
        }

        /// <summary>
        /// Sprawdzenie czy parametry konfiguracji pozwalaja na polaczenie z baza danych
        /// </summary>
        public void UpdateDBStatus()
        {
            if (ConfigurationManager.AppSettings["database"] != activeView.DBStatus)
            {
                try
                {
                    // sprawdzenie polaczenia
                    dbContext.Connection.Open();
                    activeView.DBStatus = ConfigurationManager.AppSettings["database"];
                    dbContext.Connection.Close();
                    activeView.DBStatusColor = new System.Drawing.Color();
                }
                catch (SqlException ex)
                {
                    activeView.DBStatus = "brak połączenia";
                    activeView.DBStatusColor = System.Drawing.Color.Red;
                }
            }
        }

        /// <summary>
        /// Wybranie wszystkich rekordow aktywnej tabeli i zaladowanie do widoku
        /// </summary>
        public void SelectAllAtActiveWindow()
        {
            try
            {
                // wybor zapytania do bazy danych
                if (activeView == clients)
                {
                    activeView.DataTable = queriesClient.SelectAll();
                }
                else if (activeView == employees)
                {
                    activeView.DataTable = queriesEmployee.SelectAll();
                }

                activeView.SetColumns();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Błąd komunikacji z bazą danych", "Błąd");
                activeView.ClearTable();
            }
        }
 
        /// <summary>
        /// Sprawdza czy ostatni formularz zwrocil OK, jesli tak, odswieza dane w tabeli 
        /// </summary>
        public void ReloadIfFormReturnedOK()
        {
            if (dr == DialogResult.OK)
            {
                // odswiezenie danych
                SelectAllAtActiveWindow();
            }
            // zresetowanie stanu dr
            dr = DialogResult.None;
        }

        #endregion

        // --- --- --- --- --- KLIENT --- --- --- --- --- // 
        #region Klient
        // --- FORMULARZE

        public void ShowClientAddForm()
        {
            ClientForm form = new ClientForm();
            ClientFormController formController = new ClientFormController(form, Operation.Add);
            formController.Queries = queriesClient;
            dr = form.ShowDialog();
            // odswiezenie danych
            ReloadIfFormReturnedOK();
        }

        public void ShowClientEditForm()
        {
            try
            {
                Klient client = clients.GetActiveElement();
                ClientForm form = new ClientForm(client);
                ClientFormController formController = new ClientFormController(form, Operation.Edit);
                formController.Queries = queriesClient;
                dr = form.ShowDialog();
                // odswiezenie danych
                ReloadIfFormReturnedOK();
            }
            catch (NullReferenceException ex)
            {
                // pusta tabela/?
            }
        }

        public void ShowClientDeleteForm()
        {
            try
            {
                Klient client = clients.GetActiveElement();
                ClientForm form = new ClientForm(client);
                ClientFormController formController = new ClientFormController(form, Operation.Delete);
                formController.Queries = queriesClient;
                dr = form.ShowDialog();
                // odswiezenie danych
                ReloadIfFormReturnedOK();
            }
            catch (NullReferenceException ex)
            {

            }
        }

        // --- FILTRY

        public void SearchClientBySurname()
        {
            string surname = clients.FilterSurname;
            if (surname.Length > 0)
            {
                activeView.DataTable = queriesClient.SelectBySurname(surname);
            }
            else
            {
                activeView.DataTable = queriesClient.SelectAll();
            }
            activeView.SetColumns();
        }

        #endregion

        // --- --- --- --- --- PRACOWNIK --- --- --- --- --- // 
        #region Pracownik
        // --- FORMULARZE

        public void ShowEmployeeAddForm()
        {
            EmployeeForm form = new EmployeeForm(functions);
            EmployeeFormController formController = new EmployeeFormController(form, Operation.Add);
            formController.Queries = queriesEmployee;
            dr = form.ShowDialog();
            // odswiezenie danych
            ReloadIfFormReturnedOK();
        }

        public void ShowEmployeeEditForm()
        {
            try
            {
                Pracownik employee = employees.GetActiveElement();
                EmployeeForm form = new EmployeeForm(employee, functions);
                EmployeeFormController formController = new EmployeeFormController(form, Operation.Edit);
                formController.Queries = queriesEmployee;
                dr = form.ShowDialog();
                // odswiezenie danych
                ReloadIfFormReturnedOK();
            }
            catch (NullReferenceException ex)
            {
                // pusta tabela/?
            }
        }

        public void ShowEmployeeDeleteForm()
        {
            try
            {
                Pracownik employee = employees.GetActiveElement();
                EmployeeForm form = new EmployeeForm(employee, functions);
                EmployeeFormController formController = new EmployeeFormController(form, Operation.Delete);
                formController.Queries = queriesEmployee;
                form.ShowDialog();
                // odswiezenie danych
                SelectAllAtActiveWindow();
            }
            catch (NullReferenceException ex)
            {

            }
        }

        // --- FILTRY

        public void SelectEmployeeBySurname()
        {
            string surname = employees.FilterSurname;
            if (surname.Length > 0)
            {
                activeView.DataTable = queriesEmployee.SelectBySurname(surname);
            }
            else
            {
                activeView.DataTable = queriesEmployee.SelectAll();
            }
            activeView.SetColumns();
        }

        public void SelectEmployeeByFunction()
        {
            string function = employees.FilterFunction;
            if (functions.Contains(function))
            {
                activeView.DataTable = queriesEmployee.SelectByFunction(function);
            }
            else
            {
                activeView.DataTable = queriesEmployee.SelectAll();
            }
            activeView.SetColumns();
        }

        #endregion
    }
}
