using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace eRSN_New.Services
{
    public class ListtoDataTableConverter
    {
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in Props)
            {
                Type ColumnType = prop.PropertyType;

                if (ColumnType.IsGenericType)
                {
                    ColumnType = ColumnType.GetGenericArguments()[0];
                } // end if: ColumnType is generic

                dataTable.Columns.Add(prop.Name, ColumnType);
            } // end foeach: Props

            foreach (T item in items)
            {
                DataRow dr = dataTable.NewRow();
                dr.BeginEdit();

                foreach (PropertyInfo prop in Props)
                {
                    if (prop.GetValue(item, null) != null)
                    {
                        dr[prop.Name] = prop.GetValue(item, null);
                    } // end if: prop.GetValue is not null
                } // end foreach: Props

                dr.EndEdit();
                dataTable.Rows.Add(dr);
            } // end foreach: items

            return dataTable;
        } // end DataTable: ToDataTable
    } // end class: ListtoDataTableConverter
} // end namespace: eRSN_New.Services