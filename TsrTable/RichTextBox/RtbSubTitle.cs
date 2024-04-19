using C1.WPF.RichTextBox.Documents;
using System.Windows;
using TsrTable.RichTextBox.TsrElement;

namespace TsrTable.RichTextBox
{
    public class RtbSubTitle : C1Paragraph, IRtbElement
    {
        public string ItemNumber { get; private set; }
        public string SubTitle { get; }

        public RtbSubTitle() { }
        public RtbSubTitle(string subTitle, string itemNumber = "[自動採番]")
        {
            ItemNumber = itemNumber;
            IsEditable = false;
            Margin = new Thickness(0);
            SubTitle = subTitle;
            var itemNumberRun = new C1Run()
            {
                Text = ItemNumber + " ",
                Background = null,
                BorderThickness = new Thickness(0),
                Padding = new Thickness(0),
                FontWeight = FontWeights.Bold,
            };
            var subTitleRun = new C1Run()
            {
                Text = SubTitle,
                Background = null,
                BorderThickness = new Thickness(0),
                Padding = new Thickness(0),
                FontWeight = FontWeights.Bold,
            };
            var paragraph = new C1Paragraph()
            {
                Margin = new Thickness(20, 0, 0, 0),
            };
            this.Children.Add(itemNumberRun);
            this.Children.Add(subTitleRun);
        }

        public override C1TextElement Clone()
        {
            return new RtbSubTitle(SubTitle);
        }

        public ITsrElement GetTsrInstance()
        {
            return new TsrSubTitle(SubTitle);
        }
    }
}
