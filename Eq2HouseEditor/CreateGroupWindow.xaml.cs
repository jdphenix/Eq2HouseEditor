using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Eq2HouseEditor
{
    /// <summary>
    /// Interaction logic for CreateGroupWindow.xaml
    /// </summary>
    public partial class CreateGroupWindow : Window
    {
        public string Name { get; private set; }

        public CreateGroupWindow() {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) {
            TextBox textBox = sender as TextBox; 
            textBox.Clear();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e) {
            if (e.Key == Key.Tab || e.Key == Key.Enter) {
                TextBox textBox = sender as TextBox;
                Name = textBox.Text;
                Close();
            }
        }
    }
}
