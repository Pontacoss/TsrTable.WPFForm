using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System;
using System.Text.Json.Serialization;
using TsrTable.RichTextBox;

namespace TsrTable.TsrElement
{
    internal sealed class TsrSuperScript : ITsrElement
    {
        public string BaseScript { get; }
        public string SuperScript { get; }

        [JsonConstructor]
        internal TsrSuperScript(string baseScript, string superScript)
        {
            BaseScript = baseScript;
            SuperScript = superScript;
        }

        public RtfObject ToWord()
        {
            throw new NotImplementedException();
        }

        public void ToFlexSheet(C1FlexSheet cfs)
        {
            throw new System.NotImplementedException();
        }

        public void ToExcel(C1XLBook book)
        {
            throw new System.NotImplementedException();
        }

        public C1TextElement GetRtbInstance()
            => new RtbSuperScript(BaseScript, SuperScript);

    }
}
