using System;

namespace CustomRandomList
{
    public class StartUp
    {
        static void Main()
        {
            RandomList randList = new RandomList();
            randList.Add("1");
            randList.Add("2");
            randList.Add("3");

            Console.WriteLine($"{randList.RandomString()}"); 
        }
    }
}
