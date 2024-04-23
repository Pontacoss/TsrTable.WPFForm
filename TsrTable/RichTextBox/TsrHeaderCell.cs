using C1.WPF.RichTextBox.Documents;
using TsrTable.TsrElement;

namespace TsrTable.RichTextBox
{
    public sealed class TsrHeaderCell : TsrCell
    {
        public TsrHeaderCell() : base() { }

        internal TsrHeaderCell(CellEntity cellEntity) : base(cellEntity) 
        {
            VerticalAlignment = C1VerticalAlignment.Middle;
            RowSpan = _cellEntity.RowSpan;
            ColumnSpan = _cellEntity.ColumnSpan;
        }
    }
}
