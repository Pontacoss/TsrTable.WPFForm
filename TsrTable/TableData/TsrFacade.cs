using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System.Collections.Generic;
using TsrTable.FlexSheet;
using TsrTable.RichTextBox.TableData;
using TsrTable.RichTextBox;
using TsrTable.Domain.Common;
using TsrTable.Domain.Entities;
using C1.WPF.Excel;

namespace TsrTable.TableData
{
    public class TsrFacade
    {
        public static int FlexSheetWidth { get; } = 18;
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

            public static RtfTable CreateTableToWord(TableContent tableContent,
            List<CellEntity> list)
        {
            return WordTools.CreateTable(tableContent, list);
        }

        public static C1Table CreateTableToRichTextBox(TableContent tableContent,
            List<CellEntity> list)
        {
            return RichTextBoxTools.CreateTable(tableContent, list);
        }
        public static void CreateTableToFlexSheet(C1FlexSheet cfs,
            List<CellEntity> list)
        {
            FlexSheetTools.CreateTable(cfs, list);
        }

        public static void CreateTableToExcel(C1XLBook book,
            List<CellEntity> list)
        {
            ExcelTools.CreateTable(book, book.Sheets[0], list);
        }

        public static List<CellEntity> GetCellData(List<CellEntity> list,C1Table table)
        {
            return RichTextBoxTools.GetCellData(list, table);
        }
    }
}
