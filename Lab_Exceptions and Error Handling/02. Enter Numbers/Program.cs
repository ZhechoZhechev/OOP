using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Enter_Numbers
{
    class Program
    {
        static void Main()
        {
            List<int> nums = new List<int>();

            while (nums.Count < 10)
            {
                try
                {
                    if (!nums.Any())
                        nums.Add(ReadNumber(1, 100));
                    else
                        nums.Add(ReadNumber(nums.Max(), 100));
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                }
                catch (IndexOutOfRangeException ie)
                {
                    Console.WriteLine(ie.Message);
                }
            }
            Console.WriteLine(string.Join(", ", nums));
        }
        static int ReadNumber(int start, int end)
        {
            string input = Console.ReadLine();
            int num;

            try
            {
                num = int.Parse(input);
            }
            catch (FormatException)
            {
                throw new FormatException($"Invalid Number!");
            }

            if (num <= start || num >= end)
            {
                throw new IndexOutOfRangeException($"Your number is not in range {start} - {end}!");
            }

            return num;
        }
    }
}
