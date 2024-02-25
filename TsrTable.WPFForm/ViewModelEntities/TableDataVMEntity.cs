using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TsrTable.Domain.Entities;
using TsrTable.Domain.ValueObjects;

namespace TsrTable.WPFForm.ViewModelEntities
{
    internal class TableDataVMEntity
    {
        public Conditions Conditions { get; }
        public double Value { get; set; } = 0;
        public Operators Operators { get; set; }
        public double Tolerance { get; set; } = 0;
        public ToleranceType ToleranceType { get; set; } = ToleranceType.Percent;

        public TableDataVMEntity(TableDataEntity entity)
        {
            Conditions = entity.Conditions;
            if (entity.Criteria == null) return;
            Value = entity.Criteria.Value;
            Operators = entity.Criteria.Operators;
            Tolerance = entity.Criteria.Tolerance;
            ToleranceType = entity.Criteria.ToleranceType;
        }

        internal static List<TableDataEntity> GetList(List<TableDataVMEntity> vmList)
        {
            var list = new List<TableDataEntity>();
            foreach (var vm in vmList)
            {
                Criteria criteria;
                if (vm.Operators == null) criteria = null;
                else criteria = new Criteria(vm.Value, vm.Operators, vm.Tolerance, vm.ToleranceType);
                list.Add(new TableDataEntity(vm.Conditions, criteria));
            }
            return list;
        }
    }
}
