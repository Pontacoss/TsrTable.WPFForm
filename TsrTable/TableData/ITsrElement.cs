using C1.WPF.Excel;
using C1.WPF.FlexGrid;
using C1.WPF.RichTextBox.Documents;
using C1.WPF.Word.Objects;
using System.Text.Json.Serialization;
using TsrTable.RichTextBox.TableData;

namespace TsrTable.TableData
{
    [JsonDerivedType(typeof(TsrSentence), nameof(TsrSentence))]
    [JsonDerivedType(typeof(TsrParagraph), nameof(TsrParagraph))]
    [JsonDerivedType(typeof(TsrSpan), nameof(TsrSpan))]
    [JsonDerivedType(typeof(TsrRun), nameof(TsrRun))]
    [JsonDerivedType(typeof(TsrInlineFigure), nameof(TsrInlineFigure))]
    [JsonDerivedType(typeof(TsrParameter), nameof(TsrParameter))]
    [JsonDerivedType(typeof(TsrBullet), nameof(TsrBullet))]
    [JsonDerivedType(typeof(TsrBulletItem), nameof(TsrBulletItem))]
    [JsonDerivedType(typeof(TsrStrikethrough), nameof(TsrStrikethrough))]
    [JsonDerivedType(typeof(TsrSubScript), nameof(TsrSubScript))]
    [JsonDerivedType(typeof(TsrSubTitle), nameof(TsrSubTitle))]
    [JsonDerivedType(typeof(TsrSuperScript), nameof(TsrSuperScript))]
    [JsonDerivedType(typeof(TsrPostScript), nameof(TsrPostScript))]
    public interface ITsrElement
    {
        C1TextElement GetRtbInstance();
        RtfObject ToWord();
        void ToFlexSheet(C1FlexSheet cfs);
        void ToExcel(C1XLBook book);
    }
}
