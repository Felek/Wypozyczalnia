using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wypozyczalnia.Database
{
    public class QueriesWarehouse
    {
        WypozyczalniaDataClassesDataContext db;

        public QueriesWarehouse()
        {
            db = new WypozyczalniaDataClassesDataContext();
        }

        public QueriesWarehouse(WypozyczalniaDataClassesDataContext dbContext)
        {
            db = dbContext;
        }

        public Status_części GetStatus(string statusName)
        {
            var s = db.Status_częścis.Single(status => status.Status == statusName);
            return s;
        }

        public List<string> GetAllFunctions()
        {
            IEnumerable<Status_części> table = db.Status_częścis.ToList();
            List<string> list = new List<string>();
            foreach (Status_części s in table)
            {
                list.Add(s.Status);
            }
            return list;
        }
    }
}
