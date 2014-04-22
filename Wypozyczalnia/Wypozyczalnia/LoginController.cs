using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wypozyczalnia.Database;
using Wypozyczalnia.View;

namespace Wypozyczalnia
{
    public class LoginController
    {
        private LoginForm form;

        public LoginController(LoginForm form)
        {
            this.form = form;
            form.SetController(this);
        }

        public void Login()
        {
            WypozyczalniaDataClassesDataContext dbContext = new WypozyczalniaDataClassesDataContext(
                //"Data Source=HANIA-LAPTOP\\SQLEXPRESS;Initial Catalog=Test2;User ID=sa;Password=Admin1");
                DatabaseSettings.CreateConnectionString(form.UserName, form.Password));
            DatabaseSettings.Save(form.UserName);

            BaseView initForm = new ClientsView();
            Controller controller = new Controller(dbContext, initForm);
            initForm.Show();
            form.Hide();

        }
    }
}
