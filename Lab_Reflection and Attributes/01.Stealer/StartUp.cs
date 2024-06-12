using System;

namespace Stealer
{
    public class StartUp
    {
        static void Main()
        {
            string[] p =
            [
                "username",
                "username"
            ];
            Spy spy = new Spy();
            string result = spy.StealFieldInfo("Stealer.Hacker", p);

            Console.WriteLine(result);
        }
    }
}
