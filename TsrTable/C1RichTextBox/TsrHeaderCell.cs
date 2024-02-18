﻿using C1.WPF.RichTextBox.Documents;
using TsrTable.C1RichTextBox.TableData;

namespace TsrTable.C1RichTextBox
{
    public sealed class TsrHeaderCell : C1TableCell
    {
        private CellEntity _cellEntity;
        public int RowIndex => _cellEntity.RowIndex;
        public int ColumnIndex => _cellEntity.ColumnIndex;

        public TsrHeaderCell() : base() { }

        public TsrHeaderCell(CellEntity cellEntity) : base() 
        {
            _cellEntity = cellEntity;
            VerticalAlignment = C1VerticalAlignment.Middle;
            RowSpan = _cellEntity.RowSpan;
            ColumnSpan = _cellEntity.ColumnSpan;
        }
    }
}
