using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Wypozyczalnia.Database
{
    public static class Extensions
    {
        // Konwersja IEnumerable<T> na DataTable
        public static DataTable ToDataTable<T>(this IEnumerable<T> enumberable)
        {
            DataTable table = new DataTable("Generated");

            T first = enumberable.FirstOrDefault();
            if (first == null)
                return table;

            PropertyInfo[] properties = first.GetType().GetProperties();
            foreach (PropertyInfo pi in properties)
                table.Columns.Add(pi.Name, pi.PropertyType);

            foreach (T t in enumberable)
            {
                DataRow row = table.NewRow();
                foreach (PropertyInfo pi in properties)
                    row[pi.Name] = t.GetType().InvokeMember(pi.Name, BindingFlags.GetProperty, null, t, null);
                table.Rows.Add(row);
            }

            return table;
        }

    }

}
