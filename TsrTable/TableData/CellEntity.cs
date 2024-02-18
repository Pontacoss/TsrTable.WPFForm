using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TsrTable.C1RichTextBox.TableData
{
    public sealed class CellEntity
    {
        public int RowIndex { get; }
        public int ColumnIndex { get; }
        public int CellType { get; }
        public string Name { get; }
        public int RowSpan { get; }
        public int ColumnSpan { get; }
        public string Conditions { get; }

        /// <summary>
        /// ヘッダーセル用コンストラクタ
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="cellType"></param>
        /// <param name="name"></param>
        /// <param name="rowSpan"></param>
        /// <param name="columnSpan"></param>
        public CellEntity(int rowIndex,int columnIndex, int cellType, string name, int rowSpan, int columnSpan)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            CellType = cellType;
            Name = name ?? string.Empty;
            RowSpan = rowSpan;
            ColumnSpan = columnSpan;
        }
        /// <summary>
        /// DataCell用コンストラクタ
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="cellType"></param>
        /// <param name="rowSpan"></param>
        /// <param name="columnSpan"></param>
        /// <param name="condition"></param>
        public CellEntity(int rowIndex, int columnIndex, int cellType, int rowSpan, int columnSpan, string condition)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            CellType = cellType;
            Conditions = condition ?? string.Empty;
            RowSpan = rowSpan;
            ColumnSpan = columnSpan;
        }
    }
}
