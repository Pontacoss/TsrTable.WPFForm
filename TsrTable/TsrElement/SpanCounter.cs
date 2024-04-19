namespace TsrTable.RichTextBox.TsrElement
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
