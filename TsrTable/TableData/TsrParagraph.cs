using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System;
using System.Collections.ObjectModel;
using TsrTable.RichTextBox;

namespace TsrTable.TableData
{
    public class TsrParagraph : ITsrElement
    {
        public Collection<ITsrElement> Children { get; } = new Collection<ITsrElement>();
        public TsrParagraph(C1Paragraph paragraph)
        {
            foreach (var child in paragraph.Children)
            {
                if (child is IRtbElement element)
                {
                    this.Children.Add(element.ToTsr());
                }
                else
                {
                    this.Children.Add(TsrSentenceTools.ConvertC1ToTsr(child));
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
