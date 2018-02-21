using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ReportErrorParser.Logic;

namespace ReportErrorParser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            var clipboardText = Clipboard.GetText();
            if (!string.IsNullOrWhiteSpace(clipboardText))
            {
                ErrorTextBox.Text = clipboardText;
            }
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            var sqlText = SqlTextBox.Text;
            if (!string.IsNullOrWhiteSpace(sqlText))
            {
                Clipboard.SetText(sqlText);
            }
        }

        private void Parse_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ErrorTextBox.Text))
            {
                MessageBox.Show("Nothing to parse!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var result = Parser.Parse(ErrorTextBox.Text);
                SqlTextBox.Text = result;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Parsing error");
            }
        }
    }
}
