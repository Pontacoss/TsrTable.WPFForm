using C1.WPF.RichTextBox.Documents;
using System;
using System.Linq;
using System.Windows.Controls;
using TsrTable.C1RichTextBox;


namespace TsrTable.WPFForm
{
    /// <summary>
    /// TsrRichTextBox.xaml の相互作用ロジック
    /// </summary>
    public partial class TsrRichTextBox : TableControlBase
    {
        public TsrRichTextBox()
        {
            InitializeComponent();
        }

        public void Add(C1RichTextBox.TsrTable table)
        {
            rtb.Document.Blocks.Add(table);
        }

        public void ClearTable()
        {
            rtb.Document.Blocks.Clear();
        }

        public string GetData()
        {
            if (rtb.Selection.Cells.Any())
            {
                var cell = rtb.Selection.Cells.First() as C1TableCell;
                if (cell is TsrDataCell dataCell)
                {
                    return string.Format("Row:{0}, Column:{1}\n{2}",
                        dataCell.RowIndex, dataCell.ColumnIndex, dataCell.Conditions);
                }
                else if (cell is TsrHeaderCell headerCell)
                {
                    return string.Format("Row:{0}, Column:{1}\n{2}",
                        headerCell.RowIndex, headerCell.ColumnIndex, headerCell.GetType().ToString());
                }
            }
            return string.Empty;
        }
    }

    public abstract class TableControlBase: UserControl
    {
        public abstract void Add(C1RichTextBox.TsrTable table);
        public abstract void ClearTable();
        public abstract string GetData();

        public static implicit operator TableControlBase(TsrRichTextBox v)
        {
            return new TsrRichTextBox();
        }
    }
}
