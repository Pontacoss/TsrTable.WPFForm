using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TsrTable.Domain.ValueObjects
{
    public class Conditions : ValueObject<Conditions>
    {
        public List<string> Values;
        public Conditions()
        {
            Values = new List<string>();
        }
        public Conditions(List<string> values)
        {
            Values = values;
        }
        public void Add(List<string> list)
        {
            Values.AddRange(list);
        }
        protected override bool EqualsCore(Conditions other)
        {
            foreach(var item in Values)
            {
                if( other.Values.FirstOrDefault(x => x == item)==null)
                    return false;
            }
            return true;
        }

        protected override int GetHashCodeCore()
        {
            return this.GetHashCode();
        }
        public override string ToString()
        {
            return string.Join("\n", Values);
        }
    }
}
