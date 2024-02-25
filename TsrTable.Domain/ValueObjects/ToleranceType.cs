using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace TsrTable.Domain.ValueObjects
{
    public sealed class ToleranceType : ValueObject<ToleranceType>
    {
        public int Value;
        public static ToleranceType None = new ToleranceType(0);
        public static ToleranceType Percent = new ToleranceType(1);
        public static ToleranceType Absolute = new ToleranceType(2);

        public ToleranceType(int value)
        {
            Value = value;
        }

        public static IEnumerable<ToleranceType> Items
        {
            get
            {
                return new List<ToleranceType>()
                {
                    new ToleranceType(0),
                    new ToleranceType(1),
                    new ToleranceType(2)
                };
            }
        }
        public  string DisplayValue
        {
            get
            {
                if (Value == 1) return "%";
                if (Value == 2) return "(固定値)";
                else return string.Empty;
            }
        }
        protected override bool EqualsCore(ToleranceType other)
        {
            return this.Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}
