using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TsrTable.C1RichTextBox.TableData;

namespace TsrTable.WPFForm
{
    /// <summary>
    /// FlexSheetCellSettingForm.xaml の相互作用ロジック
    /// </summary>
    public partial class FlexSheetCellSettingForm : Page
    {
        private CellEntity _cellEntity;
        public FlexSheetCellSettingForm(CellEntity cellEntity)
        {
            _cellEntity = cellEntity;
            InitializeComponent();

            RowSpanTextBox.Text = _cellEntity.SheetSpanRow.ToString();
            ColumnSpanTextBox.Text = _cellEntity.SheetSpanColumn.ToString();
        }

        private void RowSpandecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            RowSpanTextBox.Text = (Convert.ToInt32(RowSpanTextBox.Text) - 1).ToString();
        }

        private void RowSpanIncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            RowSpanTextBox.Text = (Convert.ToInt32(RowSpanTextBox.Text) + 1).ToString();
        }

        private void ColumnSpandecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            ColumnSpanTextBox.Text = (Convert.ToInt32(ColumnSpanTextBox.Text) - 1).ToString();
        }

        private void ColumnSpanIncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            ColumnSpanTextBox.Text = (Convert.ToInt32(ColumnSpanTextBox.Text) + 1).ToString();
        }
    }
}
