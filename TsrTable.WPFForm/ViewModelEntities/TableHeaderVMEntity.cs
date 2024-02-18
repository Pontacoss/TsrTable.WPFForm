using TsrTable.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TsrTable.WPFForm.ViewModelEntities
{
    public class TableHeaderVMEntity
    {
        private TableHeaderEntity _entity;
        public int Id =>_entity.Id;
        public int Parent =>_entity.Parent;
        public string Name =>_entity.Name;
        public int Level
        {
            get => _entity.Level;
            set => _entity.Level = value;
        } 
        public int Span
        {
            get => _entity.Span;
            set => _entity.Span = value;
        }
        public bool IsTitleVisible
        {
            get => _entity.IsTitleVisible;
            set => _entity.IsTitleVisible = value;
        }
        public bool IsMeasurementItem => _entity.IsMeasurementItem;
        public bool IsRepeat
        {
            get => _entity.IsRepeat;
            set => _entity.IsRepeat = value;
        }
        public bool IsColumn
        {
            get => _entity.IsColumn;
            set => _entity.IsColumn = value;
        }
        public ObservableCollection<TableHeaderVMEntity> Children { get; private set; }
        = new ObservableCollection<TableHeaderVMEntity>();

        public TableHeaderVMEntity(TableHeaderEntity entity)
        {
            _entity = entity;
        }
        public TableHeaderVMEntity(TableHeaderEntity entity, TableHeaderVMEntity parent)
        {
            _entity = entity;
            _entity.Parent = parent.Id;
            _entity.Level = parent.Level + 1;
        }
        public TableHeaderVMEntity(string name, TableHeaderVMEntity parent)
        {
            var id = parent.Id * 100 + parent.Children.Count + 1;
            _entity=new TableHeaderEntity(id, name, parent.Id,parent.Level);
        }

        public void Add (TableHeaderVMEntity entity)
        {
            Children.Add(entity);
        }

        public TableHeaderEntity GetEntity()
        {
            return _entity;
        }

        public static List<TableHeaderEntity> GetEntities(IList<TableHeaderVMEntity> list, TableHeaderVMEntity parent)
        {
            var entities = new List<TableHeaderEntity>();
            foreach (var item in list)
            {
                var entity = item.GetEntity();

                if (parent != null)
                {
                    entity.Parent = parent.Id;
                    entity.Level = parent.Level + 1;
                    entity.IsColumn = parent.IsColumn;
                    entity.IsMeasurementItem = parent.IsMeasurementItem;
                    entity.IsRepeat = parent.IsRepeat;
                }
                entities.Add(entity);
                if (item.Children.Count > 0)
                {
                    entities.AddRange(GetEntities(item.Children, item));
                }
            }
            return entities;
        }
        public static TableHeaderVMEntity SeekParent(List<TableHeaderVMEntity> entities, int parentId)
        {
            foreach (var entity in entities)
            {
                if (entity.Id == parentId)
                {
                    return entity;
                }
                else
                {
                    var result = SeekParent(entity.Children.ToList(), parentId);
                    if (result != null) return result;
                }
            }
            return null;
        }
        public static List<TableHeaderVMEntity> ConvertToVMEntities(List<TableHeaderEntity> entities)
        {
            var vmEntities = new List<TableHeaderVMEntity>();
            foreach (var entity in entities)
            {
                var parent = SeekParent(vmEntities, entity.Parent);
                if (parent == null)
                {
                    vmEntities.Add(new TableHeaderVMEntity(entity));
                }
                else
                {
                    parent.Add(new TableHeaderVMEntity(entity, parent));
                }
            }
            return vmEntities;
        }
    }
}
