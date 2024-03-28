using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System;
using System.Collections.ObjectModel;

namespace TsrTable.TableData
{
    public class TsrSentence : ITsrElement
    {
        public Collection<ITsrElement> Children { get; set; } = new Collection<ITsrElement>();
        public TsrSentence(C1Document doc)
        {
            foreach (var child in doc.Children)
            {
                if (child is C1Paragraph paragraph)
                {
                    this.Children.Add(new TsrParagraph(paragraph));
                }
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

        public C1TextElement ToRtb()
        {
            var element = new C1Paragraph();
            foreach (ITsrElement child in Children)
            {
                element.Children.Add(child.ToRtb());
            }
            return element;
        }

        public RtfObject ToWord()
        {
            throw new NotImplementedException();
        }
    }
}
