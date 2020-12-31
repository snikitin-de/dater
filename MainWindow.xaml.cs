﻿using dater;
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

            config.SaveConfig("dataset.json", metadata, dataGrid.DataContext);
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            var config = new Config();

            var json = config.LoadConfig("dataset.json");

            if (json["RowCount"] != null)
            {
                RowCountTextBox.Text = json["RowCount"].ToString();
            }

            if (json["Separator"] != null)
            {
                SeparatorTextBox.Text = json["Separator"].ToString();
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

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
