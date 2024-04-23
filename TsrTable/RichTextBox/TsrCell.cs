using C1.WPF.RichTextBox.Documents;
using System.Linq;
using TsrTable.TsrElement;

namespace TsrTable.RichTextBox
{
    public class TsrCell : C1TableCell
    {
        protected CellEntity _cellEntity;
        public int RowIndex => _cellEntity.RowIndex;
        public int ColumnIndex => _cellEntity.ColumnIndex;
        public string Value
        {
            get
            {
                var run = this.Children[0].Children.FirstOrDefault(x => x.GetType() == typeof(C1Run)) as C1Run;
                if (run == null) return string.Empty;
                return run.Text;
            }
        }
        public TsrCell() : base() { }
        internal TsrCell(CellEntity cellEntity) : base()
        {
            _cellEntity = cellEntity;
        }
    }
}
