
namespace BorderControl.IO
{
    using System;

    using Interfaces;

    class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
