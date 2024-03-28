using C1.WPF.RichTextBox.Documents;
using TsrTable.Domain.ValueObjects;
using TsrTable.TableData;

namespace TsrTable.RichTextBox.TableData
{
    public sealed class CellEntity
    {
        public EnumCellType CellType { get; }
        public Conditions Conditions { get; }
        public string Value { get; private set; } = string.Empty;
        public int RowIndex { get; }
        public int ColumnIndex { get; }
        public int RowSpan { get; }
        public int ColumnSpan { get; }

        public int SheetSpanRow { get; set; }
        public int SheetSpanColumn { get; set; }
        public int SheetIndexRow { get; set; }
        public int SheetIndexColumn { get; set; }
        public C1Length Width { get; set; }
        public C1Length Height { get; set; }

        /// <summary>
        /// ヘッダーセル用コンストラクタ
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="cellType"></param>
        /// <param name="value"></param>
        /// <param name="rowSpan"></param>
        /// <param name="columnSpan"></param>
        public CellEntity(int rowIndex, int columnIndex,
            EnumCellType cellType, string value,
            int rowSpan, int columnSpan)
        {
            RowIndex = rowIndex;
            SheetIndexRow = rowIndex;
            ColumnIndex = columnIndex;
            SheetIndexColumn = columnIndex;
            CellType = cellType;
            Value = value ?? string.Empty;
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
        public CellEntity(int rowIndex, int columnIndex,
            EnumCellType cellType, int rowSpan,
            int columnSpan, Conditions condition)
        {
            RowIndex = rowIndex;
            SheetIndexRow = rowIndex;
            ColumnIndex = columnIndex;
            SheetIndexColumn = columnIndex;
            CellType = cellType;
            Conditions = condition;
            RowSpan = rowSpan;
            SheetSpanRow = rowSpan;
            ColumnSpan = columnSpan;
            SheetSpanColumn = columnSpan;
        }

        public void SetSheetSpanColumn(int gap)
        {
            SheetSpanColumn += gap;
        }
        public bool CanChangeSpan(int gap)
        {
            if (SheetSpanColumn + gap == 0) return false;
            return true;
        }

        public bool CanMove(int gap)
        {
            if (SheetSpanColumn + SheetIndexColumn + gap >
                TsrFacade.FlexSheetColumnCount) return false;
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

        internal void SetValue(string value)
        {
            if (value == null) return;
            Value = value;
        }
    }
}
