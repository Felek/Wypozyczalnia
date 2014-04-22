using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wypozyczalnia.Database;
using Wypozyczalnia.View;

namespace Wypozyczalnia
{
    public class DatabaseSettingsController
    {
        private DatabaseSettingsForm form;
        private WypozyczalniaDataClassesDataContext dbContext;

        public DatabaseSettingsController(DatabaseSettingsForm form)
        {
            this.form = form;
            form.SetController(this);
        }

        public WypozyczalniaDataClassesDataContext DbContext
        {
            set { this.dbContext = value; }
        }

        public void Confirm()
        {
            if ((form.TextBox1.Length > 0) && (form.TextBox2.Length > 0) &&
                (form.TextBox3.Length > 0) && (form.TextBox4.Length > 0))
            {
                DatabaseSettings.Save(form.TextBox1, form.TextBox2, form.TextBox3);

                dbContext.Connection.ConnectionString
                    = DatabaseSettings.CreateConnectionString(form.TextBox1, form.TextBox2, form.TextBox3, form.TextBox4);
                form.Dispose();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Pola nie mogą być puste", "Błąd");
                form.DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }

    }
}
