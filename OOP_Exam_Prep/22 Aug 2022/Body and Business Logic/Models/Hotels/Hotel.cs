
namespace BookingApp.Models.Hotels
{
    using Bookings.Contracts;
    using Rooms.Contracts;
    using Repositories.Contracts;
    using Contacts;
    using System;
    using BookingApp.Utilities.Messages;
    using BookingApp.Repositories;

    public class Hotel : IHotel
    {
        private string fullName;
        private int category;
        private RoomRepository rooms;
        private BookingRepository bookings;

        public Hotel(string fullName, int category)
        {
            FullName = fullName;
            Category = category;
            this.rooms = new RoomRepository();
            this.bookings = new BookingRepository();
        }
        public string FullName 
        {
            get => this.fullName;
            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExceptionMessages.HotelNameNullOrEmpty);

                this.fullName = value;
            }
        }

        public int Category 
        {
            get => this.category;
            private set 
            {
                if (value < 1 || value > 5)
                    throw new ArgumentException(ExceptionMessages.InvalidCategory);

                this.category = value;
            }
        }

        public double Turnover 
        {
            get 
            {
                double turnover = 0;
                foreach (var booking in this.bookings.All())
                {
                    turnover += Math.Round((booking.ResidenceDuration * booking.Room.PricePerNight), 2);
                }

                return turnover;
            }
        }

        public IRepository<IRoom> Rooms => this.rooms;

        public IRepository<IBooking> Bookings => this.bookings;
    }
}
