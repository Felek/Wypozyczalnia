using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Wypozyczalnia.Database
{
    public class QueriesEmployee
    {
        WypozyczalniaDataClassesDataContext db;

        public QueriesEmployee()
        {
            db = new WypozyczalniaDataClassesDataContext();
        }

        public QueriesEmployee(WypozyczalniaDataClassesDataContext dbContext)
        {
            db = dbContext;
        }

        public DataTable SelectAll()
        {
            var query = from e in db.Pracowniks
                        orderby e.Nazwisko ascending
                        select new
                        {
                            e.Pracownik_ID,
                            e.Imię,
                            e.Nazwisko,
                            e.Data_urodzenia,
                            e.Miejsce_urodzenia,
                            e.Pensja,
                            e.Funkcja.Nazwa_funkcji
                        };

            DataTable dt = Extensions.ToDataTable(query);
            return dt;
        }

        public void Insert(Pracownik employee)
        {
            db.Pracowniks.InsertOnSubmit(employee);
            db.SubmitChanges();
        }

        public void Edit(Pracownik e)
        {
            var record = db.Pracowniks.Single(employee => employee.Pracownik_ID == e.Pracownik_ID);
            record.Imię = e.Imię;
            record.Nazwisko = e.Nazwisko;
            record.Data_urodzenia = e.Data_urodzenia;
            record.Miejsce_urodzenia = e.Miejsce_urodzenia;
            record.Pensja = e.Pensja;
            record.Funkcja_Funkcja_ID = e.Funkcja_Funkcja_ID;
            db.SubmitChanges();
        }

        public void Delete(Pracownik e)
        {
            var record = db.Pracowniks.Single(employee => employee.Pracownik_ID == e.Pracownik_ID);
            db.Pracowniks.DeleteOnSubmit(record);
            db.SubmitChanges();
        }

        public DataTable SelectBySurname(string surname)
        {
            var query = from e in db.Pracowniks
                        where e.Nazwisko == surname
                        select new
                        {
                            e.Pracownik_ID,
                            e.Imię,
                            e.Nazwisko,
                            e.Data_urodzenia,
                            e.Miejsce_urodzenia,
                            e.Pensja,
                            e.Funkcja.Nazwa_funkcji
                        };
            DataTable dt = Extensions.ToDataTable(query);
            return dt;
        }

        public DataTable SelectByFunction(string function)
        {
            var query = from e in db.Pracowniks
                        where e.Funkcja.Nazwa_funkcji == function
                        select new
                        {
                            e.Pracownik_ID,
                            e.Imię,
                            e.Nazwisko,
                            e.Data_urodzenia,
                            e.Miejsce_urodzenia,
                            e.Pensja,
                            e.Funkcja.Nazwa_funkcji
                        };
            DataTable dt = Extensions.ToDataTable(query);
            return dt;
        }

        public Funkcja GetFunction(string functionName)
        {
            var f = db.Funkcjas.Single(funkcja => funkcja.Nazwa_funkcji == functionName);
            return f;
        }

        public List<string> GetAllFunctions()
        {
            IEnumerable<Funkcja> table = db.Funkcjas.ToList();
            List<string> list = new List<string>();
            foreach (Funkcja f in table) 
            {
                list.Add(f.Nazwa_funkcji);
            }
            return list;
        }

    }
}