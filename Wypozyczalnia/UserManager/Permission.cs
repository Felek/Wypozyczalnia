using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManager
{
    public enum AccountType
    {
        Admin,
        Sprzedawca,
        Serwisant,
        Magazynier,
        Kadrowy
    }

    public enum TableAccess
    {
        FullControll,
        Edit,
        Insert,
        Select,
        None
    }

    public class Permission
    {
        private Dictionary<string, TableAccess> dictionary = new Dictionary<string,TableAccess>();

        public Permission()
        {
            
        }

        public Permission(AccountType account)
        {
            switch (account)
            {
                case AccountType.Admin:
                    dictionary.Add("Klient", TableAccess.FullControll);
                    dictionary.Add("Pracownik", TableAccess.FullControll);
                    dictionary.Add("Funkcja", TableAccess.FullControll);
                    dictionary.Add("Rezerwacja", TableAccess.FullControll);
                    dictionary.Add("pilotuje", TableAccess.FullControll);
                    dictionary.Add("Statek", TableAccess.FullControll);
                    dictionary.Add("Typ_statku", TableAccess.FullControll);
                    dictionary.Add("jest_serwisowany", TableAccess.FullControll);
                    dictionary.Add("Część", TableAccess.FullControll);
                    dictionary.Add("Status_części", TableAccess.FullControll);
                    dictionary.Add("Zamówienie", TableAccess.FullControll);
                    break;

                case AccountType.Sprzedawca:
                    dictionary.Add("Klient", TableAccess.Edit);
                    dictionary.Add("Pracownik", TableAccess.Select);
                    dictionary.Add("Funkcja", TableAccess.Select);
                    dictionary.Add("Rezerwacja", TableAccess.FullControll);
                    dictionary.Add("pilotuje", TableAccess.FullControll);
                    dictionary.Add("Statek", TableAccess.Select);
                    dictionary.Add("Typ_statku", TableAccess.Select);
                    break;

                case AccountType.Serwisant:
                    dictionary.Add("Statek", TableAccess.Select);
                    dictionary.Add("Typ_statku", TableAccess.Select);
                    dictionary.Add("jest_serwisowany", TableAccess.Edit);
                    dictionary.Add("Część", TableAccess.Edit);
                    dictionary.Add("Status_części", TableAccess.Select);
                    break;

                case AccountType.Magazynier:
                    dictionary.Add("Część", TableAccess.Edit);
                    dictionary.Add("Status_części", TableAccess.Select);
                    dictionary.Add("Zamówienie", TableAccess.FullControll);
                    break;

                case AccountType.Kadrowy:
                    dictionary.Add("Pracownik", TableAccess.FullControll);
                    dictionary.Add("Funkcja", TableAccess.Select);
                    break;
            }
        }

        public Dictionary<string, TableAccess> Dictionary
        {
            get { return this.dictionary; }
        }
    }
}
