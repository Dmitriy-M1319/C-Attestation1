using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;

namespace Task2_14;

public partial class MainWindow : Window
{
    private Matrix _table;
    private string _filename;
    public MainWindow()
    {
        InitializeComponent();
    }

    public async void inputFileButton_Click(object? sender, RoutedEventArgs e)
    {
        var dlg = new OpenFileDialog();
        dlg.Filters.Add(new FileDialogFilter() { Name = "Text Files", Extensions = { "txt" } });
        dlg.Filters.Add(new FileDialogFilter() { Name = "All Files", Extensions = { "*" } });
        var result = await dlg.ShowAsync(this);
        if (result != null)
        {
            string[] filename = result;
            List<List<int>> inputTable = new List<List<int>>();
            _filename = filename[0];
            using (var inputFile = new StreamReader(filename[0]))
            {
                string? line;
                while ((line = inputFile.ReadLine()) != null)
                {
                    if (line[0] == '|')
                    {
                        string[] values = line.Split(new char[] { ' ', '|' }, StringSplitOptions.RemoveEmptyEntries);
                        List<int> row = new List<int>();
                        foreach (var value in values)
                        {
                            row.Add(Int32.Parse(value.Trim())); 
                        }
                        inputTable.Add(row);
                    }
                }
            }

            _table = new Matrix(inputTable.ToArray().ToArray(), inputTable.Count, inputTable[0].Count);
            InsertMatrixInGrid(_table);
        }
    }

    public void InsertMatrixInGrid(Matrix matrix)
    {
        for (int i = 0; i < matrix.RowsCount; i++)
        {
           tableGrid.RowDefinitions.Add(new RowDefinition()); 
        }

        for (int i = 0; i < matrix.ColsCount; i++)
        {
            tableGrid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        for (int i = 0; i < matrix.RowsCount; i++)
        {
            for (int j = 0; j < matrix.ColsCount; j++)
            {
                TextBlock block = new TextBlock();
                block.Text = matrix.GetElement(i, j).ToString();
                block.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetRow(block, i);
                Grid.SetColumn(block, j);
                tableGrid.Children.Add(block);
            } 
        }

        tableGrid.ShowGridLines = true;
        tableName.Text = "Входные данные";
    }

    public void CountSumsButton_Click(object? sender, RoutedEventArgs e)
    {
        List<int> sums = _table.GetRowsSum();
        sumsBlock.Text = "Суммы чисел в строках таблицы";
        string outputName = _filename.Split('.')[0] + "_output.txt";
        using (var writer = new StreamWriter(outputName))
        {
            for (int i = 0; i < sums.Count; i++)
            {
                writer.WriteLine(sums[i].ToString());
                TextBlock block = new TextBlock();
                block.Text = sums[i].ToString();
                sumsList.Children.Add(block);
            }
        }

        string[] tokens = outputName.Split('/');
        outputFilename.Text = "Данные сохранены в файл " + tokens[^1];
        
    }
}