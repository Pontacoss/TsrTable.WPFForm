using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TsrTable.Domain.Entities;
using TsrTable.Domain.ValueObjects;
using TsrTable.RichTextBox.TableData;
using TsrTable.WPFForm.ViewModelEntities;

namespace TsrTable.WPFForm
{
    /// <summary>
    /// TableDataInputWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class TableDataInputWindow : Window
    {
        private List<CellEntity> _list;
        private List<TableDataEntity> _tableDatas;
        private List<TableDataVMEntity> _vmDatas=new List<TableDataVMEntity>();
        public TableDataInputWindow(List<CellEntity> list, List<TableDataEntity> tableDatas)
        {
            InitializeComponent();

            _list = list;
            _tableDatas =tableDatas;
            _tableDatas.ForEach(x => _vmDatas.Add(new TableDataVMEntity(x)));

            cbOperators.ItemsSource=Operators.Items;
            cbToleranceType.ItemsSource=ToleranceType.Items;

            dg.ItemsSource = _vmDatas;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _tableDatas.Clear();
            _tableDatas.AddRange(TableDataVMEntity.GetList(_vmDatas));
            this.Close();
        }
    }
}
