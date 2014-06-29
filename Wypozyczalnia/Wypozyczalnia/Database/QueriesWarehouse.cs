﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

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

        public Statek GetStatek(int statekID)
        {
            try
            {
                var s = db.Stateks.Single(statek => statek.Statek_ID == statekID);
                return s;
            }
            catch (InvalidOperationException)
            {
                throw new GetException("Niepoprawne ID statku");
            }
        }

        public Zamówienie GetZamowienie(int orderID)
        {
            try
            {
                var s = db.Zamówienies.Single(order => order.Zamówienie_ID == orderID);
                return s;
            }
            catch (InvalidOperationException)
            {
                throw new GetException("Niepoprawne ID zamówienia");
            }
        }

        public List<string> GetAllStatuses()
        {
            IEnumerable<Status_części> table = db.Status_częścis.ToList();
            List<string> list = new List<string>();
            foreach (Status_części s in table)
            {
                list.Add(s.Status);
            }
            return list;
        }

        public DataTable SelectAll()
        {
            var query = from cz in db.Częśćs
                        orderby cz.Nazwa ascending
                        select new
                        {
                            cz.Część_ID,
                            cz.Nazwa,
                            cz.Status_części.Status,
                            cz.Zamówienie_Zamówienie_ID,
                            cz.Cena,
                            cz.Statek_Statek_ID
                        };

            DataTable dt = Extensions.ToDataTable(query);
            return dt;
        }

        public DataTable SelectByName(string name)
        {
            var query = from cz in db.Częśćs
                        where cz.Nazwa == name
                        select new
                        {
                            cz.Część_ID,
                            cz.Nazwa,
                            cz.Status_części.Status,
                            cz.Zamówienie_Zamówienie_ID,
                            cz.Cena,
                            cz.Statek_Statek_ID
                        };

            DataTable dt = Extensions.ToDataTable(query);
            return dt;
        }

        public DataTable SelectByStatus(string status)
        {
            var query = from cz in db.Częśćs
                        where cz.Status_części.Status == status
                        select new
                        {
                            cz.Część_ID,
                            cz.Nazwa,
                            cz.Status_części.Status,
                            cz.Zamówienie_Zamówienie_ID,
                            cz.Cena,
                            cz.Statek_Statek_ID,                    
                        };
            DataTable dt = Extensions.ToDataTable(query);
            return dt;
        }

        public void Insert(Część part)
        {
            db.Częśćs.InsertOnSubmit(part);
            db.SubmitChanges();
        }

        public void Edit(Część e)
        {
            var record = db.Częśćs.Single(part => part.Część_ID == e.Część_ID);
            record.Nazwa = e.Nazwa;
            record.Zamówienie_Zamówienie_ID = e.Zamówienie_Zamówienie_ID;
            record.Cena = e.Cena;
            record.Statek_Statek_ID = e.Statek_Statek_ID;
            record.Status_części_Status_części_ID = e.Status_części_Status_części_ID;
            db.SubmitChanges();
        }

        public void Delete(Część e)
        {
            var record = db.Częśćs.Single(part => part.Część_ID == e.Część_ID);
            db.Częśćs.DeleteOnSubmit(record);
            db.SubmitChanges();
        }
    }

    public class GetException : Exception
    {
        private string cause;

        public GetException(string str)
        {
            cause = str;
        }

        public override string ToString()
        {
            return cause;
        }
    }
}
