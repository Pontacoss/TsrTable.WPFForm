using C1.WPF.FlexGrid;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using TsrTable.Domain.Common;
using TsrTable.Domain.Entities;
using TsrTable.TsrElement;
using TsrTable.WPFForm.ViewModelEntities;

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

        private List<CellEntity> _cellList = new List<CellEntity>();
        private List<TableDataEntity> _datas;

        public Window1(
            List<TableHeaderVMEntity> headerList,
            List<TableHeaderVMEntity> criteriaList,
            List<TableDataEntity> datas)
        {
            InitializeComponent();
            _datas = datas;

            CriteriaPositionRadioButton.IsChecked = true;

            headerList.ForEach(x => HeaderList.Add(x));
            ContainerDataGrid.ItemsSource = HeaderList.ToList().FindAll(x => x.Parent == 0);

            criteriaList.ForEach(x => CriteriaList.Add(x));
            CriteriaDataGrid.ItemsSource = CriteriaList;

            InitializeGrid();
        }

        private void CreateTableButton_Click(object sender, RoutedEventArgs e)
        {
            tb1.Text = null;
            InitializeGrid();

            var tableContent = TsrFacadeForTable.GetTableContent(
                        TableHeaderVMEntity.GetEntities(HeaderList.ToList(), null),
                        TableHeaderVMEntity.GetEntities(CriteriaList.ToList(), null),
                        EnumTsrDocumentType.TestReport,
                        CriteriaPositionRadioButton.IsChecked);
            if (tableContent == null) return;

            _cellList = TsrFacadeForTable.CreateCellList(tableContent);

            TsrFacadeForTable.CreateTableToFlexSheet(cfs, _cellList, _datas);
        }

        private void GetDataButton_Click(object sender, RoutedEventArgs e)
        {
            //var scaleMode = ScaleMode.PageWidth;
            //cfs.PrintPreview("C1FlexSheet", scaleMode, new Thickness(96), int.MaxValue);

            var book = new C1.WPF.Excel.C1XLBook();
            TsrFacadeForTable.CreateTableToExcel(book, _cellList, _datas);
            book.Save("C:\\Users\\ey28754\\Desktop\\book.xlsx");
            MessageBox.Show("Excel Document saved ");

            //var dlg = new Microsoft.Win32.SaveFileDialog();
            //dlg.DefaultExt = "xlsx";
            //dlg.Filter =
            //    "Excel Workbook (*.xlsx)|*.xlsx|" +
            //    "Excel 97-2003 Workbook (*.xls)|*.xls|" +
            //    "HTML File (*.htm;*.html)|*.htm;*.html|" +
            //    "Comma Separated Values (*.csv)|*.csv|" +
            //    "Text File (*.txt)|*.txt|" +
            //    "PDF (*.pdf)|*.pdf";

            //if (dlg.ShowDialog().Value)
            //{
            //    using (var s = dlg.OpenFile())
            //    {
            //        var ext = System.IO.Path.GetExtension(dlg.SafeFileName).ToLower();
            //        switch (ext)
            //        {
            //            case ".htm":
            //            case ".html":
            //                cfs.Save(s, FileFormat.Html, SaveOptions.Formatted);
            //                break;
            //            case ".csv":
            //                cfs.Save(s, FileFormat.Csv, SaveOptions.Formatted);
            //                break;
            //            case ".txt":
            //                cfs.Save(s, FileFormat.Text, SaveOptions.Formatted);
            //                break;
            //            case ".pdf":
            //                SavePdf(s, "ComponentOne ExcelBook");
            //                break;
            //            case ".xlsx":
            //                cfs.SaveXlsx(s);
            //                break;
            //            default:
            //                cfs.SaveXls(s);
            //                break;
            //        }
            //    }
            //}
        }

        private void SavePdf(Stream s, string documentName)
        {
            PdfExportOptions options = new PdfExportOptions();
            options.Margin = new Thickness(96, 96, 96 / 2, 96 / 2);
            options.ScaleMode = ScaleMode.ActualSize;
            cfs.SavePdf(s, options);
            s.Close();
        }

        private void ContainerDataGrid_SelectedCellsChanged(object sender,
            System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            if (ContainerDataGrid.SelectedItem is TableHeaderVMEntity item)
            {
                tv1.ItemsSource = item.Children;
            }
        }

        private void SheetSpanColumnSeting(int gap)
        {
            var selection = cfs.Selection;
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
            InitializeGrid();
            TsrFacadeForTable.CreateTableToFlexSheet(cfs, _cellList, _datas);
        }

        private void SheetSpanRowSeting(int gap)
        {
            var selection = cfs.Selection;
            var rowElement = _cellList.FindAll(x =>
                x.SheetIndexRow <= selection.TopRow &&
                x.SheetIndexRow + x.SheetSpanRow - 1 >= selection.BottomRow);
            var rowElementMoveOnly = _cellList.FindAll(x =>
                x.SheetIndexRow > selection.BottomRow);

            foreach (var row in rowElement)
            {
                if (!row.CanChangeSpanRow(gap)) return;
            }
            foreach (var row in rowElement)
            {
                row.SetSheetSpanRow(gap);
            }
            foreach (var col in rowElementMoveOnly)
            {
                col.SheetIndexRow += gap;
            }
            InitializeGrid();
            TsrFacadeForTable.CreateTableToFlexSheet(cfs, _cellList, _datas);
        }

        private void ColumnSpanIncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            SheetSpanColumnSeting(1);
        }

        private void ColumnSpandecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            SheetSpanColumnSeting(-1);
        }

        private void RowSpandecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            SheetSpanRowSeting(-1);
        }

        private void RowSpanIncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            SheetSpanRowSeting(1);
        }

        private void InitializeGrid()
        {
            cfs.Columns.Clear();
            while (cfs.Columns.Count < TsrFacadeForTable.FlexSheetColumnCount)
            {
                cfs.Columns.Add(new Column()
                {
                    Width = new GridLength(TsrFacadeForTable.FlexSheetCellWidth)
                });
            }
        }
    }
}
