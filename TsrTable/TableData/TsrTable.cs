using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System;
using System.Collections.Generic;
using TsrTable.Domain.Entities;
using TsrTable.FlexSheet;
using TsrTable.RichTextBox;
using TsrTable.RichTextBox.TableData;

namespace TsrTable.TableData
{
    public class TsrTable : ITsrElement
    {
        public List<CellEntity> CellEntities { get; }
        public List<TableDataEntity> Datas { get; }

        public TsrTable(
            List<CellEntity> cellEntities,
            List<TableDataEntity> tableDataEntities)
        {
            CellEntities = cellEntities;
            Datas = tableDataEntities;
        }
        public void ToExcel(C1XLBook book)
        {
            ExcelTools.CreateTable(book, CellEntities, Datas);
        }

        public void ToFlexSheet(C1FlexSheet cfs)
        {
            FlexSheetTools.CreateTable(cfs, CellEntities, Datas);
        }

        public C1Block OutputToRichTextBox()
        {
            return RichTextBoxTools.CreateTable(CellEntities, Datas);
        }

        public RtfObject ToWord()
        {
            return WordTools.CreateTable(CellEntities, Datas);
        }

        public C1TextElement ToRtb()
        {
            throw new NotImplementedException();
        }

        public C1TextElement GetRtbInstance()
        {
            throw new NotImplementedException();
        }
    }
}
