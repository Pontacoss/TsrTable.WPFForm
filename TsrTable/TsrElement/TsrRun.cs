using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System;

namespace TsrTable.RichTextBox.TsrElement

{
    public class TsrRun : ITsrElement
    {
        public string Text { get; }
        public TsrRun(string text)
        {
            Text = text;
        }
        public void ToExcel(C1XLBook book)
        {
            throw new NotImplementedException();
        }

        public void ToFlexSheet(C1FlexSheet cfs)
        {
            throw new NotImplementedException();
        }

        public RtfObject ToWord()
        {
            throw new NotImplementedException();
        }

        public C1TextElement GetRtbInstance()
            => new C1Run() { Text = this.Text };

    }
}
