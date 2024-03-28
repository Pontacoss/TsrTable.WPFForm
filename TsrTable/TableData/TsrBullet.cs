using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System;
using System.Collections.Generic;
using TsrTable.RichTextBox;

namespace TsrTable.TableData
{
    public sealed class TsrBullet : ITsrElement
    {
        public TextMarkerStyle MarkerStyle { get; }
        public List<string> ListItem { get; }
        public TsrBullet(RtbBullet rtbBullet)
        {

        }
        public void ToExcel(C1XLBook book)
        {
            throw new NotImplementedException();
        }

        public void ToFlexSheet(C1FlexSheet cfs)
        {
            throw new NotImplementedException();
        }

        public C1TextElement ToRtb()
        {
            throw new NotImplementedException();
        }

        public RtfObject ToWord()
        {
            throw new NotImplementedException();
        }
    }
}
