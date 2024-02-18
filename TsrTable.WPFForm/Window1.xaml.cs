using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TsrTable.C1RichTextBox.TableData;
using TsrTable.Domain.Entities;
using TsrTable.WPFForm.ViewModelEntities;
using C1.WPF.FlexGrid;
using System.Collections.Generic;
using TsrTable.TableData;

namespace TsrTable.WPFForm
{
    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class Window1 : Window
    {
        public ObservableCollection<TableHeaderVMEntity> HeaderList
            = new ObservableCollection<TableHeaderVMEntity>();

        public ObservableCollection<TableHeaderVMEntity> CriteriaList
             = new ObservableCollection<TableHeaderVMEntity>();

        private List<CellEntity> _cellList=new List<CellEntity>();

        public Window1()
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

            InitializeGrid();
        }
        private void CreateTable()
        {

            InitializeGrid();

            var xmm = cfs.MergeManager as ExcelMergeManager;
            foreach (var cell in _cellList)
            {
                cfs[cell.SheetIndexRow, cell.SheetIndexColumn] = cell.Name;
                var range = new CellRange(cell.SheetIndexRow, cell.SheetIndexColumn,
                    cell.SheetIndexRow + cell.SheetSpanRow - 1, cell.SheetIndexColumn + cell.SheetSpanColumn - 1);
                cfs.SetCellFormat(range.Cells, CellFormat.BorderThickness, new Thickness(1));
                cfs.SetCellFormat(range.Cells, CellFormat.BorderBrush, System.Windows.Media.Brushes.Black);
                if (cell.CellType == EnumCellType.ColumnHeaderTitle ||
                    cell.CellType == EnumCellType.ColumnHeader ||
                    cell.CellType == EnumCellType.CellHeader)
                {
                    cfs.SetCellFormat(range.Cells, CellFormat.Background, System.Windows.Media.Brushes.LightGray);
                    cfs.SetCellFormat(range.Cells, CellFormat.HorizontalAlignment, HorizontalAlignment.Center);
                }
                if (cell.CellType == EnumCellType.ColumnHeaderTitle ||
                    cell.CellType == EnumCellType.CellHeader)
                {
                    cfs.SetCellFormat(range.Cells, CellFormat.FontWeight, FontWeights.Bold);
                }
                xmm.AddRange(range);
                cfs.Invalidate();
            }
        }
        private void CreateTableButton_Click(object sender, RoutedEventArgs e)
        {
            tb1.Text = null;
            

            var tableContent = TsrTableTools.GetTableContent(
                        TableHeaderVMEntity.GetEntities(HeaderList.ToList(), null),
                        TableHeaderVMEntity.GetEntities(CriteriaList.ToList(), null),
                        SpecSheetRadioButton.IsChecked,
                        CriteriaPositionRadioButton.IsChecked);
            if (tableContent == null) return;

            _cellList = TsrTableTools.CreateCellList(tableContent);
            var table = new TsrTable.C1RichTextBox.TsrTableData(tableContent, _cellList);

            CreateTable();
        }

        private void ClearTableButton_Click(object sender, RoutedEventArgs e)
        {
            CriteriaList.Clear();
            HeaderList.Clear();
            CriteriaDataGrid.ItemsSource = null;
            ContainerDataGrid.ItemsSource = null;
            tv1.ItemsSource = null;
            tb1.Text = null;
            InitializeGrid();
        }
        private void InitializeGrid()
        {
            cfs.Columns.Clear();
            while (cfs.Columns.Count < 26)
            {
                cfs.Columns.Add(new C1.WPF.FlexGrid.Column());
            }
            foreach (var col in cfs.Columns)
            {
                col.Width = new GridLength(35);
            }
        }

        private void GetDataButton_Click(object sender, RoutedEventArgs e)
        {
            
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

        private void ColumnSpanIncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            SheetSpanColumnSeting(1);
        }

        private void ColumnSpandecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            SheetSpanColumnSeting(-1);
        }

        private void SheetSpanColumnSeting(int gap)
        {
            var selection = cfs.Selection;
            //var rowElement = _cellList.FindAll(x =>
            //    x.SheetIndexRow <= selection.TopRow &&
            //    x.SheetIndexRow + x.SheetSpanRow - 1 >= selection.BottomRow);
            var columnElement = _cellList.FindAll(x =>
                x.SheetIndexColumn <= selection.LeftColumn &&
                x.SheetIndexColumn + x.SheetSpanColumn - 1 >= selection.RightColumn);
            var columnElementMoveOnly = _cellList.FindAll(x =>
                x.SheetIndexColumn > selection.RightColumn);

            foreach (var col in columnElement)
            {
                if (!col.CanChangeSpan(gap)) return;
            }
            foreach (var col in columnElementMoveOnly)
            {
                if (!col.CanMove(gap)) return;
            }
            foreach (var col in columnElement)
            {
                col.SetSheetSpanColumn(gap);
            }
            foreach (var col in columnElementMoveOnly)
            {
                col.SheetIndexColumn += gap;
            }
            CreateTable();
        }

        private void SheetSpanRowSeting(int gap)
        {
            var selection = cfs.Selection;
            //var rowElement = _cellList.FindAll(x =>
            //    x.SheetIndexRow <= selection.TopRow &&
            //    x.SheetIndexRow + x.SheetSpanRow - 1 >= selection.BottomRow);
            var rowElement = _cellList.FindAll(x =>
                x.SheetIndexRow <= selection.TopRow &&
                x.SheetIndexRow + x.SheetSpanRow - 1 >= selection.BottomRow);
            var rowElementMoveOnly = _cellList.FindAll(x =>
                x.SheetIndexRow > selection.BottomRow);

            foreach (var col in rowElement)
            {
                if (!col.CanChangeSpanRow(gap)) return;
            }
    
            foreach (var col in rowElement)
            {
                col.SetSheetSpanRow(gap);
            }
            foreach (var col in rowElementMoveOnly)
            {
                col.SheetIndexRow += gap;
            }
            CreateTable();
        }

        private void RowSpandecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            SheetSpanRowSeting(-1);
        }

        private void RowSpanIncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            SheetSpanRowSeting(1);
        }
    }
}
