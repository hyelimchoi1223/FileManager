using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LinkGenesis.FileDataManager.Model
{
    public class ExcelInfo
    {
        private DataSet dataSet;
        public DataTable dataTable;

        public ExcelInfo(DataSet dataSet)
        {
            this.dataSet = dataSet;
            dataTable = ConvertToDataTable(dataSet);
        }

        private DataTable ConvertToDataTable(DataSet dataSet)
        {
            DataTable result = new DataTable();
            result.Columns.AddRange(ConvertToColumn(GetFieldRow()).ToArray());
            DataRowCollection rows = dataSet.Tables[0].Rows;
            rows.RemoveAt(0);
            foreach (DataRow item in rows)
            {
                result.Rows.Add(item.ItemArray);
            }
            return result;
        }

        public List<object> GetFieldRow()
        {
            return dataSet.Tables[0].Rows[0].ItemArray.ToList();
        }

        private List<DataColumn> ConvertToColumn(List<object> list)
        {
            List<DataColumn> result = new List<DataColumn>();
            for(int i = 0; i<list.Count;i++)
            {
                DataColumn column = new DataColumn();
                column.ColumnName = list[i].ToString();
                result.Add(column);
            }            
            return result;
        }

        public bool ColumnContains(string columnName)
        {
            return dataTable.Columns.Contains(columnName);
        }
    }
}
