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
    public partial class BaseView : Form
    {
        protected Controller controller;

        public BaseView()
        {
            InitializeComponent();
            dataGridView1.RowHeadersVisible = false;
        }

        public void SetController(Controller controller)
        {
            this.controller = controller;
        }

        public int GetActiveElementIndex()
        {
            return dataGridView1.CurrentRow.Index;
        }

        /// <summary>
        /// Pobiera lub ustawia nazwe bazy danych w pasku statusu
        /// </summary>
        public string DBStatus
        {
            get { return dbStatusLabel.Text; }
            set { dbStatusLabel.Text = value; }
        }

        public System.Drawing.Color DBStatusColor
        {
            set { dbStatusLabel.ForeColor = value; }
        }

        public void ClearTable()
        {
            dataGridView1.DataSource = null;
        }

        /// <summary>
        /// Ustawia DataTable tablice, ktorej zawartosc ma byc wyswietlona
        /// </summary>
        public DataTable DataTable
        {
            set { this.dataGridView1.DataSource = value; }
        }

        /// <summary>
        /// Kopiuje informacje o rozmiarze i stanie okna
        /// </summary>
        /// <param name="baseView"></param>
        public void CopyWindowState(BaseView baseView)
        {

            if (baseView.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = baseView.WindowState;
            }
            else
            {
                this.Width = baseView.Width;
                this.Height = baseView.Height;
                this.Location = baseView.Location;
            }
        }

        public virtual void SetColumnNames()
        {
            DataGridViewColumnCollection columns = dataGridView1.Columns;
            foreach (DataGridViewColumn column in columns)
            {
                column.HeaderText = column.HeaderText.Replace('_', ' ');
            }
        }

        public virtual void SetColumnsWidth()
        {

        }

        /// <summary>
        /// Ustawia nazwy i szerokosci kolumn
        /// </summary>
        public void SetColumns()
        {
            SetColumnNames();
            SetColumnsWidth();
        }

        private void ActionShowClientsView(object sender, EventArgs e)
        {
            controller.ShowClientsView();
        }

        private void ActionShowEmployeesView(object sender, EventArgs e)
        {
            controller.ShowEmployeesView();
        }

        private void ActionShowWarehouseView(object sender, EventArgs e)
        {
            controller.ShowWarehouseView();
        }

        private void ActionClose(object sender, FormClosingEventArgs e)
        {
            if (!controller.IsClosing)
            {
                if (MessageBox.Show("Wyjść?", "Wypożyczalnia",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    controller.IsClosing = true;
                    Application.Exit();
                }
                else
                {
                    // Cancel the Closing event from closing the form.
                    e.Cancel = true;
                }
            }
        }

        private void ActionExit(object sender, EventArgs e)
        {
            if (!controller.IsClosing)
            {
                if (MessageBox.Show("Wyjść?", "Wypożyczalnia",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    controller.IsClosing = true;
                    Application.Exit();
                }
            }
        }

        private void ActionLoadData(object sender, EventArgs e)
        {
            controller.SelectAllAtActiveWindow();
        }

        private void ActionChangeDBSettings(object sender, EventArgs e)
        {
            controller.ChangeDBSettings();
        }

    }
}

