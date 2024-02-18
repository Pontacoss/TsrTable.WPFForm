using TsrTable.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TsrTable.C1RichTextBox.TableData
{

    public abstract class ContainerBase : HeaderBase
    {
        protected int _repeat;
        protected int _unitSize;

        protected ContainerBase(TableHeaderEntity tableHeaderEntity) : base(tableHeaderEntity) { }
      
        public int CreateRowHeaders(List<CellEntity> list, int columnHeaderHeight, int columnIndex)
        {
            int rowIndex = 0;
            int maxDepth = GetDepth();

            for (int i = 0; i < _repeat; i++)
            {
                foreach (var cell in Children)
                {
                    rowIndex += cell.CreateRowHeader(list, rowIndex, columnIndex, _unitSize, maxDepth, columnHeaderHeight);
                }
            }
            return maxDepth;
        }

        public CellEntity CreateCellHeader(int columnHeaderHeight, int columnIndex)
        {
            return new CellEntity(0, columnIndex, 1, Name, columnHeaderHeight, GetDepth());
        }
    }
}
