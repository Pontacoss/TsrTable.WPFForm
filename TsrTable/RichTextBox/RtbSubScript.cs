﻿using C1.WPF.RichTextBox.Documents;
using TsrTable.TableData;

namespace TsrTable.RichTextBox
{
    public class RtbSubScript : C1Span, IRtbElement
    {
        public string BaseScript { get; }
        public string SubScript { get; }
        public RtbSubScript() { }
        public RtbSubScript(string text, string subScript)
        {
            BaseScript = text;
            SubScript = subScript;
            var run = new C1Run()
            {
                Text = text,
                IsEditable = false
            };
            var run2 = new C1Run
            {
                IsEditable = false,
                Text = subScript,
                VerticalAlignment = C1VerticalAlignment.Sub
            };
            Children.Add(run);
            Children.Add(run2);
        }
        public ITsrElement ToTsr()
        {
            return new TsrSubScript(BaseScript, SubScript);
        }
    }
}
