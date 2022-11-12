using System;

namespace _01._Square_Root
{
    class Program
    {
        static void Main()
        {
            try
            {
                double number = int.Parse(Console.ReadLine());
                if (number < 0)
                {
                    throw new ArgumentException("Invalid number.");
                }
                double result = Math.Sqrt(number);
                Console.WriteLine(result);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
            finally
            {
                Console.WriteLine("Goodbye.");
            }


        }
    }
}
