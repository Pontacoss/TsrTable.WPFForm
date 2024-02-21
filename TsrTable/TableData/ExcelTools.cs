using C1.WPF.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TsrTable.RichTextBox.TableData;

namespace TsrTable.TableData
{
    internal class ExcelTools
    {
        internal static void CreateTable(C1XLBook book,XLSheet sheet, List<CellEntity> cellList)
        {
            sheet.DefaultColumnWidth = 500;
            XLStyle style = new XLStyle(book)
            {
                Font = new XLFont("MS UI Gothic", 10, false, false),
            };
            style.SetBorderStyle(XLLineStyleEnum.Thin);
            style.SetBorderColor(System.Windows.Media.Colors.Black);

            //var xmm = sheet.MergeManager as ExcelMergeManager;
            foreach (var cell in cellList)
            {
                sheet[cell.SheetIndexRow, cell.SheetIndexColumn].Value = cell.Value;
                var range = new XLCellRange(cell.SheetIndexRow, cell.SheetIndexRow + cell.SheetSpanRow - 1, 
                    cell.SheetIndexColumn, cell.SheetIndexColumn + cell.SheetSpanColumn - 1);
                
                sheet.MergedCells.Add(range);
                range.Style = style;

                //if (cell.CellType == EnumCellType.ColumnHeaderTitle) SetColumnHeaderTitle(sheet, range);
                //else if (cell.CellType == EnumCellType.ColumnHeader) SetColumnHeader(sheet, range);
                //else if (cell.CellType == EnumCellType.RowHeader) SetRowHeader(sheet, range);
                //else if (cell.CellType == EnumCellType.CellHeader) SetCellHeader(sheet, range);
                //else SetDatCell(sheet, range);

                //cfs.Invalidate();
            }
        }

        //private static void SetColumnHeaderTitle(XLSheet sheet, CellRange range)
        //{
        //    var eee=sheet.MergedCells;
        //    eee.
        //    sheet.SetCellFormat(range.Cells, CellFormat.BorderThickness, new Thickness(1));
        //    sheet.SetCellFormat(range.Cells, CellFormat.BorderBrush, System.Windows.Media.Brushes.Black);
        //    sheet.SetCellFormat(range.Cells, CellFormat.Background, System.Windows.Media.Brushes.LightGray);
        //    sheet.SetCellFormat(range.Cells, CellFormat.HorizontalAlignment, HorizontalAlignment.Center);
        //    sheet.SetCellFormat(range.Cells, CellFormat.FontWeight, FontWeights.Bold);
        //}

        //private static void SetColumnHeader(C1FlexSheet cfs, CellRange range)
        //{
        //    cfs.SetCellFormat(range.Cells, CellFormat.BorderThickness, new Thickness(1));
        //    cfs.SetCellFormat(range.Cells, CellFormat.BorderBrush, System.Windows.Media.Brushes.Black);
        //    cfs.SetCellFormat(range.Cells, CellFormat.Background, System.Windows.Media.Brushes.LightGray);
        //    cfs.SetCellFormat(range.Cells, CellFormat.HorizontalAlignment, HorizontalAlignment.Center);
        //}

        //private static void SetCellHeader(C1FlexSheet cfs, CellRange range)
        //{
        //    cfs.SetCellFormat(range.Cells, CellFormat.BorderThickness, new Thickness(1));
        //    cfs.SetCellFormat(range.Cells, CellFormat.BorderBrush, System.Windows.Media.Brushes.Black);
        //    cfs.SetCellFormat(range.Cells, CellFormat.Background, System.Windows.Media.Brushes.LightGray);
        //    cfs.SetCellFormat(range.Cells, CellFormat.HorizontalAlignment, HorizontalAlignment.Center);
        //    cfs.SetCellFormat(range.Cells, CellFormat.FontWeight, FontWeights.Bold);
        //}

        //private static void SetDatCell(C1FlexSheet cfs, CellRange range)
        //{
        //    cfs.SetCellFormat(range.Cells, CellFormat.BorderThickness, new Thickness(1));
        //    cfs.SetCellFormat(range.Cells, CellFormat.BorderBrush, System.Windows.Media.Brushes.Black);
        //}

        //private static void SetRowHeader(C1FlexSheet cfs, CellRange range)
        //{
        //    cfs.SetCellFormat(range.Cells, CellFormat.BorderThickness, new Thickness(1));
        //    cfs.SetCellFormat(range.Cells, CellFormat.BorderBrush, System.Windows.Media.Brushes.Black);
        //}
    }
}
