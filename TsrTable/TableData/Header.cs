using TsrTable.Domain.Entities;

namespace TsrTable.C1RichTextBox.TableData
{
    public sealed class Header : HeaderBase
    {
        public Header(TableHeaderEntity headerEntity) : base(headerEntity) { }

        public override string DisplayName()
        {
            return "[" + Name + "]";
        }
    }
}
