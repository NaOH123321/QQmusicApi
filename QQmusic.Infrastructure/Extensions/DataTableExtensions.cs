using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Text;

namespace QQmusic.Infrastructure.Extensions
{
    public static class DataTableExtensions
    {
        public static IEnumerable<dynamic> AsDynamicEnumerable(this DataTable table)
        {
            // Validate argument here..

            return table.AsEnumerable().Select(row => new DynamicRow(row));
        }

        private sealed class DynamicRow : DynamicObject
        {
            private readonly DataRow _row;

            internal DynamicRow(DataRow row) { _row = row; }

            public int Count => _row.ItemArray.Length;

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                var retVal = false;
                var columnName = "";

                foreach (DataColumn column in _row.Table.Columns)
                {
                    retVal = column.ColumnName.Contains(binder.Name);
                    if (!retVal) continue;
                    columnName = column.ColumnName;
                    break;
                }

                result = retVal ? _row[columnName] : null;
                return retVal;
            }
        }
    }
}
