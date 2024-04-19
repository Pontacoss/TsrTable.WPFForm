using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System;

namespace TsrTable.RichTextBox.TsrElement
{
    public sealed class TsrSubScript : ITsrElement
    {
        public string BaseScript { get; }
        public string SubScript { get; }
        public TsrSubScript(string baseScript, string subScript)
        {
            BaseScript = baseScript;
            SubScript = subScript;
        }

        public RtfObject ToWord()
        {
            throw new NotImplementedException();
        }

        public void ToFlexSheet(C1FlexSheet cfs)
        {
            throw new NotImplementedException();
        }

        public void ToExcel(C1XLBook book)
        {
            throw new NotImplementedException();
        }

        public C1TextElement GetRtbInstance() => new RtbSubScript(BaseScript, SubScript);

    }
}
