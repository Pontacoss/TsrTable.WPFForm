using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TsrTable.Domain.Entities;
using TsrTable.Domain.ValueObjects;
using TsrTable.RichTextBox;
using TsrTable.RichTextBox.TableData;

namespace TsrTable.WPFForm
{
    /// <summary>
    /// TableDataInputWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class TableDataInputWindow : Window
    {
        public TableDataInputWindow(List<CellEntity> list)
        {
            InitializeComponent();

            var dataCellList = list.FindAll(x => x.CellType == TableData.EnumCellType.DataCell);

            var convertList=new List<TableDataEntity>();
            foreach (var cell in dataCellList)
            {
                convertList.Add(new TableDataEntity(cell.RowIndex,cell.ColumnIndex,cell.Conditions));
            }
            combo.ItemsSource = Operators.Items;
            dg.ItemsSource = convertList;
        }
    }
}
