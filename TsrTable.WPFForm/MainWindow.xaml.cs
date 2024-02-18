using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox;
using C1.WPF.RichTextBox.Documents;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TsrTable.C1RichTextBox;
using TsrTable.C1RichTextBox.TableData;
using TsrTable.Domain.Entities;
using TsrTable.WPFForm.ViewModelEntities;

namespace TsrTable.WPFForm
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<TableHeaderVMEntity> HeaderList
    = new ObservableCollection<TableHeaderVMEntity>();

        public ObservableCollection<TableHeaderVMEntity> CriteriaList
             = new ObservableCollection<TableHeaderVMEntity>();

        private C1TableCell targetCell;
        private System.Windows.Media.Brush targetColor;
        private TableControlBase tableControl;
        public MainWindow()
        {

            InitializeComponent();
            

            CriteriaPositionRadioButton.IsChecked = true;
            SpecSheetRadioButton.IsChecked = true;

            var list = TableHeaderFake.GetData(1);
            TableHeaderVMEntity.ConvertToVMEntities(list).ForEach(x => HeaderList.Add(x));
            ContainerDataGrid.ItemsSource = HeaderList.ToList().FindAll(x => x.Parent == 0);

            var list2 = TableHeaderFake.GetData(0);
            TableHeaderVMEntity.ConvertToVMEntities(list2).ForEach(x => CriteriaList.Add(x));
            CriteriaDataGrid.ItemsSource = CriteriaList;

            var flex = new C1FlexSheet();
            dp.Children.Add(flex);
        }

        private void CreateTableButton_Click(object sender, RoutedEventArgs e)
        {
            tb1.Text = null;
            targetCell = null;
            tableControl.ClearTable();

            var tableContent = TsrTableTools.GetTableContent(
                        TableHeaderVMEntity.GetEntities(HeaderList.ToList(), null),
                        TableHeaderVMEntity.GetEntities(CriteriaList.ToList(), null),
                        SpecSheetRadioButton.IsChecked,
                        CriteriaPositionRadioButton.IsChecked);
            if (tableContent == null) return;

            var cellList = TsrTableTools.CreateCellList(tableContent);
            var table = new TsrTable.C1RichTextBox.TsrTable(tableContent, cellList);
            tableControl.Add(table);
        }

        private void ClearTableButton_Click(object sender, RoutedEventArgs e)
        {
            CriteriaList.Clear();
            HeaderList.Clear();
            CriteriaDataGrid.ItemsSource = null;
            ContainerDataGrid.ItemsSource = null;
            tv1.ItemsSource = null;
            tb1.Text = null;
            targetCell = null;
            tableControl.ClearTable();
        }

        private void GetDataButton_Click(object sender, RoutedEventArgs e)
        {
            tb1.Text = string.Empty;
            if (targetCell != null)
            {
                targetCell.Background = targetColor;
            }

            tb1.Text = tableControl.GetData();
            
                //targetColor = cell.Background;
                //cell.Background = System.Windows.Media.Brushes.Red;
                //targetCell = cell;
            
        }

        private void CriteriaButton_Click(object sender, RoutedEventArgs e)
        {
            if (CriteriaTextBox.Text is null) return;
            if (CriteriaTextBox.Text == string.Empty) return;

            // todo Criteria Sub Container の作り方検討
            var id = CriteriaList.Count * 100 + 1001;
            CriteriaList.Add(new TableHeaderVMEntity(
                new TableHeaderEntity(id, CriteriaTextBox.Text, 1000, 0)));
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            CriteriaDataGrid.ItemsSource = CriteriaList;
            CriteriaTextBox.Text = string.Empty;
        }

        private void ContainerButton_Click(object sender, RoutedEventArgs e)
        {
            if (ContainerTextBox.Text == string.Empty) return;
            var id = HeaderList.Count + 1;
            HeaderList.Add(new TableHeaderVMEntity(new TableHeaderEntity(id, ContainerTextBox.Text)));
            ContainerDataGrid.ItemsSource = HeaderList.ToList().FindAll(x => x.Parent == 0);
            ContainerTextBox.Text = string.Empty;
        }

        private void HeaderButton_Click(object sender, RoutedEventArgs e)
        {
            if (HeaderTextBox.Text is null) return;
            if (HeaderTextBox.Text == string.Empty) return;
            var container = ContainerDataGrid.SelectedItem as TableHeaderVMEntity;
            if (container == null) return;

            string[] hierarchy = HeaderTextBox.Text.Split('/');

            var input = HeaderTextBox.Text;
            var parent = container;
            if (hierarchy.Count() > 1)
            {
                if (hierarchy[0] == string.Empty)
                {
                    if (tv1.SelectedItem == null)
                    {
                        HeaderTextBox.Text = string.Empty;
                        return;
                    }
                    parent = tv1.SelectedItem as TableHeaderVMEntity;
                    if (parent == null) return;
                }
                else
                {
                    parent = new TableHeaderVMEntity(hierarchy[0], container);
                    container.Add(parent);
                }
                input = hierarchy[1];
            }

            tv1.ItemsSource = null;
            string[] items = input.Split(',');
            foreach (var item in items)
            {
                parent.Add(new TableHeaderVMEntity(item, parent));
            }
            tv1.ItemsSource = container.Children;
            HeaderTextBox.Text = string.Empty;
        }

        private void ContainerDataGrid_SelectedCellsChanged(object sender, 
            System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            if (ContainerDataGrid.SelectedItem is TableHeaderVMEntity item)
            {
                tv1.ItemsSource = item.Children;
            }
        }

        private void SpecSheetRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            tableControl = new TsrRichTextBox();
            tableContainer.Children.Add(tableControl);
            //tableControl.rtb.ViewMode = TextViewMode.Draft;
            //tableControl.rtb.Zoom = 1.5;
        }
    }
}
