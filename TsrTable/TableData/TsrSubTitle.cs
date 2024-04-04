using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System;
using System.Collections.ObjectModel;
using TsrTable.RichTextBox;

namespace TsrTable.TableData
{
    public sealed class TsrSubTitle : ITsrBlock, ITsrElement
    {
        public string SubTitle { get; }
        public Collection<ITsrElement> Children { get; }
            = new Collection<ITsrElement>();

        public TsrSubTitle(string subTitle)
        {
            SubTitle = subTitle;
        }
        public C1TextElement GetRtbInstance() => new RtbSubTitle(SubTitle);


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
    }
}
