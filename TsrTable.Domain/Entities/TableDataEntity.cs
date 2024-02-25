using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsrTable.Domain.ValueObjects;

namespace TsrTable.Domain.Entities
{
    public class TableDataEntity
    {
        public Conditions Conditions { get; }
        public Criteria Criteria { get; set; }

        public TableDataEntity(Conditions conditions)
        {
            Conditions = conditions;
        }

        public TableDataEntity(Conditions conditions,Criteria criteria)
        {
            Conditions = conditions;
            Criteria = criteria;
        }
    }
}