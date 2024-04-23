using System;
using System.Collections.Generic;
using System.Text;
using TsrTable.Domain.Entities;

namespace TsrTable.TsrElement
{
    public abstract class HeaderBase
    {
        protected TableHeaderEntity _headerEntity;

        public bool IsColumn => _headerEntity.IsColumn;
        public bool IsTitleVisible => _headerEntity.IsTitleVisible;
        public bool IsMeasurementItem => _headerEntity.IsMeasurementItem;
        public bool IsRepeat => _headerEntity.IsRepeat;
        public int Id => _headerEntity.Id;
        public int Level => _headerEntity.Level;
        public string Name => _headerEntity.Name;
        public int Span => _headerEntity.Span;

        public IList<HeaderBase> Children { get; }
            = new List<HeaderBase>();

        public HeaderBase(TableHeaderEntity headerEntity)
        {
            _headerEntity = headerEntity;
        }

        public override string ToString()
        {
            return Name ?? "";
        }

        public abstract string DisplayName();

        public void Add(HeaderBase tableHeader)
        {
            Children.Add(tableHeader);
        }
        public int GetDepth()
        {
            int depth = Level;
            foreach (var item in Children)
            {
                depth = Math.Max(depth, item.GetDepth());
            }
            return depth;
        }
        public int GetSpanSum()
        {
            if (Children.Count == 0)
            {
                return Span;
            }
            int counter = 0;
            foreach (var item in Children)
            {
                counter += item.GetSpanSum();
            }
            return counter;
        }

        public TableHeaderEntity GetEntity()
        {
            return _headerEntity;
        }
        protected string GetConditionStringRecursive(int Index, int unitSize, int counter = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DisplayName());
            var subCounter = 0;
            foreach (var header in Children)
            {
                var width = header.GetSpanSum() * unitSize;
                counter += width;
                if (counter >= Index)
                {
                    sb.Append(header.GetConditionStringRecursive(Index, unitSize, subCounter));
                    break;
                }
                subCounter += width;
            }
            return sb.ToString();
        }

        public int CreateRowHeader(
            List<CellEntity> list,
            int rowIndex,
            int columnIndex,
            int unitSize,
            int maxDepth,
            int columnHeaderHeight)
        {
            var rowSpan = GetSpanSum() * unitSize;
            var columnSpan = Children.Count > 0 ? 1 : maxDepth - Level + 1;

            list.Add(new CellEntity(
                 rowIndex + columnHeaderHeight,
                 columnIndex,
                 0,
                 Name,
                 rowSpan,
                 columnSpan));

            var rowIndexSub = rowIndex;
            columnIndex += columnSpan;
            foreach (var cell in Children)
            {
                rowIndexSub += cell.CreateRowHeader(
                    list, rowIndexSub, columnIndex, unitSize, maxDepth, columnHeaderHeight);
            }
            return rowSpan;
        }
        public (int, int) CreateColumnHeader(
             List<CellEntity> list,
            int rowIndex,
            int columnIndex,
            int unitSize,
            int maxDepth)
        {
            var columnSpan = GetSpanSum() * unitSize;
            var rowSpan = Children.Count > 0 ? 1 : maxDepth - Level + 1;

            list.Add(new CellEntity(
                 rowIndex,
                 columnIndex,
                 EnumCellType.ColumnHeader,
                 Name,
                 rowSpan,
                 columnSpan));

            var columnIndexSub = columnIndex;
            var rowIndexSub = rowIndex + 1;
            foreach (var cell in Children)
            {
                (rowIndexSub, columnIndexSub) = cell.CreateColumnHeader(
                    list, rowIndexSub, columnIndexSub, unitSize, maxDepth);
            }
            columnIndex += columnSpan;

            return (rowIndex, columnIndex);
        }
    }
}
