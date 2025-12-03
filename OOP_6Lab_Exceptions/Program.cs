using System;
using System.IO;
using System.Text;

namespace OOP_6Lab_Exceptions_1block
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int totalSum = 0;
            bool anySuccess = false;

            for (int i = 10; i <= 29; i++)
            {
                string fileName = $"{i}.txt";
                Random rand = new Random();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(rand.Next(int.MinValue, int.MaxValue).ToString());
                sb.AppendLine(rand.Next(int.MinValue, int.MaxValue).ToString());
                File.WriteAllText(fileName, sb.ToString());

                try
                {
                    string[] lines = File.ReadAllLines(fileName);
                    string s1 = lines[0].Trim();
                    string s2 = lines[1].Trim();

                    int a = int.Parse(s1);
                    int b = int.Parse(s2);

                    int product = a * b;
                    Console.WriteLine($"Файл \"{fileName}\": {a} * {b} = {product}");
                    totalSum += product;
                    anySuccess = true;
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Помилка вводу/виводу при обробці файлу \"{fileName}\": {ex.Message}. Пропускаю.");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine($"Файл \"{fileName}\" містить менше 2 рядків. Очікуються два цілі числа. Пропускаю.");
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Не вдалося розпарсити одне з чисел у файлі \"{fileName}\". Очікуються два цілих числа. Пропускаю.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine($"Одне з чисел у файлі \"{fileName}\" виходить за межі типу int. Пропускаю.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Несподівана помилка при обробці файлу \"{fileName}\": {ex.Message}. Пропускаю.");
                }
            }

            Console.WriteLine(anySuccess ? $"Сума всіх добутків: {totalSum}" : "Не вдалося обчислити жодного добутку (немає коректно оброблених файлів).");
        }
    }
}