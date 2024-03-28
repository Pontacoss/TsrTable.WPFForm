using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using TsrTable.RichTextBox;

namespace TsrTable.TableData
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

        public C1TextElement ToRtb()
        {
            return new RtbSuperScript(BaseScript, SubScript);
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
