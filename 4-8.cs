using System;
using System.IO;
using System.Net.Mail;
using System.Runtime.InteropServices;

/*
 Задания 4 – 8 выполнить в виде статических методов одного класса, но отдельно от заданий 1-3.
    В задании 4 бинарные файлы, содержат числовые данные, исходный файл необходимо заполнить 
случайными данными, заполнение организовать отдельным методом.

        /4 Получить в другом файле последовательного доступа все компоненты исходного файла, кроме 
тех, которые кратны k.


    В задании 5 бинарные файлы содержат величины типа struct, заполнение исходного файла
необходимо организовать отдельным методом.

        /5 Файл содержит сведения об игрушках: название игрушки, ее стоимость в рублях и возрастные 
границы (например, игрушка может предназначаться для детей от двух до пяти лет). Получить 
сведения о том, можно ли подобрать игрушку, любую, кроме мяча, подходящую ребенку трех лет.


В задании 6 в текстовом файле хранятся целые числа по одному в строке, исходный файл 
необходимо заполнить случайными данными, заполнение организовать отдельным методом.

        /6 В файле найти сумму максимального и минимального элементов.

В задании 7 в текстовом файле хранятся целые числа по несколько в строке, исходный файл 
необходимо заполнить случайными данными, заполнение организовать отдельным методом.

 /7 Вычислить сумму чётных элементов.

В задании 8 в текстовом файле хранится текст.
        /8 Создать новый текстовый файл, каждая строка которого содержит первый символ
соответствующей строки исходного файла.
*/


[Serializable]
public struct Toy
{
    public string Name;
    public double Price;
    public int MinAge;
    public int MaxAge;

    public Toy(string name, double price, int minAge, int maxAge)
    {
        Name = name;
        Price = price;
        MinAge = minAge;
        MaxAge = maxAge;


    }
}

class BinaryFile    
{
    //4
    public static void FillRandNum(string file, int countNum)
    {
        Random rand = new Random();
        using (FileStream fs = new FileStream(file, FileMode.Create))
        using (BinaryWriter bw = new BinaryWriter(fs))
        {
            for(int i = 0; i < countNum; i++)
            {
                bw.Write(rand.Next(1, 101));
            }
        }
    }

    public static void CopyNonModK(string anotherFile, string file, int k)
    {
        using (FileStream fs = new FileStream(anotherFile, FileMode.Open))
        using (BinaryReader reader = new BinaryReader(fs))
        using (FileStream outputFs = new FileStream(file, FileMode.Create))
        using (BinaryWriter writer = new BinaryWriter(outputFs))
        {
            while (fs.Position < fs.Length)
            {
                int num = reader.ReadInt32();
                if(num % k != 0)
                {
                    writer.Write(num);
                }
            }
        }
    }

    //5
    public static void FillToy(string file)
    {
        using (FileStream fs = new FileStream(file, FileMode.Create))
        using (BinaryWriter wr = new BinaryWriter(fs))
        {
            while (true)
            {
                Console.WriteLine("Введите название игрушки (или 'exit' для завершения):");
                string name = Console.ReadLine();
                if (name.ToLower() == "exit")
                {
                    break; // Завершение ввода
                }

                Console.WriteLine("Введите стоимость игрушки в рублях:");
                double price;
                while (!double.TryParse(Console.ReadLine(), out price))
                {
                    Console.WriteLine("Некорректная стоимость. Пожалуйста, введите число.");
                }

                Console.WriteLine("Введите минимальный возраст (в годах):");
                int minAge;
                while (!int.TryParse(Console.ReadLine(), out minAge))
                {
                    Console.WriteLine("Некорректный возраст. Пожалуйста, введите целое число.");
                }

                Console.WriteLine("Введите максимальный возраст (в годах):");
                int maxAge;
                while (!int.TryParse(Console.ReadLine(), out maxAge))
                {
                    Console.WriteLine("Некорректный возраст. Пожалуйста, введите целое число.");
                }

                // Запись данных об игрушке в файл
                wr.Write(name);
                wr.Write(price);
                wr.Write(minAge);
                wr.Write(maxAge);
            }
        }
    }

    public static void FindToy(string file)
    {
        bool found = false;

        using (FileStream fs = new FileStream(file, FileMode.Open))
        using (BinaryReader reader = new BinaryReader(fs))
        {
            while (fs.Position < fs.Length)
            {
                string name = reader.ReadString();
                double price = reader.ReadDouble();
                int minAge = reader.ReadInt32();
                int maxAge = reader.ReadInt32();

                if (minAge <= 3 && maxAge >= 3 && name != "Мяч")
                {
                    Console.WriteLine($"Вам подходит игрушка: {name}, Цена {price}");
                    found = true;
                }
            }
        }

        if (!found)
        {
            Console.WriteLine("не найдено");
        }
    }
    //6
    public static void FillRandomIntegers(string file, int count)
    {
        Random random = new Random();
        using (StreamWriter writer = new StreamWriter(file))
        {
            for (int i = 0; i < count; i++)
            {
                writer.Write(random.Next(1,101));
            }
        }
    }

    public static int SumMinMax(string file)
    {
        int min = int.MaxValue;
        int max = int.MinValue;

        using (StreamReader reader = new StreamReader(file))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if(int.TryParse(line, out int number))
                {
                    if(number < min) min = number;
                    if(number > max) max = number;
                }
            }
        }
        return min+max;
    }
    //7
    public static void FillRandomInt2(string file, int count)
    {
        Random random = new Random();
        using (StreamWriter wr = new StreamWriter(file))
        {
            for(int i = 0; i <= count; i++)
            {
                wr.Write(random.Next(1, 101));
                if( i < count - 1)
                {
                    wr.Write(' ');
                }
            }
        }
    }

    public static int SumOfMod2Num(string file)
    {
        int sum = 0;

        using (StreamReader read = new StreamReader(file))
        {
            string line;
            while((line = read.ReadLine()) != null)
            {
                string[] num = line.Split(' ');
                foreach(var numberStr  in num)
                {
                    if( int.TryParse(numberStr, out int number) && number % 2 == 0)
                    {
                        sum += number;
                    }
                }
            }
            return sum;
        }
    }

    //8
    public static void FirstCharFile(string file, string outputFile)
    {
        using (StreamReader reader = new StreamReader(file))
        using (StreamWriter writer = new StreamWriter(outputFile))
        {
            string line;
            while((line = reader.ReadLine()) != null)
            {
                if(line.Length > 0)
                {
                    writer.WriteLine(line[0]);
                }
            }
        }
    }
}
