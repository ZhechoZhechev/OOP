
namespace BookingApp.Models.Bookings
{
    using System;
    using System.Text;

    using Contracts;
    using Rooms.Contracts;
    using Utilities.Messages;

    public class Booking : IBooking
    {
        private int residenceDuration;
        private int adultsCount;
        private int childrenCount;

        public Booking(IRoom room, int residenceDuration, int adultsCount, int childrenCount, int bookingNumber)
        {
            Room = room;
            ResidenceDuration = residenceDuration;
            AdultsCount = adultsCount;
            ChildrenCount = childrenCount;
            BookingNumber = bookingNumber;
        }
        public IRoom Room { get; private set; }

        public int ResidenceDuration 
        {
            get => this.residenceDuration;
            private set 
            {
                if (value <= 0)
                    throw new ArgumentException(ExceptionMessages.DurationZeroOrLess);

                this.residenceDuration = value;
            }
        }

        public int AdultsCount 
        {
            get => this.adultsCount;
            private set
            {
                if (value < 1)
                    throw new ArgumentException(ExceptionMessages.AdultsZeroOrLess);

                this.adultsCount = value;
            }
        }

        public int ChildrenCount 
        {
            get => this.childrenCount;
            private set
            {
                if (value < 0)
                    throw new ArgumentException(ExceptionMessages.ChildrenNegative);

                this.childrenCount = value;
            }
        }

        public int BookingNumber { get; private set; }

        public string BookingSummary()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Booking number: {BookingNumber}")
                .AppendLine($"Room type: {Room.GetType().Name}")
                .AppendLine($"Adults: {AdultsCount} Children: {ChildrenCount}")
                .AppendLine($"Total amount paid: {TotalPaid():F2} $");

            return sb.ToString().TrimEnd();
        }
        public double TotalPaid() => Math.Round(ResidenceDuration * Room.PricePerNight, 2);
    }
}
