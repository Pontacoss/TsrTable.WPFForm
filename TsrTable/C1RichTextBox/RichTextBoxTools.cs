using C1.WPF.RichTextBox.Documents;
using System.Windows;

namespace TsrTable.C1RichTextBox
{
    internal static class RichTextBoxTools
    {
        /// <summary>
        /// C1TableCellの中身(C1Paragraph,C1Run)の追加と罫線、パディング、
        /// マージンの設定を行う拡張メソッド
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static C1TableCell TsrCellExtensions(this C1TableCell cell, string name)
        {
            var paragraph = new C1Paragraph();
            paragraph.Children.Add(
                new C1Run()
                {
                    Text = name,
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
    }
}
