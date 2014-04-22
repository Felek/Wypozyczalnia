using System.Data;
using System.Linq;

namespace Wypozyczalnia.Database
{
    public class QueriesClient
    {
        WypozyczalniaDataClassesDataContext db;

        public QueriesClient()
        {
            db = new WypozyczalniaDataClassesDataContext();
        }

        public QueriesClient(WypozyczalniaDataClassesDataContext dbContext)
        {
            db = dbContext;
        }

        public DataTable SelectAll()
        {
            var query = from c in db.Klients
                    orderby c.Nazwisko ascending
                    select new
                    {
                        c.Klient_ID,
                        c.Imię,
                        c.Nazwisko,
                        c.Nr_dowodu
                    };

            DataTable dt = Extensions.ToDataTable(query);
            return dt;
        }

        public void Insert(Klient client)
        {
            db.Klients.InsertOnSubmit(client);
            db.SubmitChanges();
        }

        public void Edit(Klient c)
        {
            var record = db.Klients.Single(client => client.Klient_ID == c.Klient_ID);
            record.Imię = c.Imię;
            record.Nazwisko = c.Nazwisko;
            record.Nr_dowodu = c.Nr_dowodu;
            db.SubmitChanges();
        }

        public void Delete(Klient c)
        {
            var record = db.Klients.Single(client => client.Klient_ID == c.Klient_ID);
            db.Klients.DeleteOnSubmit(record);
            db.SubmitChanges();
        }

        public DataTable SelectBySurname(string surname)
        {
            var query = from c in db.Klients
                        where c.Nazwisko == surname
                        select new
                        {
                            c.Klient_ID,
                            c.Imię,
                            c.Nazwisko,
                            c.Nr_dowodu
                        };

            DataTable dt = Extensions.ToDataTable(query);
            return dt;
        }
    }
}
