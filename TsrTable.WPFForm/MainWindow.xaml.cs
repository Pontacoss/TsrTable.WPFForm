using C1.WPF.Word;
using C1.WPF.RichTextBox;
using C1.WPF.RichTextBox.Documents;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TsrTable.RichTextBox;
using TsrTable.RichTextBox.TableData;
using TsrTable.Domain.Common;
using TsrTable.Domain.Entities;
using TsrTable.WPFForm.ViewModelEntities;
using Microsoft.Win32;
using System.Windows.Documents;
using System.Collections.Generic;
using TsrTable.TableData;
using System;

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
        private TableContent _tableContent;
        private List<CellEntity> _cellList;
        private C1Table _table;
 
        public MainWindow()
        {
            InitializeComponent();
            
            rtb.ViewMode= TextViewMode.Draft ;
            rtb.Zoom = 1.5;

            CriteriaPositionRadioButton.IsChecked = true;

            var list = TableHeaderFake.GetData(1);
            TableHeaderVMEntity.ConvertToVMEntities(list).ForEach(x => HeaderList.Add(x));
            ContainerDataGrid.ItemsSource = HeaderList.ToList().FindAll(x => x.Parent == 0);

            var list2 = TableHeaderFake.GetData(0);
            TableHeaderVMEntity.ConvertToVMEntities(list2).ForEach(x => CriteriaList.Add(x));
            CriteriaDataGrid.ItemsSource = CriteriaList;
        }

        private void CreateTableButton_Click(object sender, RoutedEventArgs e)
        {
            tb1.Text = null;
            targetCell = null;
            rtb.Document.Blocks.Clear();

            _tableContent = TsrFacade.GetTableContent(
                        TableHeaderVMEntity.GetEntities(HeaderList.ToList(), null),
                        TableHeaderVMEntity.GetEntities(CriteriaList.ToList(), null),
                        EnumTsrDocumentType.SpecSheet,
                        CriteriaPositionRadioButton.IsChecked);
            if (_tableContent == null) return;
            _cellList = TsrFacade.CreateCellList(_tableContent);

            _table = TsrFacade.CreateTableToRichTextBox(_tableContent, _cellList);
            rtb.Document.Blocks.Add(_table);

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
            rtb.Document.Blocks.Clear();
        }

        private void GetDataButton_Click(object sender, RoutedEventArgs e)
        {
            tb1.Text = string.Empty;
            if (targetCell != null)
            {
                targetCell.Background = targetColor;
            }
            if (rtb.Selection.Cells.Any())
            {
                var cell = rtb.Selection.Cells.First() as C1TableCell;
                if (cell is TsrDataCell dataCell)
                {
                    tb1.Text = string.Format("Row:{0}, Column:{1}  {3} \n{2}",
                        dataCell.RowIndex, dataCell.ColumnIndex, dataCell.Conditions,dataCell.Width.Value);
                }
                else if (cell is TsrHeaderCell headerCell)
                {
                    tb1.Text = string.Format("Row:{0}, Column:{1}, Width:{3}\n{2}",
                        headerCell.RowIndex, headerCell.ColumnIndex, headerCell.GetType().ToString(), headerCell.Width.Value);
                }

                targetColor = cell.Background;
                cell.Background = System.Windows.Media.Brushes.Red;
                CellWidthTextBox.Text = cell.Width.ToString();
                targetCell = cell;

            }
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

        private void PrintOutToWordButton_Click(object sender, RoutedEventArgs e)
        {
            var word = new C1WordDocument();
            var dlg = new SaveFileDialog();
            dlg.FileName = "document";
            dlg.DefaultExt = ".docx";
            dlg.Filter = "RTF files (*.rtf)|*.rtf|MS Word (Open XML) files (*.docx)|*.docx";
            var dr = dlg.ShowDialog();
            if (!dr.HasValue || !dr.Value)
            {
                return;
            }
            word.Clear();

            // ドキュメント情報を設定します
            var di = word.Info;
            di.Author = "ComponentOne";
            di.Subject = "C1.WPF.Word sample.";

            _cellList = TsrFacade.GetCellData(_cellList, _table);

            var table= TsrFacade.CreateTableToWord(_tableContent, _cellList);

            
            word.Add(table);

            using (var stream = dlg.OpenFile())
            {
                word.Save(stream, dlg.FileName.ToLower().EndsWith("docx") ? FileFormat.OpenXml : FileFormat.Rtf);
                MessageBox.Show("Word Document saved to " + dlg.SafeFileName);
            }
        }

        private void CellWidthChangeButton_Click(object sender, RoutedEventArgs e)
        {
            double width = Convert.ToDouble(CellWidthTextBox.Text);
            targetCell.Width = new C1Length(width);
        }

        private void DataInputButton_Click(object sender, RoutedEventArgs e)
        {
            var fm = new TableDataInputWindow(_cellList);
            fm.ShowDialog();
        }
    }
}
