using C1.WPF.FlexGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TsrTable.RichTextBox.TableData;
using TsrTable.TableData;

namespace TsrTable.FlexSheet
{
    internal static class FlexSheetTools
    {
        internal static void CreateTable(C1FlexSheet cfs,List<CellEntity> cellList)
        {
            var xmm = cfs.MergeManager as ExcelMergeManager;
            foreach (var cell in cellList)
            {
                cfs[cell.SheetIndexRow, cell.SheetIndexColumn] = cell.Name;
                var range = new CellRange(cell.SheetIndexRow, cell.SheetIndexColumn,
                    cell.SheetIndexRow + cell.SheetSpanRow - 1, cell.SheetIndexColumn + cell.SheetSpanColumn - 1);
                xmm.AddRange(range);

                if (cell.CellType == EnumCellType.ColumnHeaderTitle) SetColumnHeaderTitle(cfs, range);
                else if (cell.CellType == EnumCellType.ColumnHeader) SetColumnHeader(cfs, range);
                else if (cell.CellType == EnumCellType.RowHeader) SetRowHeader(cfs, range);
                else if(cell.CellType==EnumCellType.CellHeader) SetCellHeader(cfs, range);
                else SetDatCell(cfs, range);

                //cfs.Invalidate();
            }
        }

        private static void SetColumnHeaderTitle(C1FlexSheet cfs, CellRange range)
        {
            cfs.SetCellFormat(range.Cells, CellFormat.BorderThickness, new Thickness(1));
            cfs.SetCellFormat(range.Cells, CellFormat.BorderBrush, System.Windows.Media.Brushes.Black);
            cfs.SetCellFormat(range.Cells, CellFormat.Background, System.Windows.Media.Brushes.LightGray);
            cfs.SetCellFormat(range.Cells, CellFormat.HorizontalAlignment, HorizontalAlignment.Center);
            cfs.SetCellFormat(range.Cells, CellFormat.FontWeight, FontWeights.Bold);
        }

        private static void SetColumnHeader(C1FlexSheet cfs, CellRange range)
        {
            cfs.SetCellFormat(range.Cells, CellFormat.BorderThickness, new Thickness(1));
            cfs.SetCellFormat(range.Cells, CellFormat.BorderBrush, System.Windows.Media.Brushes.Black);
            cfs.SetCellFormat(range.Cells, CellFormat.Background, System.Windows.Media.Brushes.LightGray);
            cfs.SetCellFormat(range.Cells, CellFormat.HorizontalAlignment, HorizontalAlignment.Center);
        }

        private static void SetCellHeader(C1FlexSheet cfs, CellRange range)
        {
            cfs.SetCellFormat(range.Cells, CellFormat.BorderThickness, new Thickness(1));
            cfs.SetCellFormat(range.Cells, CellFormat.BorderBrush, System.Windows.Media.Brushes.Black);
            cfs.SetCellFormat(range.Cells, CellFormat.Background, System.Windows.Media.Brushes.LightGray);
            cfs.SetCellFormat(range.Cells, CellFormat.HorizontalAlignment, HorizontalAlignment.Center);
            cfs.SetCellFormat(range.Cells, CellFormat.FontWeight, FontWeights.Bold);
        }

        private static void SetDatCell(C1FlexSheet cfs, CellRange range)
        {
            cfs.SetCellFormat(range.Cells, CellFormat.BorderThickness, new Thickness(1));
            cfs.SetCellFormat(range.Cells, CellFormat.BorderBrush, System.Windows.Media.Brushes.Black);
        }

        private static void SetRowHeader(C1FlexSheet cfs, CellRange range)
        {
            cfs.SetCellFormat(range.Cells, CellFormat.BorderThickness, new Thickness(1));
            cfs.SetCellFormat(range.Cells, CellFormat.BorderBrush, System.Windows.Media.Brushes.Black);
        }



    }
}
