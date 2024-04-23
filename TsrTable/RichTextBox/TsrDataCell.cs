using C1.WPF.RichTextBox.Documents;
using System.Collections.Generic;
using TsrTable.Domain.ValueObjects;
using TsrTable.TsrElement;

namespace TsrTable.RichTextBox
{
    public sealed class TsrDataCell : TsrCell
    {
        public Conditions Conditions => _cellEntity.Conditions;
        public TsrDataCell() : base() { }
        internal TsrDataCell(CellEntity cellEntity) : base(cellEntity) 
        {
            TextAlignment = C1TextAlignment.Right;
            VerticalAlignment = C1VerticalAlignment.Middle;
        }

    }
}
