using System;
using System.Collections.Generic;

namespace CustomStack
{
    public class StartUp
    {
        static void Main()
        {
            List<string> list = new List<string> { "1", "2", "3" };
            StackOfStrings stack = new StackOfStrings();
            stack.AddRange(list);
            Console.WriteLine($"{string.Join(",", stack)}");
        }
    }
}
