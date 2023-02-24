using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Task14Simple;

public partial class MainWindow : Window
{
    private Matrix _matrix;
    public TextBox mCount;
    public TextBox nCount;
    public Grid matrixGrid;
    public Button createMatrixButton;
    public Button sortMatrixButton;
    public MainWindow()
    {
        InitializeComponent();
        _matrix = null;
        mCount = this.Find<TextBox>("mTextBox");
        nCount = this.Find<TextBox>("nTextBox");
        matrixGrid = this.Find<Grid>("matrix");
        createMatrixButton = this.Find<Button>("CreateMatrixButton");
        createMatrixButton.Click += CreateMatrixButtonOnClick;
        sortMatrixButton = this.Find<Button>("SortMatrixButton");
        sortMatrixButton.Click += SortMatrixButtonOnClick;
        
    }

    private void SortMatrixButtonOnClick(object? sender, RoutedEventArgs e)
    {
        int m = Int32.Parse(mCount.Text);
        int n = Int32.Parse(nCount.Text);
        int[][] arr = new int[m][];
        for (int i = 0; i < m; i++)
        {
            arr[i] = new int[n];
        }

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                arr[i][j] = Int32.Parse(matrixGrid.Children.Cast<TextBox>()
                    .First(e => Grid.GetRow(e) == i && Grid.GetColumn(e) == j).Text);
            } 
        }
        _matrix = new Matrix(arr, m, n);
        
        Matrix.sortMatrix(ref _matrix);
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matrixGrid.Children.Cast<TextBox>()
                    .First(e => Grid.GetRow(e) == i && Grid.GetColumn(e) == j).Text = _matrix.GetElement(i, j).ToString();
            } 
        }
    }

    public void CreateMatrixButtonOnClick(object? sender, RoutedEventArgs argsi)
    {
         int m = Int32.Parse(mCount.Text);
         int n = Int32.Parse(nCount.Text);
         matrixGrid.Children.Clear();
         matrix.RowDefinitions.Clear();
         matrix.ColumnDefinitions.Clear();
         for (int i = 0; i < m; i++)
         {
             matrix.RowDefinitions.Add(new RowDefinition());
         }
         for (int i = 0; i < n; i++)
         {
             matrix.ColumnDefinitions.Add(new ColumnDefinition());
         }
         for (int i = 0; i < m; i++)
         {
             for (int j = 0; j < n; j++)
             { 
                 TextBox box = new TextBox();
                 Grid.SetRow(box, i);
                 Grid.SetColumn(box, j);
                 box.Text = "0";
                 matrixGrid.Children.Add(box);
             } 
         }
    }
}