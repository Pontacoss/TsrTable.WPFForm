using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System;
using System.Text.Json.Serialization;

namespace TsrTable.TsrElement

{
    internal class TsrRun : ITsrElement
    {
        // Jsonに書き出すプロパティはPublicにしておく
        public string Text { get; }

        // Jsonからデータを読みだすためのコンストラクタに属性付与
        // PublicのPropertyを引数に持つコンストラクタが必要。
        // 引数の変数名はプロパティと同じか小文字にしておく必要あり。
        [JsonConstructor]
        internal TsrRun(string text)
        {
            Text = text;
        }
        public void ToExcel(C1XLBook book)
        {
            throw new NotImplementedException();
        }

        public void ToFlexSheet(C1FlexSheet cfs)
        {
            throw new NotImplementedException();
        }

        public RtfObject ToWord()
        {
            throw new NotImplementedException();
        }

        public C1TextElement GetRtbInstance()
            => new C1Run() { Text = this.Text };

    }
}
