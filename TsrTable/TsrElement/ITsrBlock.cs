using System.Collections.ObjectModel;

namespace TsrTable.RichTextBox.TsrElement
{
    public interface ITsrBlock
    {
        Collection<ITsrElement> Children { get; }
    }
}
