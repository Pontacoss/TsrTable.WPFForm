using System.Collections.Generic;
using System.Linq;

namespace TsrTable.TsrElement
{
    /// <summary>
    /// 表作成に必要なHeaderの情報を格納するクラス
    /// </summary>
    public sealed class TableContent
    {
        public string TableName { get; }
        public int ColumnHeaderHeight { get; }
        public int RowHeaderHeight { get; }
        public int ColumnHeaderWidth { get; }
        public int RowHeaderWidth { get; }
        public SpanCounter RowSpanCounter { get; }
        public SpanCounter ColumnSpanCounter { get; }
        public IEnumerable<HeaderBase> RowHeaders { get; }
        public IEnumerable<HeaderBase> ColumnHeaders { get; }

        /// <summary>
        ///  表作成に必要なデータを格納するクラス
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="rowHeaders"></param>
        /// <param name="columnHeaders"></param>
        public TableContent(string tableName,
            IEnumerable<HeaderBase> rowHeaders,
            IEnumerable<HeaderBase> columnHeaders)
        {
            TableName = tableName;
            RowHeaders = rowHeaders;
            ColumnHeaders = columnHeaders;
            //　列ヘッダーの行数を算出
            var rowSpanCounter = new SpanCounter();
            if (this.RowHeaders != null)
            {
                foreach (var container in this.RowHeaders.OfType<IContainer>())
                {
                    rowSpanCounter = container.GetHeaderWidth(rowSpanCounter);
                    RowHeaderWidth += container.GetDepth();
                }
            }
            RowHeaderHeight = rowSpanCounter.GetNodesCount();
            RowSpanCounter = rowSpanCounter;

            //　表の列ヘッダー部分の高さColumnHeaderHeightを算出
            var visibleTitleNumber =
                this.ColumnHeaders.OfType<IContainer>()
                .Count(x => x.IsTitleVisible == true);
            ColumnHeaderHeight =
                this.ColumnHeaders.Sum(x => x.GetDepth())
                + visibleTitleNumber;

            // 表の列ヘッダー部分の幅を算出
            var columnSpanCounter = new SpanCounter();
            if (ColumnHeaders != null)
            {
                foreach (var container in ColumnHeaders.OfType<IContainer>())
                {
                    columnSpanCounter = container.GetHeaderWidth(columnSpanCounter);
                }
            }
            ColumnSpanCounter = columnSpanCounter;
            ColumnHeaderWidth = columnSpanCounter.GetNodesCount();

            SetUnitSizeAndRepeat(RowHeaders.OfType<IContainer>(), RowSpanCounter);
            SetUnitSizeAndRepeat(ColumnHeaders.OfType<IContainer>(), ColumnSpanCounter);

        }

        private void SetUnitSizeAndRepeat(IEnumerable<IContainer> list, SpanCounter spanConuter)
        {
            // 各ContainerのUnitSizeとRepeat回数の取得
            int repeat = spanConuter.BlockSpan;
            int repaetHeaderUnitSize = spanConuter.RepeatSpan;
            foreach (var container in list)
            {
                repaetHeaderUnitSize =
                    container.SetUnitSize(
                        spanConuter,
                        repaetHeaderUnitSize);
                repeat = container.SetRepeat(repeat);
            }
        }
    }
}
