using C1.WPF.RichTextBox.Documents;
using System.Windows;

namespace TsrTable.RichTextBox
{
    public class TsrSubTitle : C1Span
    {
        public string ItemNumber { get; private set; } = "[自動採番]";
        public string SubTitle { get; }

        public TsrSubTitle() { }
        public TsrSubTitle(string subTitle)
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
    }
}
