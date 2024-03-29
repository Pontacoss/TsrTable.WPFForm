using System.Collections.ObjectModel;

namespace TsrTable.TableData
{
    public interface ITsrBlock
    {
        Collection<ITsrElement> Children { get; }
    }
}
