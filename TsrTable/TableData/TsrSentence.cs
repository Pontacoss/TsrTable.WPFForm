using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace TsrTable.TableData
{
    public class TsrSentence : ITsrElement, ITsrBlock
    {
        public Collection<ITsrElement> Children { get; set; }
            = new Collection<ITsrElement>();
        public TsrSentence() { }
        [JsonConstructor]
        public TsrSentence(Collection<ITsrElement> children)
        {
            Children = children;
        }

        public TsrSentence(C1Document doc)
        {
            foreach (var child in doc.Children)
            {
                var element = child.ToTsr();
                if (element != null)
                {
                    this.Children.Add(element);
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


        public RtfObject ToWord()
        {
            throw new NotImplementedException();
        }

        public C1TextElement GetRtbInstance() => new C1Document();

    }
}
