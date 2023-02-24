using System;

namespace Task14Simple;

public class Matrix
{
    private int[][] mArray;
    public int RowsCount { get; set; }
    public int ColsCount { get; set; }

    public Matrix(int[][] array, int rows, int cols)
    {
        mArray = new int[rows][];
        for (int i = 0; i < rows; i++)
        {
            mArray[i] = new int[cols];
            for (int j = 0; j < cols; j++)
            {
                mArray[i][j] = array[i][j];
            }
        }

        RowsCount = rows; 
        ColsCount = cols;
    }

    public int GetElement(int row, int column)
    {
        return mArray[row][column];
    }

    public static void sortMatrix(ref Matrix m)
    {
        int n = m.RowsCount;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (m.compare(j, j + 1))
                {
                    m.swap(j, j + 1);
                }
            }
        }
    }

    public bool compare(int i1, int i2)
    {
        if (i1 < 0 || i1 > RowsCount)
        {
            throw new ArgumentException("Недопустимы индекс i1");
        }
        if (i2 < 0 || i2 > RowsCount)
        {
            throw new ArgumentException("Недопустимы индекс i2");
        }

        int countI1 = 0, countI2 = 0;
        for (int i = 0; i < ColsCount; i++)
        {
            if (mArray[i1][i] % 2 != 0)
                countI1++;
            if (mArray[i2][i] % 2 != 0)
                countI2++;
        }
        return countI1 < countI2;
    }

    public void swap(int i1, int i2)
    {
        if (i1 < 0 || i1 > RowsCount)
        {
            throw new ArgumentException("Недопустимы индекс i1");
        }
        if (i2 < 0 || i2 > RowsCount)
        {
            throw new ArgumentException("Недопустимы индекс i2");
        }

        int[] tmp = new int[ColsCount];
        tmp = mArray[i1];
        mArray[i1] = mArray[i2];
        mArray[i2] = tmp;
    }
}