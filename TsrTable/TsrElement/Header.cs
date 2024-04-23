using TsrTable.Domain.Entities;

namespace TsrTable.TsrElement
{
    internal sealed class Header : HeaderBase
    {
        internal Header(TableHeaderEntity headerEntity) : base(headerEntity) { }

        public override string DisplayName()
        {
            return "[" + Name + "]";
        }
    }
}
