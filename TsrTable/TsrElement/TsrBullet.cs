using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace TsrTable.TsrElement
{
    internal sealed class TsrBullet : ITsrElement, ITsrBlock
    {
        #region Property
        public TextMarkerStyle MarkerStyle { get; }
        public Collection<ITsrElement> Children { get; }
            = new Collection<ITsrElement>();
        #endregion

        #region Constractor
        internal TsrBullet(TextMarkerStyle markerStyle)
        {
            MarkerStyle = markerStyle;
        }

        [JsonConstructor]
        internal TsrBullet(
            TextMarkerStyle markerStyle,
            Collection<ITsrElement> children) : this(markerStyle)
        {
            Children = children;
        }
        #endregion


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
            => new C1List() { MarkerStyle = this.MarkerStyle, };

    }
}
