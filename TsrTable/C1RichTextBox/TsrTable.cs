using C1.WPF.RichTextBox.Documents;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using TsrTable.C1RichTextBox.TableData;

namespace TsrTable.C1RichTextBox
{
    public sealed class TsrTable : C1Table
    {
        public TsrTable() : base() { }

        public TsrTable(TableContent tableContent, List<CellEntity> list) : base()
        {
            // C1TableRowを生成
            var rows = new C1TableRowGroup();
            for (int i = 0; i < tableContent.RowHeaderHeight + tableContent.ColumnHeaderHeight; i++)
            {
                rows.Rows.Add(new C1TableRow());
            }
            this.RowGroups.Add(rows);

            // セルの生成とC1TableRowへの追加
            foreach (var cellEntity in list)
            {
                C1TableCell cell;
                if (cellEntity.CellType == 0)
                    cell = CreateRowHeaderCell(cellEntity);
                else if (cellEntity.CellType == 1)
                    cell = CreateColumnHeaderCell(cellEntity);
                else if (cellEntity.CellType == 2)
                    cell = CreateColumnHeaderTitleCell(cellEntity);
                else
                    cell = CreateDataCell(cellEntity);
                rows.First(x => x.Index == cellEntity.RowIndex).Children.Add(cell);
            }
            BorderCollapse = true;
            Margin = new Thickness(5);
        }
        
       

        private static C1TableCell CreateDataCell(CellEntity cellEntity)
        {
            var cell = new TsrDataCell(cellEntity).TsrCellExtensions(string.Empty);
            
            // DataCellは、何種類か作成予定。規定値を選択して入れるタイプなど。

            return cell;
        }

        private static C1TableCell CreateColumnHeaderCell(CellEntity cellEntity)
        {
            var cell = new TsrHeaderCell(cellEntity).TsrCellExtensions(cellEntity.Name);
            cell.Background = Brushes.LightGray;
            cell.TextAlignment = C1TextAlignment.Center;
            return cell;
        }

        private static C1TableCell CreateColumnHeaderTitleCell(CellEntity cellEntity)
        {
            var cell = new TsrHeaderCell(cellEntity).TsrCellExtensions(cellEntity.Name);
            cell.FontWeight = FontWeights.Bold;
            cell.Background = Brushes.LightGray;
            cell.TextAlignment = C1TextAlignment.Center;
            return cell;
        }

        private static C1TableCell CreateRowHeaderCell(CellEntity cellEntity)
        {
            var cell = new TsrHeaderCell(cellEntity).TsrCellExtensions(cellEntity.Name);
            if (cellEntity.Name == null) return cell;
            // 文字が全て数字だけの場合は右寄せ。数字以外がありなら左寄せ。
            char[] chars = cellEntity.Name.ToCharArray();
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
    }
}

//private static C1TableCell CreateComboBoxCell(string? name,C1TableCell cell)
//{
//    var paragraph = new C1Paragraph();

//    var combo= new System.Windows.Controls.ComboBox();
//    combo.ItemsSource = new string[] { "+", "-", "±" };
//    combo.SelectionChanged += Combo_SelectionChanged;

//    paragraph.Children.Add(new C1InlineUIContainer() { Content = combo });
//    paragraph.Children.Add(
//        new C1Run()
//        {
//            Text = name,
//            Padding = new Thickness(0, 0, 0, 0),
//            Margin = new Thickness(0)
//        });
//    paragraph.Padding = new Thickness(0);
//    paragraph.Margin = new Thickness(2);
//    cell.Children.Add(paragraph);

//    cell.BorderThickness = new Thickness(1);
//    cell.Padding = new Thickness(0);
//    cell.Margin = new Thickness(0);

//    return cell;
//}

//private static void Combo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
//{
//    var combo = sender as ComboBox;
//}

//private static C1TableCell CreateComboBoxDataCell(int rowIndex, int columnIndex, string? name)
//{
//    var cell = CreateComboBoxCell(string.Empty, new TsrDataCell(rowIndex, columnIndex, name));
//    cell.TextAlignment = C1TextAlignment.Right;
//    cell.VerticalAlignment = C1VerticalAlignment.Middle;
//    return cell;
//}
