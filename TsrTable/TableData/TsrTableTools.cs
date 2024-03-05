using TsrTable.Domain.Entities;
using System.Text;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System;
using TsrTable.TableData;
using TsrTable.Domain.Common;
using TsrTable.Domain.ValueObjects;

namespace TsrTable.RichTextBox.TableData
{
    internal static class TsrTableTools
    {
        /// <summary>
        /// 作成が必要なセルのリストを生成
        /// </summary>
        /// <param name="tableContent"></param>
        /// <returns></returns>
        internal static List<CellEntity> CreateCellList(TableContent tableContent)
        {
            // 生成するCellの情報生成
            var createCellList = new List<CellEntity>();
            // CellHeaderの作成
            CreateCellHeaderArea(tableContent, createCellList);
            // RowHeaderの作成
            CreateRowHeaderArea(tableContent, createCellList);
            // ColumnHeaderの作成
            CreateColumnHeaderArea(tableContent, createCellList);
            // DataCellの作成
            CreateDataCellArea(tableContent, createCellList);

            return createCellList;
        }
        /// <summary>
        /// 表作成に必要なデータを生成する。表を作成出来ない場合、Nullを返す。
        /// </summary>
        /// <param name="headerList"></param>
        /// <param name="criteriaList"></param>
        /// <param name="documentType"></param>
        /// <param name="criteriaPosition"></param>
        /// <returns></returns>
        internal static TableContent   GetTableContent(
            List<TableHeaderEntity> headerList,
            List<TableHeaderEntity> criteriaList,
            EnumTsrDocumentType documentType,
            bool? criteriaPosition = false)
        {
            // ヘッダーを行、列に振り分け
            var rowHeaderList = GetItemSource(
                headerList.ToList().FindAll(x => x.IsColumn == false));
            var columnHeaderList = GetItemSource(
                headerList.ToList().FindAll(x => x.IsColumn == true));
            // 基準値コンテナをメインコンテナに格納＋ヘッダーを追加
            var entities = new List<TableHeaderEntity>();
            entities.Add(new TableHeaderEntity(1000, "試験項目", false, true, true));
            entities.AddRange(criteriaList);
            foreach (var criteriaSubContainer in criteriaList)
            {
                entities.AddRange(CriteriaHeaderSetting(criteriaSubContainer, documentType));
            }
            // 基準値を行、列に振り分け
            if (criteriaPosition == true)
                columnHeaderList.AddRange(GetItemSource(entities));
            else
                rowHeaderList.AddRange(GetItemSource(entities));
            //　現状、列ヘッダーなしでは表が作れない。
            if (columnHeaderList.Count == 0)
            {
                MessageBox.Show("列ヘッダーを一つ以上配置してください。", "",
                    MessageBoxButton.OK);
                return null;
            }
            else if (rowHeaderList.Count == 0)
            {
                return null;
            }
            return new TableContent("name", rowHeaderList, columnHeaderList);
        }

        private static Conditions GetConditionString(TableContent tableContent, int rowIndex, int columnIndex)
        {
            var list = new Conditions();
            if (tableContent.RowHeaders != null)
            {
                list.Add(GetConditionStringSub(tableContent.RowHeaders, rowIndex));
            }
            list.Add(GetConditionStringSub(tableContent.ColumnHeaders, columnIndex));
            return list;
        }

        private static List<string> GetConditionStringSub(IEnumerable<HeaderBase> HeaderList, int Index)
        {
            var sb = new StringBuilder();
            var list=new List<string>();
            foreach (var header in HeaderList.OfType<IContainer>())
            {
                var condition = header.GetConditionStringByContainer(Index);
                if(condition != null)
                    list.Add(header.GetConditionStringByContainer(Index));
            }
            return list;
        }

        private static List<HeaderBase> GetItemSource(IList<TableHeaderEntity> list)
        {
            var source = new List<HeaderBase>();

            foreach (var entity in list)
            {
                var parent = GetParent(source, entity.Parent);
                if (parent != null)
                {
                    if (entity.IsMeasurementItem)
                    {
                        parent.Add(new CriteriaContainer(entity));
                    }
                    else
                    {
                        parent.Add(new Header(entity));
                    }
                }
                else
                {
                    if (entity.IsMeasurementItem)
                    {
                        source.Add(new CriteriaContainer(entity));
                    }
                    else if (entity.IsRepeat)
                    {
                        source.Add(new RepeartContainer(entity));
                    }
                    else
                    {
                        if (entity.IsColumn)
                        {
                            source.Add(new RepeartContainer(entity));
                        }
                        else
                        {
                            source.Add(new BlockContainer(entity));
                        }
                    }
                }
            }
            return source;
        }

        private static HeaderBase GetParent(IList<HeaderBase> list, int parentId)
        {
            foreach (var entity in list)
            {
                if (entity.Id == parentId) return entity;

                var target = GetParent(entity.Children, parentId);

                if (target != null) return target;
            }
            return null;
        }

