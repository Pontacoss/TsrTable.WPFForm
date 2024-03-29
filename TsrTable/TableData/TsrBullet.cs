using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System;
using System.Collections.ObjectModel;
using TsrTable.RichTextBox;

namespace TsrTable.TableData
{
    public sealed class TsrBullet : ITsrElement, ITsrBlock
    {
        public TextMarkerStyle MarkerStyle { get; }
        public Collection<ITsrElement> Children { get; }
            = new Collection<ITsrElement>();
        public TsrBullet(RtbBullet rtbBullet)
        {
            MarkerStyle = rtbBullet.MarkerStyle;
            //foreach (var child in rtbBullet.Children)
            //{
            //    Children.Add(child.ToTsr());
            //}
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
            => new RtbBullet() { MarkerStyle = this.MarkerStyle, };

    }
}
