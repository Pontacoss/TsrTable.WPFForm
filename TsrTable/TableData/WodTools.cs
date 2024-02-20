using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word;
using C1.WPF.Word.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsrTable.RichTextBox.TableData;

namespace TsrTable.TableData
{
    internal class WordTools
    {
        internal static RtfTable CreateTable(TableContent tableContent,
            List<CellEntity> list)
        {
            var table = new RtfTable(
                tableContent.RowHeaderHeight + tableContent.ColumnHeaderHeight,
                tableContent.RowHeaderWidth + tableContent.ColumnHeaderWidth);
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
                    SetDataCell(cell, cellEntity);
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
            cell.Content.Add(new RtfString(cellEntity.Name ?? string.Empty, font));
        }

        private static void SetColumnHeader(RtfCell cell, CellEntity cellEntity)
        {
            cell.Alignment = ContentAlignment.MiddleCenter;
            cell.SetRectBorder(RtfBorderStyle.Single, System.Windows.Media.Colors.Black, 1);
            cell.BackFilling = System.Windows.Media.Colors.LightGray;
            var font = new Font("MS UI Gothic", 10, RtfFontStyle.Regular);
            cell.Content.Add(new RtfString(cellEntity.Name ?? string.Empty, font));
        }

        private static void SetRowHeader(RtfCell cell, CellEntity cellEntity)
        {
            cell.SetRectBorder(RtfBorderStyle.Single, System.Windows.Media.Colors.Black, 1);
            var font = new Font("MS UI Gothic", 10, RtfFontStyle.Regular);
            cell.Content.Add(new RtfString(cellEntity.Name ?? string.Empty, font));

            if (cellEntity.Name == null) return;
            // 文字が全て数字だけの場合は右寄せ。数字以外がありなら左寄せ。
            char[] chars = cellEntity.Name.ToCharArray();
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
            cell.Content.Add(new RtfString(cellEntity.Name ?? string.Empty, font));
        }

        private static void SetDataCell(RtfCell cell, CellEntity cellEntity)
        {
            cell.Alignment = ContentAlignment.MiddleRight;
            cell.SetRectBorder(RtfBorderStyle.Single, System.Windows.Media.Colors.Black, 1);
            var font = new Font("MS UI Gothic", 10, RtfFontStyle.Regular);
            //cell.Content.Add(new RtfString(cellEntity.Name ?? string.Empty, font));
        }

    }
}
