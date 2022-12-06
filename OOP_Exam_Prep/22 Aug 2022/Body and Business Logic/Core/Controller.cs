
namespace BookingApp.Core
{
    using System;
    using System.Linq;
    using System.Text;
    using BookingApp.Models.Bookings;
    using BookingApp.Models.Hotels;
    using BookingApp.Models.Rooms;
    using BookingApp.Models.Rooms.Contracts;
    using BookingApp.Utilities.Messages;
    using Contracts;
    using Repositories;
    public class Controller : IController
    {
        private HotelRepository hotels;

        public Controller()
        {
            this.hotels = new HotelRepository();
        }
        public string AddHotel(string hotelName, int category)
        {
            var hotelToCheck = this.hotels.All().FirstOrDefault(x => x.FullName == hotelName);

            if (hotelToCheck != null)
                return string.Format(OutputMessages.HotelAlreadyRegistered, hotelName);

            var hotelToAdd = new Hotel(hotelName, category);
            hotels.AddNew(hotelToAdd);

            return string.Format(OutputMessages.HotelSuccessfullyRegistered, category, hotelName);
        }

        public string BookAvailableRoom(int adults, int children, int duration, int category)
        {
            if (!hotels.All().Any(x => x.Category == category))
                return string.Format(OutputMessages.CategoryInvalid, category);

            var hotelsOrdered = hotels.All().Where(x => x.Category == category).OrderBy(x => x.FullName);

            foreach (var hotel in hotelsOrdered)
            {
                var selectedRoom = hotel.Rooms.All()
                    .Where(x => x.PricePerNight > 0)
                    .Where(y => y.BedCapacity >= adults + children)
                    .OrderBy(z => z.BedCapacity).FirstOrDefault();


                if (selectedRoom != null)
                {
                    var hotelToBook = hotels.All().FirstOrDefault(x => x.Rooms.All().Contains(selectedRoom));

                    int bookingNumber = hotelToBook.Bookings.All().Count() + 1;

                    Booking newBooking = new Booking(selectedRoom, duration, adults, children, bookingNumber);

                    hotelToBook.Bookings.AddNew(newBooking);

                    return string.Format(OutputMessages.BookingSuccessful, bookingNumber, hotel.FullName);
                }
            }

            return string.Format(OutputMessages.RoomNotAppropriate);
        }

        public string HotelReport(string hotelName)
        {
            var hotelToReport =  hotels.All().FirstOrDefault(x => x.FullName == hotelName);
            if (hotelToReport == null)
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Hotel name: {hotelToReport.FullName}");
            sb.AppendLine($"--{hotelToReport.Category} star hotel");
            sb.AppendLine($"--Turnover: {hotelToReport.Turnover:F2} $");
            sb.AppendLine($"--Bookings:");
            sb.AppendLine();

            if (hotelToReport.Bookings.All().Count == 0)
            {
                sb.Append("none");
                return sb.ToString();
            }
            foreach (var booking in hotelToReport.Bookings.All())
            {
                sb.AppendLine(booking.BookingSummary())
                    .AppendLine();
            }
            return sb.ToString().TrimEnd();

        }

        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            var hotelNameToSetPrices = hotels.All().FirstOrDefault(x => x.FullName == hotelName);
            if (hotelNameToSetPrices == null)
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);

            if (!new string[] { "Apartment", "DoubleBed", "Studio" }.Contains(roomTypeName))
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);

            var rooms = hotelNameToSetPrices.Rooms.All();
            if (!rooms.Any(x => x.GetType().Name == roomTypeName))
                return string.Format(OutputMessages.RoomTypeNotCreated);

            var roomToSetPriceTo = rooms.FirstOrDefault(x => x.GetType().Name == roomTypeName);
            if (roomToSetPriceTo.PricePerNight != 0)
                throw new InvalidOperationException(ExceptionMessages.CannotResetInitialPrice);

            roomToSetPriceTo.SetPrice(price);
            return string.Format(OutputMessages.PriceSetSuccessfully, roomTypeName, hotelName);
        }

        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            var hotelToAddNewRoomType = hotels.All().FirstOrDefault(x => x.FullName == hotelName);
            if (hotelToAddNewRoomType == null)
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);

            var hotelRooms = hotelToAddNewRoomType.Rooms.All();
            if (hotelRooms.Any(x => x.GetType().Name == roomTypeName))
                return string.Format(OutputMessages.RoomTypeAlreadyCreated);

            IRoom roomToAdd = null;
            switch (roomTypeName)
            {
                case "Apartment": roomToAdd = new Apartment();
                    break;
                case "DoubleBed": roomToAdd = new DoubleBed();
                    break;
                case "Studio": roomToAdd = new Studio();
                    break;
                default: throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }

            hotelToAddNewRoomType.Rooms.AddNew(roomToAdd);
            return string.Format(OutputMessages.RoomTypeAdded, roomTypeName, hotelName);
        }
    }
}
