using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TsrTable.RichTextBox.TableData
{
    public sealed class SpanCounter
    {
        public int BlockSpan { get; set; } = 1;
        public int RepeatSpan { get; set; } = 1;

        internal int GetNodesCount()
        {
            return BlockSpan * RepeatSpan;
        }
    }
}
