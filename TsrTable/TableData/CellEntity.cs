using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TsrTable.TableData;

namespace TsrTable.C1RichTextBox.TableData
{
    public sealed class CellEntity
    {
        public int RowIndex { get; }
        public int ColumnIndex { get; }
        public EnumCellType CellType { get; }
        public string Name { get; }
        public int RowSpan { get; }
        public int ColumnSpan { get; }
        public string Conditions { get; }
        public int SheetSpanRow { get;  set; }
        public int SheetSpanColumn { get;  set; }
        public int SheetIndexRow { get;  set; }
        public int SheetIndexColumn { get;  set; }

        /// <summary>
        /// ヘッダーセル用コンストラクタ
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="cellType"></param>
        /// <param name="name"></param>
        /// <param name="rowSpan"></param>
        /// <param name="columnSpan"></param>
        public CellEntity(int rowIndex,int columnIndex, EnumCellType cellType, string name, int rowSpan, int columnSpan)
        {
            RowIndex = rowIndex;
            SheetIndexRow=rowIndex;
            ColumnIndex = columnIndex;
            SheetIndexColumn=columnIndex;
            CellType = cellType;
            Name = name ?? string.Empty;
            RowSpan = rowSpan;
            SheetSpanRow = rowSpan;
            ColumnSpan = columnSpan;
            SheetSpanColumn = columnSpan;
        }
        /// <summary>
        /// DataCell用コンストラクタ
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="cellType"></param>
        /// <param name="rowSpan"></param>
        /// <param name="columnSpan"></param>
        /// <param name="condition"></param>
        public CellEntity(int rowIndex, int columnIndex, EnumCellType cellType, int rowSpan, int columnSpan, string condition)
        {
            RowIndex = rowIndex;
            SheetIndexRow = rowIndex;
            ColumnIndex = columnIndex;
            SheetIndexColumn = columnIndex;
            CellType = cellType;
            Conditions = condition ?? string.Empty;
            RowSpan = rowSpan;
            SheetSpanRow = rowSpan;
            ColumnSpan = columnSpan;
            SheetSpanColumn = columnSpan;
        }

        public void SetSheetSpanColumn(int gap)
        {
            SheetSpanColumn +=gap;
        }
        public bool CanChangeSpan(int gap)
        {
            if (SheetSpanColumn + gap == 0) return false;
            return true;
        }

        public bool CanMove(int gap)
        {
            if (SheetSpanColumn + SheetIndexColumn + gap > 26) return false;
            return true;
        }

        public void SetSheetSpanRow(int gap)
        {
            SheetSpanRow += gap;
        }
        public bool CanChangeSpanRow(int gap)
        {
            if (SheetSpanRow + gap == 0) return false;
            return true;
        }
    }
}
