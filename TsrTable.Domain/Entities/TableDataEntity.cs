using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsrTable.Domain.ValueObjects;

namespace TsrTable.Domain.Entities
{
    public class TableDataEntity
    {
        public int RowIndex { get; }
        public int ColumnIndex { get; }
        public string Conditions { get; }
        public double Criteria { get; set; }
        public int Operators { get; set; }
        public double Tolerance { get; set; }
        public int ToleranceType { get; set; }


        public TableDataEntity(int rowIndex,int columnIndex,string conditions)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            Conditions = conditions;
        }
    }
}