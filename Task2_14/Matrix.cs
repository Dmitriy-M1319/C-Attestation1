using System;
using System.Collections.Generic;

namespace Task2_14;

public class Matrix
{
    public int[][] MatrixArray { get; }
    public int RowsCount { get; set; }
    public int ColsCount { get; set; }

    public Matrix(List<int>[] array, int rows, int cols)
    {
        MatrixArray = new int[rows][];
        for (int i = 0; i < rows; i++)
        {
            MatrixArray[i] = new int[cols];
            for (int j = 0; j < cols; j++)
            {
                MatrixArray[i][j] = array[i][j];
            }
        }

        RowsCount = rows; 
        ColsCount = cols;
    }

    public int GetElement(int row, int column)
    {
        return MatrixArray[row][column];
    }

    public List<int> GetRowsSum()
    {
        List<int> result = new List<int>();
        for (int i = 0; i < RowsCount; i++)
        {
            int sum = 0;
            for (int j = 0; j < ColsCount; j++)
            {
               sum += MatrixArray[i][j];
            }
            result.Add(sum);
        }

        return result;
    }
}