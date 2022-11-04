
namespace Telephony.Core
{
    using Interfaces;
    using IO.Interfaces;
    using System;
    using Telephony.Modles;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly Stationaryphone stationaryphone;
        private readonly Smartphone smartphone;

        private Engine()
        {
            this.stationaryphone = new Stationaryphone();
            this.smartphone = new Smartphone();
        }
        public Engine(IReader reader, IWriter writer)
            : this()
        {
            this.reader = reader;
            this.writer = writer;
        }
        public void Run()
        {
            string[] phoneNumebers = this.reader.ReadLine().Split(" ");
            string[] urls = this.reader.ReadLine().Split(" ");

            foreach (var number in phoneNumebers)
            {
                try
                {
                    if (number.Length == 7)
                    {
                        this.writer.WriteLine(this.stationaryphone.Call(number));
                    }
                    else if (number.Length == 10)
                    {
                        this.writer.WriteLine(this.smartphone.Call(number));
                    }
                }
                catch (ArgumentException ae) 
                {
                    this.writer.WriteLine(ae.Message);
                }
                catch (Exception)
                {

                    throw;
                }
            }

            foreach (var url in urls)
            {
                try
                {
                    this.writer.WriteLine(this.smartphone.Browse(url));
                }
                catch (ArgumentException ae)
                {
                    this.writer.WriteLine(ae.Message);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
