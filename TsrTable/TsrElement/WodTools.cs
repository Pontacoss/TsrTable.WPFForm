using C1.WPF.Word;
using C1.WPF.Word.Objects;
using System.Collections.Generic;
using System.Linq;
using TsrTable.Domain.Common;
using TsrTable.Domain.Entities;

namespace TsrTable.RichTextBox.TsrElement
{
    internal class WordTools
    {
        internal static RtfTable CreateTable(
            List<CellEntity> list,
            List<TableDataEntity> datas)
        {
            var maxRow = list.Max(x => x.RowIndex + x.RowSpan);
            var maxColumn = list.Max(x => x.ColumnIndex + x.ColumnSpan);

            var table = new RtfTable(maxRow, maxColumn);
            table.IsCellsFitPage = false;

            foreach (var cellEntity in list)
            {
                var cell = table.Rows[cellEntity.RowIndex].Cells[cellEntity.ColumnIndex];
                if (cellEntity.RowSpan > 1 || cellEntity.ColumnSpan > 1)
                {
                    cell.SetMerged(cellEntity.RowSpan, cellEntity.ColumnSpan);
                }

                if (cellEntity.CellType == EnumCellType.ColumnHeaderTitle)
                    SetColumnHeaderTitle(cell, cellEntity);
                else if (cellEntity.CellType == EnumCellType.ColumnHeader)
                    SetColumnHeader(cell, cellEntity);
                else if (cellEntity.CellType == EnumCellType.RowHeader)
                    SetRowHeader(cell, cellEntity);
                else if (cellEntity.CellType == EnumCellType.CellHeader)
                    SetCellHeader(cell, cellEntity);
                else
                    SetDataCell(cell, cellEntity, datas);
            }

            table.Alignment = ContentAlignment.MiddleCenter;

            return table;
        }

        private static void SetColumnHeaderTitle(RtfCell cell, CellEntity cellEntity)
        {
            cell.Alignment = ContentAlignment.MiddleCenter;
            cell.SetRectBorder(RtfBorderStyle.Single, System.Windows.Media.Colors.Black, 1);
            cell.BackFilling = System.Windows.Media.Colors.LightGray;
            var font = new Font("MS UI Gothic", 10, RtfFontStyle.Bold);
            if (cellEntity.Value != null)
            {
                cell.Content.Add(new RtfString(cellEntity.Value));
            }
            if (!cellEntity.Width.IsAuto)
            {
                cell.Width = (float)cellEntity.Width.Value;
            }
        }

        private static void SetColumnHeader(RtfCell cell, CellEntity cellEntity)
        {
            cell.Alignment = ContentAlignment.MiddleCenter;
            cell.SetRectBorder(RtfBorderStyle.Single, System.Windows.Media.Colors.Black, 1);
            cell.BackFilling = System.Windows.Media.Colors.LightGray;
            var font = new Font("MS UI Gothic", 10, RtfFontStyle.Regular);
            if (cellEntity.Value != null)
            {
                cell.Content.Add(new RtfString(cellEntity.Value));
            }
            if (!cellEntity.Width.IsAuto)
            {
                cell.Width = (float)cellEntity.Width.Value;
            }
        }

        private static void SetRowHeader(RtfCell cell, CellEntity cellEntity)
        {
            cell.SetRectBorder(RtfBorderStyle.Single, System.Windows.Media.Colors.Black, 1);
            var font = new Font("MS UI Gothic", 10, RtfFontStyle.Regular);
            if (cellEntity.Value != null)
            {
                cell.Content.Add(new RtfString(cellEntity.Value));
            }
            if (!cellEntity.Width.IsAuto)
            {
                cell.Width = (float)cellEntity.Width.Value;
            }
            if (cellEntity.Value == null) return;
            // 文字が全て数字だけの場合は右寄せ。数字以外がありなら左寄せ。
            char[] chars = cellEntity.Value.ToCharArray();
            if (chars.Any(x => char.IsDigit(x) == false))
            {
                cell.Alignment = ContentAlignment.MiddleLeft;
            }
            else
            {
                cell.Alignment = ContentAlignment.MiddleRight;
            }
        }

        private static void SetCellHeader(RtfCell cell, CellEntity cellEntity)
        {
            cell.Alignment = ContentAlignment.MiddleCenter;
            cell.SetRectBorder(RtfBorderStyle.Single, System.Windows.Media.Colors.Black, 1);
            cell.BackFilling = System.Windows.Media.Colors.LightGray;
            var font = new Font("MS UI Gothic", 10, RtfFontStyle.Bold);
            if (cellEntity.Value != null)
            {
                cell.Content.Add(new RtfString(cellEntity.Value));
            }
            if (!cellEntity.Width.IsAuto)
            {
                cell.Width = (float)cellEntity.Width.Value;
            }
        }

        private static void SetDataCell(RtfCell cell, CellEntity cellEntity, List<TableDataEntity> datas)
        {
            cell.Alignment = ContentAlignment.MiddleRight;
            cell.SetRectBorder(RtfBorderStyle.Single, System.Windows.Media.Colors.Black, 1);
            var font = new Font("MS UI Gothic", 10, RtfFontStyle.Regular);

            cell.Content.Add(new RtfString(
                TsrTableTools.GetCellContent(
                    cellEntity, datas, EnumTsrDocumentType.SpecSheet),
                    font));


            if (!cellEntity.Width.IsAuto)
            {
                cell.Width = (float)cellEntity.Width.Value;
            }
        }

    }
}
