
namespace BorderControl
{
    using BorderControl.Core;
    using BorderControl.IO;
    using IO.Interfaces;
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
