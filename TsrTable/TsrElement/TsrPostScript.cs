using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using System.Windows.Media;

namespace TsrTable.RichTextBox.TsrElement
{
    public sealed class TsrPostScript : ITsrElement, ITsrBlock
    {
        private Brush _color;
        public string Color => _color.ToString();

        public Collection<ITsrElement> Children { get; }
            = new Collection<ITsrElement>();

        [JsonConstructor]
        public TsrPostScript(string color,
            Collection<ITsrElement> children)
        {
            _color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            Children = children;

        }

        public TsrPostScript(Brush color)
        {
            _color = color;
        }

        public RtfObject ToWord()
        {
            throw new System.NotImplementedException();
        }

        public void ToFlexSheet(C1FlexSheet cfs)
        {
            throw new System.NotImplementedException();
        }

        public void ToExcel(C1XLBook book)
        {
            throw new System.NotImplementedException();
        }

        public C1TextElement GetRtbInstance()
            => new RtbPostScript(_color);

    }
}
