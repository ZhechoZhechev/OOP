
namespace Telephony
{
    using Telephony.Core;
    using Telephony.IO;
    using Telephony.IO.Interfaces;

    public class StartUp
    {
        static void Main()
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            Engine engine = new Engine(reader, writer);
            engine.Run();
        }
    }
}
