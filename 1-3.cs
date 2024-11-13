using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*

Задания 1, 2 и 3 выполнить в виде методов одного класса. 
Задание 1 реализовать в виде конструкторов (кроме них, могут быть и другие конструкторы). Класс 
содержит единственное поле – двумерный массив.

Первый массив, размерностью n х m , заполняется данными, вводимыми с клавиатуры, так что 
заполнение ведется по строкам от первых элементов строки к последним. 

Второй массив, размерностью n х n, заполняется случайными числами так, что четные числа 
заносятся в элементы массива, которые на шахматной доске были бы черными, а нечетные числа 
заносятся в элементы, которые на шахматной доске были бы белыми.

Третий массив, размерностью n х n, заполняется для произвольного n так же, как для n=5:
*/

public class TwoDimArr
{
    private int[,] _mtrx;

   
    public TwoDimArr()
    {
        _mtrx = new int[0, 0];
    }

    public TwoDimArr(int n, int m, int qustions)
    {
        if(qustions == 1) 
        {
            _mtrx = new int[n, m];
            FillArray();
        }
        else if(qustions == 2)
        {
            _mtrx = new int[n, n];
            FillRandArray();
        }
        else if (qustions == 3)
        {
            _mtrx = new int[n, n];
            FillLowTriArray();
        }
        else
        {
            throw new ArgumentException($"номера {qustions} нет в данном классе");
        }
        
    }

    public TwoDimArr(int[,] mtrx)
    {
        _mtrx = mtrx;
    }

    public void FillArray()
    {
        Console.WriteLine($"Введите {_mtrx.GetLength(0)} строк массива: ");
       

        for (int i = 0; i < _mtrx.GetLength(0); i++) {
         
            bool Valid = false;
            while (!Valid)
            {
                Console.Write($"строка {i+1} :");

                string input = Console.ReadLine();
                string[] inputParts = input.Split(' ');

                if (inputParts.Length != _mtrx.GetLength(1))
                {
                    throw new ArgumentException("nums is smollest/largest than expected");
                }

                Valid = true;

                for (int j = 0; j < _mtrx.GetLength(1); j++)
                {
                    if (!int.TryParse(inputParts[j], out _mtrx[i, j]))
                    {
                        Valid = false;
                        throw new ArgumentException($"{inputParts[j]} is NaN");
                    }
                }
            }

        }
    }

    public void FillRandArray()
    {
        Random rand = new Random();
        for (int i = 0; i < _mtrx.GetLength(0); i++)
        {
            for (int j = 0; j < _mtrx.GetLength(1); j++)
            {
                
                int value = rand.Next(1, 50) * 2;
                if (((i + j ) % 2 != 0) && ( value % 2 == 0))
                {
                    _mtrx[i, j] = value;
                }
                else
                {
                    _mtrx[i, j] = rand.Next(1,50) * 2 +1;
                }
            }
        }
    }

    public void FillLowTriArray()
    {
        int mtrxLenght = _mtrx.GetLength(0);
        int k = 1, j = 0, i = mtrxLenght - 1;
        for(int counter = 0; counter < mtrxLenght; counter++)
        {
            int counter2 = 0;
            i = mtrxLenght-counter -1 ; j = 0;
            while(counter2 <= counter)
            {
                _mtrx[i++, j++] = k;
                counter2++;
                k++;
            }
        }
    }

    //ВТОРОЕ ЗАДАНИЕ
    /*
     Задан двумерный массив. Найдите сумму элементов первого столбца без одного последнего 
    элемента, сумму элементов второго столбца без двух последних, сумму третьего столбца без трех 
    последних и т.д. Последний столбец не обрабатывается. Среди найденных сумм найдите 
    максимальную.
     */

    public void MaxSumsOfRows()
    {
        int maxSums = -1;
        int sums = 0;
        for (int i = 0; i < _mtrx.GetLength(0)-1; i++)
        {
            for (int j = 0; j < _mtrx.GetLength(1)-1-i; j++)
            {
                sums += _mtrx[j, i];
                
            }
            Console.WriteLine($"сцмма {i+1} = {sums}");
            if(sums > maxSums) 
                maxSums = sums;
            sums = 0;
        }
        Console.WriteLine($"максимальная сумма: {maxSums}");
    }

    public static TwoDimArr operator *(int num, TwoDimArr arr)
    {
        int[,] resultArray = new int[arr._mtrx.GetLength(0), arr._mtrx.GetLength(1)];

        for (int i = 0; i < arr._mtrx.GetLength(0); i++)
        {
            for (int j = 0; j < arr._mtrx.GetLength(1); j++)
            {
                resultArray[i, j] = arr._mtrx[i, j] * num;
            }
        }
        return new TwoDimArr(resultArray);
    }

    public static TwoDimArr operator +(TwoDimArr arr1, TwoDimArr arr2)
    {
        // Проверяем, совпадают ли размеры матриц
        if (arr1._mtrx.GetLength(0) != arr2._mtrx.GetLength(0) ||
            arr1._mtrx.GetLength(1) != arr2._mtrx.GetLength(1))
        {
            throw new ArgumentException("Can't add Matrixs");
        }

        int[,] resultArray = new int[arr1._mtrx.GetLength(0), arr1._mtrx.GetLength(1)];

        for (int i = 0; i < arr1._mtrx.GetLength(0); i++)
        {
            for (int j = 0; j < arr1._mtrx.GetLength(1); j++)
            {
                resultArray[i, j] = arr1._mtrx[i, j] + arr2._mtrx[i, j];
            }
        }
        return new TwoDimArr(resultArray);
    }

    public static TwoDimArr operator -(TwoDimArr arr1, TwoDimArr arr2)
    {
        // Проверяем, совпадают ли размеры матриц
        if (arr1._mtrx.GetLength(0) != arr2._mtrx.GetLength(0) ||
            arr1._mtrx.GetLength(1) != arr2._mtrx.GetLength(1))
        {
            throw new ArgumentException("Can't add Matrixs");
        }

        int[,] resultArray = new int[arr1._mtrx.GetLength(0), arr1._mtrx.GetLength(1)];

        for (int i = 0; i < arr1._mtrx.GetLength(0); i++)
        {
            for (int j = 0; j < arr1._mtrx.GetLength(1); j++)
            {
                resultArray[i, j] = arr1._mtrx[i, j] - arr2._mtrx[i, j];
            }
        }
        return new TwoDimArr(resultArray);
    }

    public static TwoDimArr operator ~(TwoDimArr arr)
    {
        int rows = arr._mtrx.GetLength(0);
        int cols = arr._mtrx.GetLength(1);

        int[,] transposedArray = new int[cols, rows];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                transposedArray[j, i] = arr._mtrx[i, j];
            }
        }

        return new TwoDimArr(transposedArray);
    }


    public override string ToString()
    {
        string result = "";
        for (int i = 0; i < _mtrx.GetLength(0); i++)
        {
            for (int j = 0; j < _mtrx.GetLength(1); j++)
            {
                result += _mtrx[i, j] + "\t";
            }
            result += '\n';
        }
        return result;
    }
}