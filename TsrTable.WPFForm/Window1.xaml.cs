using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TsrTable.RichTextBox.TableData;
using TsrTable.WPFForm.ViewModelEntities;
using C1.WPF.FlexGrid;
using System.Collections.Generic;
using TsrTable.TableData;
using TsrTable.Domain.Common;
using TsrTable.FlexSheet;
using System.IO;
using C1.WPF.Excel;
using TsrTable.Domain.Entities;

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
        private List<TableDataEntity> _datas;

        public Window1(List<TableDataEntity> datas)
        {
            InitializeComponent();
            _datas = datas;

            CriteriaPositionRadioButton.IsChecked = true;

            var list = TableHeaderFake.GetData(1);
            TableHeaderVMEntity.ConvertToVMEntities(list).ForEach(x => HeaderList.Add(x));
            ContainerDataGrid.ItemsSource = HeaderList.ToList().FindAll(x => x.Parent == 0);

            var list2 = TableHeaderFake.GetData(0);
            TableHeaderVMEntity.ConvertToVMEntities(list2).ForEach(x => CriteriaList.Add(x));
            CriteriaDataGrid.ItemsSource = CriteriaList;
            
            InitializeGrid();
        }
        
        private void CreateTableButton_Click(object sender, RoutedEventArgs e)
        {
            tb1.Text = null;
            InitializeGrid();

            var tableContent = TsrFacade.GetTableContent(
                        TableHeaderVMEntity.GetEntities(HeaderList.ToList(), null),
                        TableHeaderVMEntity.GetEntities(CriteriaList.ToList(), null),
                        EnumTsrDocumentType.TestReport,
                        CriteriaPositionRadioButton.IsChecked);
            if (tableContent == null) return;

            _cellList = TsrFacade.CreateCellList(tableContent);

            TsrFacade.CreateTableToFlexSheet(cfs, _cellList,_datas);
        }

        private void GetDataButton_Click(object sender, RoutedEventArgs e)
        {
            //var scaleMode = ScaleMode.PageWidth;
            //cfs.PrintPreview("C1FlexSheet", scaleMode, new Thickness(96), int.MaxValue);

            var book = new C1.WPF.Excel.C1XLBook();
            TsrFacade.CreateTableToExcel(book, _cellList);
            book.Save("C:\\Users\\ey28754\\Desktop\\book.xlsx");

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
            TsrFacade.CreateTableToFlexSheet(cfs,_cellList, _datas);
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
            TsrFacade.CreateTableToFlexSheet(cfs, _cellList, _datas);
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
            while (cfs.Columns.Count < TsrFacade.FlexSheetWidth)
            {
                cfs.Columns.Add(new Column());
            }
            foreach (var col in cfs.Columns)
            {
                col.Width = new GridLength(TsrFacade.FlexSheetCellWidth);
            }
        }
    }
}
