using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wypozyczalnia.Database
{
    public class DatabaseSettings
    {
        /// <summary>
        /// Zapisuje nazwe uzytkownika
        /// </summary>
        /// <param name="user">Nazwa uzytkownika</param>
        public static void Save(string user)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            try
            {
                config.AppSettings.Settings["user"].Value = user;
            }
            catch (NullReferenceException ex)
            {
                config.AppSettings.Settings.Add("user", user);
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// Zapisuje informacje o bazie danych oraz nazwe uzytkownika
        /// </summary>
        /// <param name="server">Nazwa serwera</param>
        /// <param name="database">Nazwa bazy danych</param>
        /// <param name="user">Nazwa uzytkownika</param>
        public static void Save(string server, string database, string user)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            
            try
            {
                config.AppSettings.Settings["server"].Value = server;
            }
            catch (NullReferenceException ex)
            {
                config.AppSettings.Settings.Add("server", server);
            }

            try
            {
                config.AppSettings.Settings["database"].Value = database;
            }
            catch (NullReferenceException ex)
            {
                config.AppSettings.Settings.Add("database", database);
            }

            try
            {
                config.AppSettings.Settings["user"].Value = user;
            }
            catch (NullReferenceException ex)
            {
                config.AppSettings.Settings.Add("user", user);
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// Zapisuje informacje o bazie danych
        /// </summary>
        /// <param name="server">Nazwa serwera</param>
        /// <param name="database">Nazwa bazy danych</param>
        public static void Save(string server, string database)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            
            try
            {
                config.AppSettings.Settings["server"].Value = server;
            }
            catch (NullReferenceException ex)
            {
                config.AppSettings.Settings.Add("server", server);
            }

            try
            {
                config.AppSettings.Settings["database"].Value = database;
            }
            catch (NullReferenceException ex)
            {
                config.AppSettings.Settings.Add("database", database);
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static string CreateConnectionString(string password)
        {
            return "Server=" + ConfigurationManager.AppSettings["server"] + ";" +
                "Database=" + ConfigurationManager.AppSettings["database"] + ";" +
                "User Id=" + ConfigurationManager.AppSettings["user"] + ";" +
                "Password=" + password + ";";
        }

        public static string CreateConnectionString(string user, string password)
        {
            return "Server=" + ConfigurationManager.AppSettings["server"] + ";" +
                "Database=" + ConfigurationManager.AppSettings["database"] + ";" +
                "User Id=" + user + ";" +
                "Password=" + password + ";";
        }

        public static string CreateConnectionString(string server, string database, string user, string password)
        {
            return "Server=" + server + ";" +
                "Database=" + database + ";" +
                "User Id=" + user + ";" +
                "Password=" + password + ";";
        }
    }
}
