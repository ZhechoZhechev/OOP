using System;

namespace Stealer
{
    class Program
    {
        static void Main()
        {
            Spy spy = new Spy();
            string result = spy.GettersAndSetters("Stealer.Hacker");

            Console.WriteLine(result);
        }
    }
}
