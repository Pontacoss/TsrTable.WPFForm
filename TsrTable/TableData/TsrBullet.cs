﻿using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace TsrTable.TableData
{
    public sealed class TsrBullet : ITsrElement, ITsrBlock
    {
        public TextMarkerStyle MarkerStyle { get; }
        public Collection<ITsrElement> Children { get; }
            = new Collection<ITsrElement>();

        [JsonConstructor]
        public TsrBullet(
            TextMarkerStyle markerStyle,
            Collection<ITsrElement> children) : this(markerStyle)
        {
            Children = children;
        }

        public TsrBullet(TextMarkerStyle markerStyle)
        {
            MarkerStyle = markerStyle;
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
            => new C1List() { MarkerStyle = this.MarkerStyle, };

    }
}
