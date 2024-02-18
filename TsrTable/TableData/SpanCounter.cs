using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TsrTable.C1RichTextBox.TableData
{
    public class SpanCounter
    {
        public int BlockSpan { get; set; } = 1;
        public int RepeatSpan { get; set; } = 1;

        public int GetNodesCount()
        {
            return BlockSpan * RepeatSpan;
        }
    }
}
