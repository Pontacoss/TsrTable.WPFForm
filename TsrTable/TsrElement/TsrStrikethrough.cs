using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System;
using System.Text.Json.Serialization;

namespace TsrTable.TsrElement
{
    internal sealed class TsrStrikethrough : ITsrElement
    {
        public string Text { get; }

        [JsonConstructor]
        internal TsrStrikethrough(string text)
        {
            Text = text;
        }

        public RtfObject ToWord()
        {
            throw new NotImplementedException();
        }

        public void ToFlexSheet(C1FlexSheet cfs)
        {
            throw new NotImplementedException();
        }

        public void ToExcel(C1XLBook book)
        {
            throw new NotImplementedException();
        }

        public C1TextElement GetRtbInstance()
        {
            var run = new C1Run() { Text = this.Text };
            run.TextDecorations = C1TextDecorations.Strikethrough;
            return run;
        }
    }
}
