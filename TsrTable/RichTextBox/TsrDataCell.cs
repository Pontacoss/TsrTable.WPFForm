using C1.WPF.RichTextBox.Documents;
using TsrTable.RichTextBox.TableData;

namespace TsrTable.RichTextBox
{
    public sealed class TsrDataCell : C1TableCell
    {
        private CellEntity _cellEntity;
        public string Conditions => _cellEntity.Conditions;
        public int RowIndex => _cellEntity.RowIndex;
        public int ColumnIndex => _cellEntity.ColumnIndex;
        public TsrDataCell() : base() { }
        internal TsrDataCell(CellEntity cellEntity) : base() 
        {
            _cellEntity = cellEntity;
            TextAlignment = C1TextAlignment.Right;
            VerticalAlignment = C1VerticalAlignment.Middle;
        }

    }
}