        private static List<TableHeaderEntity> CriteriaHeaderSetting(
            TableHeaderEntity criteriaSubContainer,
            EnumTsrDocumentType documentType)
        {
            var criteriaHeaders = new List<TableHeaderEntity>();
            
            if (documentType == EnumTsrDocumentType.SpecSheet)
            {
                return criteriaHeaders;
                //criteriaHeaders.Add(
                //    new TableHeaderEntity(
                //        Convert.ToInt32(criteriaSubContainer.Id + "2"),
                //        "公差",
                //        criteriaSubContainer.Id,
                //        criteriaSubContainer.Level));
                //criteriaHeaders.Add(
                //    new TableHeaderEntity(
                //        Convert.ToInt32(criteriaSubContainer.Id + "3"),
                //        "範囲",
                //        Convert.ToInt32(criteriaSubContainer.Id ),
                //        criteriaSubContainer.Level));
                //criteriaHeaders.Add(
                //    new TableHeaderEntity(
                //        Convert.ToInt32(criteriaSubContainer.Id + "4"),
                //        "値",
                //        Convert.ToInt32(criteriaSubContainer.Id ),
                //        criteriaSubContainer.Level));
                //criteriaHeaders.Add(
                //    new TableHeaderEntity(
                //        Convert.ToInt32(criteriaSubContainer.Id + "5"),
                //        "単位",
                //        Convert.ToInt32(criteriaSubContainer.Id ),
                //        criteriaSubContainer.Level));
            }
            else
            {
                criteriaHeaders.Add(
                    new TableHeaderEntity(
                        Convert.ToInt32(criteriaSubContainer.Id + "1"),
                        "基準値",
                        criteriaSubContainer.Id,
                        criteriaSubContainer.Level));
                criteriaHeaders.Add(
                    new TableHeaderEntity(
                         Convert.ToInt32(criteriaSubContainer.Id + "2"),
                        "測定値",
                        criteriaSubContainer.Id,
                        criteriaSubContainer.Level));

                criteriaHeaders.Add(
                        new TableHeaderEntity(
                             Convert.ToInt32(criteriaSubContainer.Id + "3"),
                            "判定",
                            criteriaSubContainer.Id,
                            criteriaSubContainer.Level));
            }
            return criteriaHeaders;
        }

        private static void CreateCellHeaderArea(
                TableContent tableContent,
                List<CellEntity> list)
        {
            if (tableContent.RowHeaders == null) return;

            var colCounter = 0;
            foreach (var container in tableContent.RowHeaders.OfType<ContainerBase>())
            {
                var cellEntity = container.CreateCellHeader(tableContent.ColumnHeaderHeight, colCounter);
                list.Add(cellEntity);
                colCounter += cellEntity.ColumnSpan;
            }
        }

        private static void CreateRowHeaderArea(
            TableContent tableContent,
            List<CellEntity> list)
        {
            if (tableContent.RowHeaders is null) return;
            // Container毎にRowHeaderを作成
            var columnIndex = 0;
            foreach (var container in tableContent.RowHeaders.OfType<ContainerBase>())
            {
                columnIndex += container.CreateRowHeaders(list, tableContent.ColumnHeaderHeight, columnIndex);
            }
        }

        private static void CreateDataCellArea(
            TableContent tableContent,
           List<CellEntity> list)
        {
            for (int i = 0; i < tableContent.RowHeaderHeight; i++)
            {
                for (int j = 0; j < tableContent.ColumnHeaderWidth; j++)
                {
                    var conditions = GetConditionString(tableContent, i + 1, j + 1);
                    list.Add(new CellEntity(
                        i + tableContent.ColumnHeaderHeight,
                        j + tableContent.RowHeaderWidth,
                        EnumCellType.DataCell, 1, 1, conditions));
                }
            }
        }

        private static void CreateColumnHeaderArea(
            TableContent tableContent,
            List<CellEntity> list)
        {
            var rowIndex = 0;
            var columnIndex = tableContent.RowHeaderWidth;
            foreach (var container in tableContent.ColumnHeaders.OfType<IContainer>())
            {
                rowIndex += container.CreateColumnContainerTitles(list, rowIndex, columnIndex);
                rowIndex += container.CreateColumnHeaders(list, rowIndex, columnIndex);
            }
        }

        internal static string GetCellContent(CellEntity cell, List<TableDataEntity> datas,EnumTsrDocumentType type)
        {
            if (datas != null)
            {
                var entity = datas.FirstOrDefault(x => x.Conditions == cell.Conditions);
                if (entity !=null && entity.Criteria != null)
                {
                    if (type == EnumTsrDocumentType.TestReport)
                    {
                        return entity.Criteria.DisplayRange;
                    }
                    else
                    {
                        return entity.Criteria.DisplayValue;
                    }
                }
            }
            return cell.Value;
        }
    }
}
