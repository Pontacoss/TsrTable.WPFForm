using C1.WPF.RichTextBox.Documents;
using TsrTable.TableData;

namespace TsrTable.RichTextBox
{
    public class RtbSuperScript : C1Span, IRtbElement
    {
        public string BaseScript { get; }
        public string SuperScript { get; }
        public RtbSuperScript() { }
        public RtbSuperScript(string text, string superScript)
        {
            BaseScript = text;
            SuperScript = superScript;
            var run = new C1Run()
            {
                Text = text,
                IsEditable = false
            };
            var run2 = new C1Run
            {
                IsEditable = false,
                Text = superScript,
                VerticalAlignment = C1VerticalAlignment.Super
            };
            Children.Add(run);
            Children.Add(run2);
            IsEditable = false;
        }

        public ITsrElement GetTsrInstance()
            => new TsrSuperScript(BaseScript, SuperScript);

    }
}
