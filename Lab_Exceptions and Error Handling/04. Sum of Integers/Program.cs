using System;

namespace _04._Sum_of_Integers
{
    class Program
    {
        static void Main()
        {
            string[] input = Console.ReadLine()
                .Split(" ");

            int sum = 0;

            for (int i = 0; i < input.Length; i++)
            {
                string element = input[i];

                try
                {
                    int number = int.Parse(element);
                    sum += number;
                }
                catch (FormatException)
                {
                    Console.WriteLine($"The element '{element}' is in wrong format!");
                }
                catch (OverflowException)
                {
                    Console.WriteLine($"The element '{element}' is out of range!");
                }
                finally 
                {
                    Console.WriteLine($"Element '{element}' processed - current sum: {sum}");
                }
            }

            Console.WriteLine($"The total sum of all integers is: {sum}");
        }

    }
}
