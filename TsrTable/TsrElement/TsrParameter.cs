using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System;
using System.Text.Json.Serialization;
using TsrTable.RichTextBox;

namespace TsrTable.TsrElement
{
    internal class TsrParameter : ITsrElement
    {
        public string Name { get; }

        [JsonConstructor]
        internal TsrParameter(string name)
        {
            Name = name;
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
    => new RtbParameter(Name);

    }
}
