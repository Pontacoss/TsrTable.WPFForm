﻿using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System.Collections.ObjectModel;

namespace TsrTable.TableData
{
    public sealed class TsrBulletItem : ITsrElement, ITsrBlock
    {
        public Collection<ITsrElement> Children { get; }
            = new Collection<ITsrElement>();
        public TsrBulletItem() { }

        public RtfObject ToWord()
        {
            throw new System.NotImplementedException();
        }

        public void ToFlexSheet(C1FlexSheet cfs)
        {
            throw new System.NotImplementedException();
        }

        public void ToExcel(C1XLBook book)
        {
            throw new System.NotImplementedException();
        }

        public C1TextElement GetRtbInstance()
            => new C1ListItem();

    }
}
