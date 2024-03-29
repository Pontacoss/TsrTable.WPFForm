using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System;
using System.Collections.ObjectModel;

namespace TsrTable.TableData
{
    public class TsrSentence : ITsrElement, ITsrBlock
    {
        public Collection<ITsrElement> Children { get; set; } = new Collection<ITsrElement>();
        public TsrSentence(C1Document doc)
        {
            foreach (var child in doc.Children)
            {
                this.Children.Add(child.ToTsr());
            }
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

        public C1TextElement GetRtbInstance() => new C1Paragraph();

    }
}
