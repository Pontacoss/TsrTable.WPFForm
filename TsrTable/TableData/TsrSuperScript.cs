using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using TsrTable.RichTextBox;

namespace TsrTable.TableData
{
    public sealed class TsrSuperScript : ITsrElement
    {
        public string BaseScript { get; }
        public string SuperScript { get; }
        public TsrSuperScript(string baseScript, string superScript)
        {
            BaseScript = baseScript;
            SuperScript = superScript;
        }

        public C1TextElement ToRtb()
        {
            return new RtbSuperScript(BaseScript, SuperScript);
        }

        public RtfObject ToWord()
        {
            throw new System.NotImplementedException();
        }

        public void ToFlexSheet(C1FlexSheet cfs)
        {
            throw new System.NotImplementedException();
        }

        public void ToExcel(C1XLBook book)
        {
            throw new System.NotImplementedException();
        }
    }
}
