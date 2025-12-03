using System;
using System.IO;
using System.Text;

namespace OOP_6Lab_Exceptions_1block
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            try
            {
                File.WriteAllText("no_file.txt", string.Empty);
                File.WriteAllText("bad_data.txt", string.Empty);
                File.WriteAllText("overflow.txt", string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не вдалося створити/оновити один з файлів результатів: {ex.Message}");
                return;
            }

            int totalSum = 0;
            int successCount = 0;

            for (int i = 10; i <= 29; i++)
            {
                string fileName = $"{i}.txt";

                //File.WriteAllText(fileName, string.Empty);
                //Random rand = new Random();
                //StringBuilder sb = new StringBuilder();
                //sb.AppendLine(rand.Next(-55555, 55555).ToString());
                //sb.AppendLine(rand.Next(-55555, 55555).ToString());
                //File.WriteAllText(fileName, sb.ToString());

                try
                {
                    string[] lines = File.ReadAllLines(fileName);
                    string s1 = lines[0].Trim();
                    string s2 = lines[1].Trim();

                    int a;
                    int b;

                    try
                    {
                        a = int.Parse(s1);
                        b = int.Parse(s2);
                    }
                    catch (FormatException)
                    {
                        File.AppendAllText("bad_data.txt", fileName + Environment.NewLine);
                        continue;
                    }
                    catch (OverflowException)
                    {
                        File.AppendAllText("bad_data.txt", fileName + Environment.NewLine);
                        continue;
                    }

                    int product = checked(a * b);
                    Console.WriteLine($"Файл \"{fileName}\": {a} * {b} = {product}");
                    totalSum += product;
                    successCount++;
                }
                catch (OverflowException)
                {
                    File.AppendAllText("overflow.txt", fileName + Environment.NewLine);
                }
                catch (FileNotFoundException)
                {
                    File.AppendAllText("no_file.txt", fileName + Environment.NewLine);
                }
                catch (IndexOutOfRangeException)
                {
                    File.AppendAllText("bad_data.txt", fileName + Environment.NewLine);
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Помилка вводу/виводу при обробці файлу \"{fileName}\": {ex.Message}. Пропускаю.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Несподівана помилка при обробці файлу \"{fileName}\": {ex.Message}. Пропускаю.");
                }
            }

            try
            {
                double average = (double)totalSum / successCount;
                Console.WriteLine($"Середнє арифметичне добутків: {average}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Не вдалося обчислити жодного добутку (немає коректно оброблених файлів).");
            }
        }
    }
}