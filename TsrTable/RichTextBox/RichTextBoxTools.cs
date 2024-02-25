using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TsrTable.Domain.Entities;
using TsrTable.Domain.ValueObjects;
using TsrTable.RichTextBox.TableData;
using TsrTable.TableData;

namespace TsrTable.RichTextBox
{
    internal static class RichTextBoxTools
    {
        /// <summary>
        /// C1TableCellの中身(C1Paragraph,C1Run)の追加と罫線、パディング、
        /// マージンの設定を行う拡張メソッド
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static C1TableCell TsrCellExtensions(this TsrCell cell, string value)
        {
            var paragraph = new C1Paragraph();
            paragraph.Children.Add(
                new C1Run()
                {
                    Text = value,
                    Padding = new Thickness(0, 0, 0, 0),
                    Margin = new Thickness(0)
                });
            paragraph.Padding = new Thickness(0);
            paragraph.Margin = new Thickness(1);
            cell.Children.Add(paragraph);

            cell.BorderThickness = new Thickness(1);
            cell.Padding = new Thickness(0);
            cell.Margin = new Thickness(0);
            return cell;
        }



        private static void Button_Click(object sender, RoutedEventArgs e)
        {
            var button =sender as Button;
            if (button.Content.ToString() == "±") button.Content = "+";
            else if (button.Content.ToString() == "+") button.Content = "-";
            else if (button.Content.ToString() == "-") button.Content = "±";
        }

        internal static C1Table CreateTable(
            TableContent tableContent,
            List<CellEntity> list,
            List<TableDataEntity> datas)
        {
            // C1TableとC1TableRowのインスタンスを生成
            var table = new C1Table();
            var rows = new C1TableRowGroup();
            for (int i = 0; i < tableContent.RowHeaderHeight +
                tableContent.ColumnHeaderHeight; i++)
            {
                rows.Rows.Add(new C1TableRow());
            }
            table.RowGroups.Add(rows);

            // セルの生成とC1TableRowへの追加
            foreach (var cellEntity in list)
            {
                C1TableCell cell;
                if (cellEntity.CellType == EnumCellType.RowHeader)
                    cell = CreateRowHeaderCell(cellEntity);
                else if (cellEntity.CellType == EnumCellType.ColumnHeader ||
                          cellEntity.CellType == EnumCellType.CellHeader)
                    cell = CreateColumnHeaderCell(cellEntity);
                else if (cellEntity.CellType == EnumCellType.ColumnHeaderTitle)
                    cell = CreateColumnHeaderTitleCell(cellEntity);
                else // if (cellEntity.CellType == EnumCellType.DataCell)
                    cell = CreateDataCell(cellEntity,datas);
                //else
                //{
                //    cell = CreateDataButtonCell(cellEntity);
                //}
                rows.First(x => x.Index == cellEntity.RowIndex).Children.Add(cell);
            }
            table.BorderCollapse = true;
            table.Margin = new Thickness(5);
            return table;
        }
        private static C1TableCell CreateDataCell(CellEntity cellEntity,List<TableDataEntity> datas)
        {
            if (datas != null)
            {
                var dataCell = datas.FirstOrDefault(x => x.Conditions == cellEntity.Conditions);
                if (dataCell.Criteria != null)
                {
                    return new TsrDataCell(cellEntity).TsrCellExtensions(dataCell.Criteria.DisplayValue);
                }
            }
            return new TsrDataCell(cellEntity).TsrCellExtensions(string.Empty);

            // DataCellは、何種類か作成予定。規定値を選択して入れるタイプなど。

        }


        private static C1TableCell CreateColumnHeaderCell(CellEntity cellEntity)
        {
            var cell = new TsrHeaderCell(cellEntity).TsrCellExtensions(cellEntity.Value);
            cell.Background = Brushes.LightGray;
            cell.TextAlignment = C1TextAlignment.Center;
            return cell;
        }

        private static C1TableCell CreateColumnHeaderTitleCell(CellEntity cellEntity)
        {
            var cell = new TsrHeaderCell(cellEntity).TsrCellExtensions(cellEntity.Value);
            cell.FontWeight = FontWeights.Bold;
            cell.Background = Brushes.LightGray;
            cell.TextAlignment = C1TextAlignment.Center;
            return cell;
        }

        private static C1TableCell CreateRowHeaderCell(CellEntity cellEntity)
        {
            var cell = new TsrHeaderCell(cellEntity).TsrCellExtensions(cellEntity.Value);
            if (cellEntity.Value == null) return cell;
            // 文字が全て数字だけの場合は右寄せ。数字以外がありなら左寄せ。
            char[] chars = cellEntity.Value.ToCharArray();
            if (chars.Any(x => char.IsDigit(x) == false))
            {
                cell.TextAlignment = C1TextAlignment.Left;
            }
            else
            {
                cell.TextAlignment = C1TextAlignment.Right;
            }
            return cell;
        }

        internal static List<CellEntity> GetCellData(this List<CellEntity> list,C1Table table)
        {
            foreach(var row in table.RowGroups.First().Rows)
            {
                foreach(var cell in row.Cells.OfType<TsrCell>())
                {
                    var cellEntity = list.First(x => x.RowIndex == cell.RowIndex && x.ColumnIndex == cell.ColumnIndex);
                    cellEntity.Width = cell.Width;
                    cellEntity.Height = cell.Height;
                    cellEntity.SetValue(cell.Value);
                }
            }
            return list;
        }
    }

    //private static C1TableCell CreateDataButtonCell(CellEntity cellEntity)
    //{
    //    var cell = new TsrDataCell(cellEntity).TsrButtonCellExtensions(string.Empty);


    //    return cell;
    //}

    //internal static C1TableCell TsrButtonCellExtensions(this C1TableCell cell, string name)
    //{
    //    var paragraph = new C1Paragraph();
    //    var button = new Button()
    //    {
    //        Content = "±",
    //        Width = 25,
    //    };
    //    button.Click += Button_Click;

    //    paragraph.Children.Add(
    //        new C1InlineUIContainer()
    //        {
    //            Content = button,
    //            Padding = new Thickness(0, 0, 0, 0),
    //            Margin = new Thickness(0)
    //        }) ;
    //    paragraph.Padding = new Thickness(0);
    //    paragraph.Margin = new Thickness(1);
    //    cell.Children.Add(paragraph);

    //    cell.BorderThickness = new Thickness(1);
    //    cell.Padding = new Thickness(0);
    //    cell.Margin = new Thickness(0);

    //    return cell;
    //}
}
