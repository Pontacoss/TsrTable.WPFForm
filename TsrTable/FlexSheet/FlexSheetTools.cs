using C1.WPF.FlexGrid;
using System.Collections.Generic;
using System.Windows;
using TsrTable.Domain.Common;
using TsrTable.Domain.Entities;
using TsrTable.RichTextBox.TableData;
using TsrTable.TableData;

namespace TsrTable.FlexSheet
{
    internal static class FlexSheetTools
    {
        private static double _flexSheetFontSize = 15;

        internal static void CreateTable(
            C1FlexSheet cfs,
            List<CellEntity> cellList,
            List<TableDataEntity> datas)
        {
            var xmm = cfs.MergeManager as ExcelMergeManager;
            var allCells = new CellRange(0, 0, cfs.Rows.Count, cfs.Columns.Count);
            xmm.RemoveRange(allCells);

            foreach (var cellData in cellList)
            {
                cfs[cellData.SheetIndexRow, cellData.SheetIndexColumn] =
                    TsrTableTools.GetCellContent(cellData, datas, EnumTsrDocumentType.TestReport);

                var range = new CellRange(cellData.SheetIndexRow, cellData.SheetIndexColumn,
                    cellData.SheetIndexRow + cellData.SheetSpanRow - 1,
                    cellData.SheetIndexColumn + cellData.SheetSpanColumn - 1);
                xmm.AddRange(range);

                if (cellData.CellType == EnumCellType.ColumnHeaderTitle) SetColumnHeaderTitle(cfs, range);
                else if (cellData.CellType == EnumCellType.ColumnHeader) SetColumnHeader(cfs, range);
                else if (cellData.CellType == EnumCellType.RowHeader) SetRowHeader(cfs, range);
                else if (cellData.CellType == EnumCellType.CellHeader) SetCellHeader(cfs, range);
                else SetDataCell(cfs, range);
                //cfs.Invalidate();

            }
        }

        private static void SetColumnHeaderTitle(C1FlexSheet cfs, CellRange range)
        {
            cfs.SetCellFormat(range.Cells, CellFormat.FontSize, _flexSheetFontSize);
            cfs.SetCellFormat(range.Cells, CellFormat.BorderThickness, new Thickness(1));
            cfs.SetCellFormat(range.Cells, CellFormat.BorderBrush, System.Windows.Media.Brushes.Black);
            cfs.SetCellFormat(range.Cells, CellFormat.Background, System.Windows.Media.Brushes.LightGray);
            cfs.SetCellFormat(range.Cells, CellFormat.HorizontalAlignment, HorizontalAlignment.Center);
            cfs.SetCellFormat(range.Cells, CellFormat.FontWeight, FontWeights.Bold);
        }

        private static void SetColumnHeader(C1FlexSheet cfs, CellRange range)
        {
            cfs.SetCellFormat(range.Cells, CellFormat.FontSize, _flexSheetFontSize);
            cfs.SetCellFormat(range.Cells, CellFormat.BorderThickness, new Thickness(1));
            cfs.SetCellFormat(range.Cells, CellFormat.BorderBrush, System.Windows.Media.Brushes.Black);
            cfs.SetCellFormat(range.Cells, CellFormat.Background, System.Windows.Media.Brushes.LightGray);
            cfs.SetCellFormat(range.Cells, CellFormat.HorizontalAlignment, HorizontalAlignment.Center);
        }

        private static void SetCellHeader(C1FlexSheet cfs, CellRange range)
        {
            cfs.SetCellFormat(range.Cells, CellFormat.FontSize, _flexSheetFontSize);
            cfs.SetCellFormat(range.Cells, CellFormat.BorderThickness, new Thickness(1));
            cfs.SetCellFormat(range.Cells, CellFormat.BorderBrush, System.Windows.Media.Brushes.Black);
            cfs.SetCellFormat(range.Cells, CellFormat.Background, System.Windows.Media.Brushes.LightGray);
            cfs.SetCellFormat(range.Cells, CellFormat.HorizontalAlignment, HorizontalAlignment.Center);
            cfs.SetCellFormat(range.Cells, CellFormat.FontWeight, FontWeights.Bold);
        }

        private static void SetDataCell(C1FlexSheet cfs, CellRange range)
        {
            cfs.SetCellFormat(range.Cells, CellFormat.FontSize, _flexSheetFontSize);
            cfs.SetCellFormat(range.Cells, CellFormat.BorderThickness, new Thickness(1));
            cfs.SetCellFormat(range.Cells, CellFormat.BorderBrush, System.Windows.Media.Brushes.Black);
            cfs.SetCellFormat(range.Cells, CellFormat.HorizontalAlignment, HorizontalAlignment.Center);
        }

        private static void SetRowHeader(C1FlexSheet cfs, CellRange range)
        {
            cfs.SetCellFormat(range.Cells, CellFormat.FontSize, _flexSheetFontSize);
            cfs.SetCellFormat(range.Cells, CellFormat.BorderThickness, new Thickness(1));
            cfs.SetCellFormat(range.Cells, CellFormat.BorderBrush, System.Windows.Media.Brushes.Black);
        }
    }
}
