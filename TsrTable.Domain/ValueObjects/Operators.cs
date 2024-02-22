using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace TsrTable.Domain.ValueObjects
{
    public sealed class Operators : ValueObject<Operators>
    {
        public int Value;
        public static readonly Operators Plus = new Operators(0);
        public static readonly Operators Minus = new Operators(1);
        public static readonly Operators PlusMinus = new Operators(2);
        public static readonly Operators GreaterEqual = new Operators(3);
        public static readonly Operators Less = new Operators(4);
        public static readonly Operators LessEqual = new Operators(5);

        public Operators(int value)
        {
            Value = value;   
        }

        public static IEnumerable<Operators> Items
        {
            get
            {
                return new List<Operators>()
                {
                    new Operators(0),new Operators(1),new Operators(2),new Operators(3),new Operators(4),new Operators(5)
                    //"+","－","±","以上","未満","以下"
                };
            }
        }

        public override string ToString()
        {
            if (Value == 0) return "+";
            else if (Value == 1) return "-";
            else if (Value == 2) return "±";
            else if (Value == 3) return "以上";
            else if (Value == 4) return "未満";
            else return "以下";
        }

        protected override bool EqualsCore(Operators other)
        {
            return this.Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}
