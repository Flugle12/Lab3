using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

[Serializable]
public struct Toy
{
    [XmlElement("Name")]
    public string Name;

    [XmlElement("Price")]
    public double Price;

    [XmlElement("MinAge")]
    public int MinAge;

    [XmlElement("MaxAge")]
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
    // 4
    public static void FillRandNum(string file, int countNum)
    {
        Random rand = new Random();
        using (FileStream fs = new FileStream(file, FileMode.Create))
        using (BinaryWriter bw = new BinaryWriter(fs))
        {
            for (int i = 0; i < countNum; i++)
            {
                bw.Write(rand.Next(1, 101));
            }
        }
    }

    public static void CopyNonModK(string anotherFile, string file, int k)
    {
        using (FileStream fs = new FileStream(file, FileMode.Open))
        using (BinaryReader reader = new BinaryReader(fs))
        using (FileStream outputFs = new FileStream(anotherFile, FileMode.Create))
        using (BinaryWriter writer = new BinaryWriter(outputFs))
        {
            while (fs.Position < fs.Length)
            {
                int num = reader.ReadInt32();
                if (num % k != 0)
                {
                    writer.Write(num);
                }
            }
        }
    }

    // 5
    public static void FillToy(string file)
    {
        var toys = new List<Toy>();

        while (true)
        {
            Console.WriteLine("Введите название игрушки (или 'exit' для завершения):");
            string name = Console.ReadLine();
            if (name.ToLower() == "exit")
            {
                break;
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

            // Создание и добавление игрушки в список
            toys.Add(new Toy(name, price, minAge, maxAge));
        }

        // Сериализация списка игрушек в XML
        XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>));
        using (FileStream fs = new FileStream(file, FileMode.Create))
        {
            serializer.Serialize(fs, toys);
        }
    }


    public static void FindToy(string file)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>)); // Создаем сериализатор для списка игрушек
        List<Toy> toys;
        // Открываем файл и десериализуем (превращаем из XML обратно в объекты) список игрушек
        using (FileStream fs = new FileStream(file, FileMode.Open))
        {
            toys = (List<Toy>)serializer.Deserialize(fs); // Десериализуем список игрушек из файла
        }

        Console.WriteLine("Подходящие игрушки для ребенка трех лет:");

        foreach (var toy in toys)
        {
            if (toy.Name.ToLower() != "мяч" && toy.MinAge <= 3 && toy.MaxAge >= 3)
            {
                Console.WriteLine($"Название: {toy.Name}, Цена: {toy.Price} рублей");
            }
        }
    }

    // 6
    public static void FillRandomIntegers(string file, int count)
    {
        Random random = new Random();
        using (StreamWriter writer = new StreamWriter(file))
        {
            for (int i = 0; i < count; i++)
            {
                writer.WriteLine(random.Next(1, 101));
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
                if (int.TryParse(line, out int number))
                {
                    if (number < min) min = number;
                    if (number > max) max = number;
                }
            }
        }
        Console.WriteLine($"\nmin: {min}\nmax: {max}");
        return min + max;
    }

    // 7
    public static void FillRandomInt2(string file, int count, int numbersPerLine)
    {
        Console.WriteLine("Файл: ");
        Random random = new Random();
        using (StreamWriter wr = new StreamWriter(file))
        {
            for (int i = 0; i < count; i++)
            {
                int r = random.Next(1,101);
                wr.Write(r);
                Console.Write($"{r} ");

                if ((i + 1) % numbersPerLine != 0 && i < count - 1)
                {
                    wr.Write(" ");
                }

                if ((i + 1) % numbersPerLine == 0 && i < count - 1)
                {
                    Console.WriteLine();
                    wr.WriteLine();
                }
            }
            Console.WriteLine();
        }
    }

    public static int SumOfMod2Num(string file)
    {
        int sum = 0;
        using (StreamReader reader = new StreamReader(file))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] numbers = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 1; i < numbers.Length; i += 2)
                {
                    if (int.TryParse(numbers[i], out int number))
                    {
                        sum += number; 
                    }
                }
            }
        }
        return sum;
    }


    // 8
    public static void FirstCharFile(string inputFile, string outputFile)
    {
        using (StreamReader reader = new StreamReader(inputFile))
        using (StreamWriter writer = new StreamWriter(outputFile))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Length > 0)
                {
                    writer.WriteLine(line[0]);
                }
            }
        }
    }
}