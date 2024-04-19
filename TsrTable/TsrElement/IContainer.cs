using System.Collections.Generic;

namespace TsrTable.RichTextBox.TsrElement
{
    public interface IContainer
    {
        bool IsTitleVisible { get; }
        bool IsMeasurementItem { get; }
        bool IsRepeat { get; }

        int GetDepth();
        int GetSpanSum();
        SpanCounter GetHeaderWidth(SpanCounter spanCounter);
        /// <summary>
        /// 1セルあたりのサイズを設定
        /// </summary>
        /// <param name="spanCounter"></param>
        /// <param name="repaetCellHeight"></param>
        /// <returns></returns>
        int SetUnitSize(SpanCounter spanCounter, int repaetCellHeight);
        int SetRepeat(int repeat);
        string GetConditionStringByContainer(int Index);

        int CreateColumnHeaders(List<CellEntity> list, int rowIndex, int columnIndex);
        int CreateColumnContainerTitles(List<CellEntity> list, int rowIndex, int columnIndex);
    }
}
