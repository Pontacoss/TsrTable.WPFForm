using C1.WPF.RichTextBox.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace TsrTable.C1RichTextBox.TableData
{
    interface IContainer
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

        // ContainerBaseに移動
        //CellEntity CreateCellHeader(int columnHeaderHeight, int columnIndex);
        //int CreateRowHeaders(List<CellEntity> list, int columnHeaderHeight, int columnIndex);

        int CreateColumnHeaders(List<CellEntity> list, int rowIndex, int columnIndex);
        int CreateColumnContainerTitles(List<CellEntity> list, int rowIndex, int columnIndex);
    }
}
