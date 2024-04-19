using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System.Collections.Generic;
using TsrTable.Domain.Common;
using TsrTable.Domain.Entities;
using TsrTable.FlexSheet;

namespace TsrTable.RichTextBox.TsrElement
{
    public class TsrFacadeForTable
    {
        public static int FlexSheetColumnCount { get; } = 18;
        public static int FlexSheetCellWidth { get; } = 35;

        public static List<CellEntity> CreateCellList(TableContent tableContent)
        {
            return TsrTableTools.CreateCellList(tableContent);
        }

        public static TableContent GetTableContent(
            List<TableHeaderEntity> headerList,
            List<TableHeaderEntity> criteriaList,
            EnumTsrDocumentType documentType,
            bool? criteriaPosition = false)
        {
            return TsrTableTools.GetTableContent(headerList, criteriaList, documentType, criteriaPosition);
        }

        public static RtfTable CreateTableToWord(
            List<CellEntity> list,
            List<TableDataEntity> datas)
        {
            return WordTools.CreateTable(list, datas);
        }

        public static C1Table CreateTableToRichTextBox(
            TableContent tableContent,
            List<CellEntity> list,
            List<TableDataEntity> datas)
        {
            return RichTextBoxTools.CreateTable(list, datas);
        }
        public static void CreateTableToFlexSheet(
            C1FlexSheet cfs,
            List<CellEntity> list,
            List<TableDataEntity> datas)
        {
            FlexSheetTools.CreateTable(cfs, list, datas);
        }

        public static void CreateTableToExcel(
            C1XLBook book,
            List<CellEntity> list,
            List<TableDataEntity> datas)
        {
            ExcelTools.CreateTable(book, list, datas);
        }

        /// <summary>
        /// RichTextBoxの表から各セルの幅・高さ・値を取得し、CellEntityに格納する。
        /// </summary>
        /// <param name="list"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<CellEntity> GetCellData(List<CellEntity> list, C1Table table)
        {
            return RichTextBoxTools.GetCellData(list, table);
        }
    }
}
