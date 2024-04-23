using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace TsrTable.TsrElement
{
    internal sealed class TsrParagraph : ITsrElement, ITsrBlock
    {
        public Collection<ITsrElement> Children { get; }
            = new Collection<ITsrElement>();

        [JsonConstructor]
        internal TsrParagraph(Collection<ITsrElement> children)
        {
            Children = children;
        }

        internal TsrParagraph() { }

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
            => new C1Paragraph()
            {
                Padding = new System.Windows.Thickness(0),
                Margin = new System.Windows.Thickness(0),
            };

    }
}
