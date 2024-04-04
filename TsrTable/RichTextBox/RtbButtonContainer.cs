using C1.WPF.RichTextBox.Documents;
using System.Windows.Controls;

namespace TsrTable.RichTextBox
{
    public class RtbButtonContainer : C1InlineUIContainer
    {
        public RtbButtonContainer() { }
        public RtbButtonContainer(Button button)
        {
            Content = button;
        }
    }
}
