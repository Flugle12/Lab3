using System;


class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Создание первого двумерного массива
            TwoDimArr A = new TwoDimArr(new int[,] { { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 } });
            TwoDimArr B = new TwoDimArr(new int[,] { { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 } });
            TwoDimArr C = new TwoDimArr(new int[,] { { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 } });
            
            Console.WriteLine((A + 4*B) - ~C);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


    }
}

