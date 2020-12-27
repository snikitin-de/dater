using dater;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Список типов данных для генерации
        private List<string> dataTypes = new List<string> { "String", "Integer", "Float", "Datetime", "Boolean", "Variant", "GUID" };

        public MainWindow()
        {
            InitializeComponent();
            cmbDataType.ItemsSource = dataTypes;
        }

        private void RowCountTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !Validator.IsValidPositiveInt(((TextBox)sender).Text + e.Text);
        }

        private void SeparatorTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !Validator.IsValidSeparator(((TextBox)sender).Text + e.Text);
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
