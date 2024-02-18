﻿using System.Collections.Generic;
using TsrTable.Domain.Entities;

namespace TsrTable.C1RichTextBox.TableData
{
    class CriteriaContainer : ContainerBase, IContainer
    {
        public CriteriaContainer(TableHeaderEntity headerEntity) : base(headerEntity) { }
        public SpanCounter GetHeaderWidth(SpanCounter spanCounter)
        {
            return new SpanCounter
            {
                BlockSpan = spanCounter.BlockSpan,
                RepeatSpan = spanCounter.RepeatSpan * GetSpanSum()
            };
        }
        public int SetUnitSize(SpanCounter spanCounter, int repaetHeaderUnitSize)
        {
            _unitSize = 1;// repaetHeaderUnitSize / GetSpanSum();
            return _unitSize;
        }

        public int SetRepeat(int repeat)
        {
            _repeat = repeat;
            return repeat * GetSpanSum();
        }

        public int CreateColumnHeaders(List<CellEntity> list, int rowIndex,int columnIndex)
        {
            int maxDepth = GetDepth();

            for (int i = 0; i < _repeat; i++)
            {
                foreach (var cell in Children)
                {
                    (rowIndex,columnIndex) = cell.CreateColumnHeader(
                        list, rowIndex, columnIndex, _unitSize, maxDepth);
                }
            }
            return 0;
        }

        public int CreateColumnContainerTitles(List<CellEntity> list, int rowIndex,int columnIndex)
        {
            if (!IsTitleVisible) return 0;
           
            int rowSpan = 1;
            int columnSpan = GetSpanSum() * _unitSize;

            list.Add(new CellEntity(
                 rowIndex,
                 columnIndex,
                 2,
                 ToString(),
                 rowSpan,
                 columnSpan));
            return 1;
        }

        public string GetConditionStringByContainer(int Index)
        {
            int width = GetSpanSum() * _unitSize;
            Index = Index % width;
            if (Index == 0) Index = width;

            return GetConditionStringRecursive(Index, _unitSize);
        }
        public override string DisplayName()
        {
            return "[" + Name?.Replace("\n", "-") + "] : ";
        }
    }
}
