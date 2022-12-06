
namespace BookingApp.Models.Rooms
{
    using BookingApp.Utilities.Messages;
    using Contracts;
    using System;

    public abstract class Room : IRoom
    {
        private double pricePerNight;

        public Room(int bedCapacity)
        {
            BedCapacity = bedCapacity;
            PricePerNight = 0;
        }
        public int BedCapacity { get; private set; }

        public double PricePerNight 
        {
            get => this.pricePerNight;
            private set 
            {
                if (value < 0)
                    throw new ArgumentException(ExceptionMessages.PricePerNightNegative);

                this.pricePerNight = value;
            }
        }

        public void SetPrice(double price)
        {
            PricePerNight = price;
        }
    }
}
