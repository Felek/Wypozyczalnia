using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UserManager
{
    public class DBUserManager
    {
        public string User { get; set; }
        private string password;
        public Permission Permission { get; set; }
        private SqlConnection connection;

        public DBUserManager()
        {

        }

        public DBUserManager(string user, string password, Permission permission, SqlConnection connection)
        {
            this.Permission = permission;
            this.User = user;
            this.password = password;
            this.connection = connection;
        }

        public SqlConnection Connection
        {
            set { this.connection = value; }
        }

        public void ExecuteCreateUser()
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(CreateUserCreationString(), connection);
                //command.ExecuteNonQuery();

                foreach (KeyValuePair<string, TableAccess> ta in Permission.Dictionary)
                {
                    command = new SqlCommand(CreateCommandString(ta), connection);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
            catch (SqlException ex)
            {

            }
        }

        private string CreateUserCreationString()
        {
            return "CREATE LOGIN " + User + " " +
                "WITH PASSWORD = '" + password + "'; " +
                "USE " + connection.Database + "; " +
                "CREATE USER " + User + " FOR LOGIN " + User + ";";
        }

        private string CreateCommandString(KeyValuePair<string, TableAccess> permission)
        {
            string result = "";
            switch (permission.Value)
            {
                case TableAccess.FullControll:
                    result += "GRANT SELECT, INSERT, UPDATE, DELETE ON " + permission.Key +
                        " TO " + User + " ";
                    break;
                case TableAccess.Edit:
                    result += "GRANT SELECT, INSERT, UPDATE ON " + permission.Key +
                        " TO " + User + " ";
                    break;
                case TableAccess.Insert:
                    result += "GRANT SELECT, INSERT ON " + permission.Key +
                        " TO " + User + " ";
                    break;
                case TableAccess.Select:
                    result += "GRANT SELECT ON " + permission.Key +
                        " TO " + User + " ";
                    break;
            }
            return result;
        }

    }
}
