
namespace Vehicles
{
    using System;

    using Models;
    public class StartUp
    {
        static void Main()
        {
            string[] carInfo = Console.ReadLine().Split(" ");
            string[] truckInfo = Console.ReadLine().Split(" ");
            string[] busInfo = Console.ReadLine().Split(" ");

            int numOfInputs = int.Parse(Console.ReadLine());

            Car car = new Car(double.Parse(carInfo[1]), double.Parse(carInfo[2]), double.Parse(carInfo[3]));
            Truck truck = new Truck(double.Parse(truckInfo[1]), double.Parse(truckInfo[2]), double.Parse(truckInfo[3]));
            Bus bus = new Bus(double.Parse(busInfo[1]), double.Parse(busInfo[2]), double.Parse(busInfo[3]));

            for (int i = 0; i < numOfInputs; i++)
            {
                string[] info = Console.ReadLine().Split(" ");
                string command = info[0];
                string type = info[1];
                double value = double.Parse(info[2]);

                if (type == "Car")
                {
                    if (command == "Drive") car.Drive(value);

                    else car.Refuel(value);
                }
                else if (type == "Truck") 
                {
                    if (command == "Drive") truck.Drive(value);

                    else truck.Refuel(value);
                }
                else if (type == "Bus")
                {
                    if (command == "Drive") bus.Drive(value);
                    else if (command == "DriveEmpty") bus.DriveEmpty(value);
                    else bus.Refuel(value);
                }
            }
            Console.WriteLine(car);
            Console.WriteLine(truck);
            Console.WriteLine(bus);
        }
    }
}
