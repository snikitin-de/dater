using dater;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;

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
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var records = dataGrid.View.Records;
            var generator = new Generator();

            int rowCount = int.Parse(RowCountTextBox.Text);

            string separator = SeparatorTextBox.Text;
            string path = OutputFileTextBox.Text;

            using (var writer = new StreamWriter(path))
            {
                string headers = "";

                foreach (var record in records)
                {
                    ColumnInfo column = (ColumnInfo)record.Data;

                    headers += column.ColumnTitle.ToString() + separator;
                }

                headers = headers.Remove(headers.Length - 1);

                writer.WriteLine(headers);
                writer.Flush();

                for (int i = 0; i < rowCount; i++)
                {
                    var generatedRow = "";

                    foreach (var record in records)
                    {
                        ColumnInfo column = (ColumnInfo)record.Data;

                        switch (column.DataType.ToString())
                        {
                            case "String":
                                generatedRow += generator.generateString() + separator;
                                break;
                            case "Integer":
                                generatedRow += generator.generateInteger() + separator;
                                break;
                            case "Float":
                                generatedRow += generator.generateFloat() + separator;
                                break;
                            case "Datetime":
                                generatedRow += generator.generateDatetime() + separator;
                                break;
                            case "Boolean":
                                generatedRow += generator.generateBoolean() + separator;
                                break;
                            case "Variant":
                                generatedRow += generator.generateVariant() + separator;
                                break;
                            case "GUID":
                                generatedRow += generator.generateGUID() + separator;
                                break;
                        }
                    }

                    generatedRow = generatedRow.Remove(generatedRow.Length - 1);

                    writer.WriteLine(generatedRow);
                    writer.Flush();
                }
            }

            sw.Stop();

            string elapsedGenerationTime = (sw.ElapsedMilliseconds / 1000.0).ToString();

            ElapsedGenerationTimeLabel.Text = $"{RowCountTextBox.Text} rows has generated in {elapsedGenerationTime} seconds";
        }

        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            var addButton = sender as FrameworkElement;

            if (addButton != null)
            {
                addButton.ContextMenu.IsOpen = true;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var metadata = new DatasetMetaInfo(int.Parse(RowCountTextBox.Text), SeparatorTextBox.Text, OutputFileTextBox.Text);
            var config = new Config();
            var saveDialog = new SaveFileDialog();

            saveDialog.FileName = "config";
            saveDialog.DefaultExt = ".json";
            saveDialog.Filter = "JSON|*.json";

            bool? result = saveDialog.ShowDialog();

            if (result == true)
            {
                string filename = saveDialog.FileName;

                config.SaveConfig(filename, metadata, dataGrid.DataContext);
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            string applicationName = Application.ResourceAssembly.GetName().Name;

            var config = new Config();
            var openDialog = new OpenFileDialog();

            if (openDialog.ShowDialog() == true)
            {
                string filename = openDialog.FileName;

                var json = config.LoadConfig(filename);

                if (json != null)
                {
                    if (json["RowCount"] != null)
                    {
                        string rowCount = json["RowCount"].ToString();

                        if (Validator.IsValidPositiveInt(rowCount))
                        {
                            RowCountTextBox.Text = rowCount;
                        }
                        else
                        {
                            MessageBox.Show("Invalid row count value!", applicationName, MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }

                    if (json["Separator"] != null)
                    {
                        string separator = json["Separator"].ToString();

                        if (Validator.IsValidSeparator(separator))
                        {
                            SeparatorTextBox.Text = separator;
                        }
                        else
                        {
                            MessageBox.Show("Invalid separator value!", applicationName, MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }

                    if (json["OutputFile"] != null)
                    {
                        OutputFileTextBox.Text = json["OutputFile"].ToString();
                    }

                (dataGrid.DataContext as ColumnViewModel).Columns.Clear();

                    foreach (var column in json["Columns"])
                    {
                        if (column["ColumnTitle"] != null && column["DataType"] != null && column["MissingValues"] != null && column["Parameters"] != null)
                        {
                            (dataGrid.DataContext as ColumnViewModel).Columns.Add(
                                new ColumnInfo(
                                    column["ColumnTitle"].ToString(),
                                    column["DataType"].ToString(),
                                    int.Parse(column["MissingValues"].ToString()),
                                    column["Parameters"].ToString()
                                )
                            );
                        }
                    }
                }
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OutputFileTextBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var saveDialog = new SaveFileDialog();

            saveDialog.FileName = "dataset";
            saveDialog.DefaultExt = ".csv";
            saveDialog.Filter = "CSV|*.csv";

            bool? result = saveDialog.ShowDialog();

            if (result == true)
            {
                string filename = saveDialog.FileName;

                OutputFileTextBox.Text = filename;
            }
        }
    }
}
