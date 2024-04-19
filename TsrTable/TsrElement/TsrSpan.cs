using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace TsrTable.RichTextBox.TsrElement
{
    internal class TsrSpan : ITsrElement, ITsrBlock
    {
        public Collection<ITsrElement> Children { get; }
            = new Collection<ITsrElement>();

        [JsonConstructor]
        public TsrSpan(Collection<ITsrElement> children)
        {
            Children = children;
        }

        public TsrSpan() { }

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
            => new C1Span()
            {
                Padding = new System.Windows.Thickness(0),
                Margin = new System.Windows.Thickness(0),
            };

    }
}
