using System;
using System.IO;
using System.Linq.Expressions;


class Program
{
    static void Main(string[] args)
    {
        try
        {

            Console.WriteLine("Выберите задание (1, 2 или 3): ");
            int question = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите размерность n (для задания 1 и 2) или n и m (для задания 1): ");
            int n = 0, m = 0;

            {
                Console.Write("Введите количество столбцов (m): ");
                m = int.Parse(Console.ReadLine());
            }

            n = int.Parse(Console.ReadLine());

            TwoDimArr array = new TwoDimArr(n, m, question);
            Console.WriteLine("Заполненная матрица:");
            Console.WriteLine(array.ToString());

            if (question == 2)
            {
                Console.WriteLine("Суммы элементов столбцов:");
                array.MaxSumsOfRows();
            }

            if (question == 3)
            {
                // Создание первого двумерного массива
                TwoDimArr A = new TwoDimArr(new int[,] { { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 } });
                TwoDimArr B = new TwoDimArr(new int[,] { { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 } });
                TwoDimArr C = new TwoDimArr(new int[,] { { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 } });

                Console.WriteLine((A + 4 * B) - ~C);
            }

            // Задание 4
            Console.WriteLine("4: ");
            int k;
            Console.WriteLine("введите чисо кратные которому не будут записаны во второй файл: ");
            if (!int.TryParse(Console.ReadLine(), out k))
            {
                throw new ArgumentException("NaN");
            }
            string numFile = "numbers.bin";
            string filteredFile = "filtered.bin";
            BinaryFile.FillRandNum(numFile, 10);
            BinaryFile.CopyNonModK(filteredFile, numFile, k);
            Console.WriteLine("Первый файл:");
            FilePrint(numFile);
            Console.WriteLine($"Второй файл без чисел кратных {k}: ");
            FilePrint(filteredFile);

            //Задание 5
            Console.WriteLine("5: ");
            string toyFile = "toys.bin";
            BinaryFile.FillToy(toyFile);
            Console.WriteLine("Поиск игрушки для ребенка трех лет:");
            BinaryFile.FindToy(toyFile);

            //// Задание 6
            Console.WriteLine("6: ");
            string intFile = "integers.txt";
            BinaryFile.FillRandomIntegers(intFile, 10);
            FilePrintTXT(intFile);
            int sumMinMax = BinaryFile.SumMinMax(intFile);
            Console.WriteLine("Сумма минимального и максимального элементов: " + sumMinMax);

            // Задание 7
            Console.WriteLine("7: ");
            string intFile2 = "integers2.txt";
            int numOfNums, numOfNumsInLine;
            Console.WriteLine("Введите 2 чичла - общее количество чисел и количество чисел в строке:");
            string[] line = Console.ReadLine().Split(' ');
            if (!int.TryParse(line[0], out numOfNums) || !int.TryParse(line[1], out numOfNumsInLine))
            {
                throw new ArgumentException("NaN");
            }
            BinaryFile.FillRandomInt2(intFile2, numOfNums, numOfNumsInLine);
            int evenSum = BinaryFile.SumOfMod2Num(intFile2);
            Console.WriteLine("Сумма четных элементов: " + evenSum);

            //// Задание 8
            Console.WriteLine("8: ");
            string textFile = "text.txt";
            string firstCharFile = "firstChars.txt";
            BinaryFile.FirstCharFile(textFile, firstCharFile);
            Console.WriteLine("Файл с первыми символами строк создан: " + firstCharFile);
            FilePrintTXT(textFile);
            Console.WriteLine();
            Console.Write("первый символ: ");
            FilePrintTXT(firstCharFile);
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


    }

    static void FilePrintTXT(string nameFile)
    {
        if (!File.Exists(nameFile))
        {
            Console.WriteLine("Файл не найден: " + nameFile);
            return; // Выход из метода, если файл не найден
        }

        using (StreamReader read = new StreamReader(File.Open(nameFile, FileMode.Open)))
        {
            try
            {
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    string[] numbers = line.Split(' ');
                    foreach (string number in numbers)
                    {
                        if (int.TryParse(number, out int num))
                        {
                            Console.Write($"{num} ");
                        }
                        else
                        {
                            Console.Write($"{number} ");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при чтении файла: " + ex.Message);
            }
        }
    }

    static void FilePrint(string fileName)
    {
        // Проверяем, существует ли файл
        if (!File.Exists(fileName))
        {
            Console.WriteLine("Файл не найден: " + fileName);
            return;
        }

        using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
        {
            try
            {
                // Читаем и выводим числа до конца файла
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    int number = reader.ReadInt32(); // Читаем целое число
                    Console.Write($"{number} "); // Выводим число
                }
                Console.WriteLine();
            }
            catch (EndOfStreamException)
            {
                // Игнорируем исключение, если достигнут конец файла
            }
        }
    }

}

