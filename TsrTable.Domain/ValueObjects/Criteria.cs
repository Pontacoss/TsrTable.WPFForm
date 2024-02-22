﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsrTable.Domain.Common;

namespace TsrTable.Domain.ValueObjects
{
    public sealed class Criteria : ValueObject<Criteria>
    {
        public double Value { get; }
        public Operators Operators { get; }
        public double Tolerance { get; }
        public ToleranceType ToleranceType { get; }

        public Criteria(double value, Operators operators, double tolerance, ToleranceType type)
        {
            Value = value;
            Operators = operators;
            Tolerance = tolerance;
            ToleranceType = type;
        }

        public override string ToString()
        {
            if (Operators.Value < 3)
            {
                return string.Format("{0} {1} {2}{3}", Value, Operators.ToString(), Tolerance, ToleranceType.ToString());
            }
            else
            {
                return string.Format("{0} {1}", Value, Operators.ToString());
            }
        }

        public string DisplayValue => this.ToString();
        
        public string DisplayRange
        {
            get
            {
                if (Operators.Value < 3)
                {
                    if (ToleranceType == ToleranceType.Percent)
                    {
                        if (Operators == Operators.PlusMinus)
                            return string.Format("{0} ～ {1} ～ {2}", Value - (1 - Tolerance / 100), Value, Value - (1 + Tolerance / 100));
                        else if (Operators == Operators.Minus)
                            return string.Format("{0} ～ {1}", Value - (1 - Tolerance / 100), Value);
                        else
                            return string.Format("{0} ～ {1}", Value, Value - (1 + Tolerance / 100));
                    }
                    else
                    {
                        if (Operators == Operators.PlusMinus)
                            return string.Format("{0} ～ {1} ～ {2}", Value - Tolerance, Value, Value + Tolerance);
                        else if (Operators == Operators.Minus)
                            return string.Format("{0} ～ {1}", Value - Tolerance, Value);
                        else
                            return string.Format("{0} ～ {1}", Value, Value + Tolerance);
                    }
                }
                else return this.ToString();
            }
        }

        protected override bool EqualsCore(Criteria other)
        {
            return this.Value == other.Value &&
                this.Operators == other.Operators &&
                this.Tolerance == other.Tolerance &&
                this.ToleranceType == other.ToleranceType;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}
