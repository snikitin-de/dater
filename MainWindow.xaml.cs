using System.Collections.Generic;
using System.Windows;

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
    }
}
