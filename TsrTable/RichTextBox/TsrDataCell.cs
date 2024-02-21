using C1.WPF.RichTextBox.Documents;
using TsrTable.RichTextBox.TableData;

namespace TsrTable.RichTextBox
{
    public sealed class TsrDataCell : TsrCell
    {
        public string Conditions => _cellEntity.Conditions;
        public TsrDataCell() : base() { }
        internal TsrDataCell(CellEntity cellEntity) : base(cellEntity) 
        {
            TextAlignment = C1TextAlignment.Right;
            VerticalAlignment = C1VerticalAlignment.Middle;
        }

    }
}
