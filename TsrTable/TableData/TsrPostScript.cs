using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System.Windows;
using System.Windows.Media;
using TsrTable.TableData;

namespace TsrTable.RichTextBox.TableData
{
    public sealed class TsrPostScript : ITsrElement
    {
        public string Text { get; }

        public Color Color { get; }

        private RoutedEventHandler _action;

        public TsrPostScript(string text, Color color)
        {
            Text = text;
            Color = color;
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

        public C1TextElement ToRtb()
        {
            return new RtbPostScript(Text, Color);
        }
    }
}
