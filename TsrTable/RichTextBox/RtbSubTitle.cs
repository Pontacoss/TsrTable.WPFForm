using C1.WPF.RichTextBox.Documents;
using System.Windows;
using TsrTable.TableData;

namespace TsrTable.RichTextBox
{
    public class RtbSubTitle : C1Paragraph, IRtbElement
    {
        public string ItemNumber { get; private set; } = "[自動採番]";
        public string SubTitle { get; }

        public RtbSubTitle() { }
        public RtbSubTitle(string subTitle)
        {
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
