﻿using System.Windows;

namespace TsrTable.WPFForm
{
    /// <summary>
    /// SubTitleWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SubTitleWindow : Window
    {
        public string Text
        {
            get
            {
                return SubTitleText.Text ?? string.Empty;
            }
            private set
            {
                SubTitleText.Text = value ?? string.Empty;
            }
        }
        public SubTitleWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Text = string.Empty;
            this.Close();
        }
    }
}