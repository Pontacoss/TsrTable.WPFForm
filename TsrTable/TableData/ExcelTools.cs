using C1.Util.DX;
using C1.WPF.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using TsrTable.Domain.Common;
using TsrTable.Domain.Entities;
using TsrTable.RichTextBox.TableData;
using static C1.Util.Win.Win32;

namespace TsrTable.TableData
{
    internal static class SetStyleExtentions
    {
        internal static XLStyle BorderExtention(this  XLStyle style)
        {
            style.SetBorderStyle(XLLineStyleEnum.Thin);
            style.SetBorderColor(System.Windows.Media.Colors.Black);
            return style;
        }
        internal static XLStyle BackColorGray(this XLStyle style)
        {
            style.BackColor = System.Windows.Media.Colors.LightGray;
            return style;
        }
    }

    internal class ExcelTools
    {
        internal static void CreateTable(
            C1XLBook book,
            List<CellEntity> cellList,
            List<TableDataEntity> datas)
        {
            var sheet = book.Sheets[0];
            sheet.DefaultColumnWidth = 500;

            var documentType = EnumTsrDocumentType.TestReport;
            var containerStyle = SetContainerStyle(book);
            var columnHeaderStyle = SetColumnHeaderStyle(book);
            var rowHeaderStyle = SetRowHeaderStyle(book);
            var dataCellStyle=SetDataCellStyle(book);

            foreach (var cell in cellList)
            {
                var value = TsrTableTools.GetCellContent(cell, datas, documentType);

                if (Regex.IsMatch(value, @"^\d+$"))
                    sheet[cell.SheetIndexRow, cell.SheetIndexColumn].Value = Convert.ToDouble(value);
                else
                    sheet[cell.SheetIndexRow, cell.SheetIndexColumn].Value = value;

                var range = new XLCellRange(cell.SheetIndexRow, cell.SheetIndexRow + cell.SheetSpanRow - 1, 
                    cell.SheetIndexColumn, cell.SheetIndexColumn + cell.SheetSpanColumn - 1);
                sheet.MergedCells.Add(range);

                if (cell.CellType == EnumCellType.ColumnHeaderTitle) range.Style= containerStyle;
                else if (cell.CellType == EnumCellType.ColumnHeader) range.Style = columnHeaderStyle;
                else if (cell.CellType == EnumCellType.RowHeader) range.Style = rowHeaderStyle;
                else if (cell.CellType == EnumCellType.CellHeader) range.Style = containerStyle;
                else range.Style = dataCellStyle;
                
            }
        }

        private static XLStyle SetContainerStyle(C1XLBook book)
        {
            XLStyle style = new XLStyle(book)
            {
                Font = new XLFont("MS UI Gothic", 10, true, false),
                AlignHorz = XLAlignHorzEnum.Center,
                AlignVert = XLAlignVertEnum.Center,
                BackColor = System.Windows.Media.Colors.LightGray,
                WordWrap = true,
                ShrinkToFit = true
            };
            style.SetBorderStyle(XLLineStyleEnum.Thin);
            style.SetBorderColor(System.Windows.Media.Colors.Black);
            return style;
        }
        private static XLStyle SetColumnHeaderStyle(C1XLBook book)
        {
            XLStyle style = new XLStyle(book)
            {
                Font = new XLFont("MS UI Gothic", 10, false, false),
                AlignHorz = XLAlignHorzEnum.Center,
                AlignVert = XLAlignVertEnum.Center,
                BackColor = System.Windows.Media.Colors.LightGray,
                ShrinkToFit = true
            };
            style.SetBorderStyle(XLLineStyleEnum.Thin);
            style.SetBorderColor(System.Windows.Media.Colors.Black);
            return style;
        }

        

        private static XLStyle SetRowHeaderStyle(C1XLBook book)
        {
            XLStyle style = new XLStyle(book)
            {
                Font = new XLFont("MS UI Gothic", 10, false, false),
                AlignHorz = XLAlignHorzEnum.Center,
                AlignVert = XLAlignVertEnum.Center
            };
            style.SetBorderStyle(XLLineStyleEnum.Thin);
            style.SetBorderColor(System.Windows.Media.Colors.Black);
            return style;
        }

        private static XLStyle SetDataCellStyle(C1XLBook book)
        {
            XLStyle style = new XLStyle(book)
            {
                Font = new XLFont("MS UI Gothic", 10, false, false),
                AlignHorz = XLAlignHorzEnum.Right,
                AlignVert = XLAlignVertEnum.Center
            };
            style.SetBorderStyle(XLLineStyleEnum.Thin);
            style.SetBorderColor(System.Windows.Media.Colors.Black);
            return style;
        }
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
