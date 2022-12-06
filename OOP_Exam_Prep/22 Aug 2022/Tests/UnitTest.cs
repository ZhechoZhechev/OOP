using FrontDeskApp;
using NUnit.Framework;
using System;

namespace BookigApp.Tests
{
    public class Tests
    {
        private const int RoomBedCapacity = 10;
        private const double RoomPricePerNight = 50;
        private const int BookingResidenceDuration = 2;
        private int BookingNumber = 123;
        private string HotelFullName = "Test";
        private int HotelCategory = 3;

        private Room room;
        private Booking booking;
        private Hotel hotel;
        [SetUp]
        public void Setup()
        {
            room = new Room(RoomBedCapacity, RoomPricePerNight);
            booking = new Booking(BookingNumber, room, BookingResidenceDuration);
            hotel = new Hotel(HotelFullName, HotelCategory);
        }

        [Test]
        public void ConstructroWorkProperly()
        {
            hotel.AddRoom(room);

            Assert.IsTrue(hotel.Rooms.Count == 1 && hotel.FullName == HotelFullName
                && hotel.Category == HotelCategory && hotel.Bookings != null);
        }

        [TestCase(null)]
        [TestCase("")]
        public void FullName_ExceptionWhenNullOrWhiteSpace(string fullName) 
        {
            Assert.That(() =>
            {
                Hotel hotel = new Hotel(fullName, HotelCategory);

            }, Throws.TypeOf<ArgumentNullException>());
        }

        [TestCase(0)]
        [TestCase(6)]
        public void Category_ExceptionWithInvalidValues(int category) 
        {
            Assert.That(() =>
            {
                Hotel hotel = new Hotel(HotelFullName, category);

            }, Throws.TypeOf<ArgumentException>());
        }
        [Test]
        public void Hotel_AddRoom_IncreasesCountOfTheCollection()
        {
            hotel.AddRoom(room);
            Assert.AreEqual(hotel.Rooms.Count, 1);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void BookRoom_ErrorIfAdultsNegativeOrZero(int adults) 
        {
            Assert.That(() =>
            {
                hotel.BookRoom(adults, 2, BookingResidenceDuration, 200);

            }, Throws.TypeOf<ArgumentException>());
            
        }

        [TestCase(-1)]
        public void BookRoom_ErrorIfChildrenNegative(int children)
        {
            Assert.That(() =>
            {
                hotel.BookRoom(2, children, BookingResidenceDuration, 200);

            }, Throws.TypeOf<ArgumentException>());

        }

        [TestCase(-1)]
        [TestCase(0)]
        public void BookRoom_ErrorIfresidenceDurationZeroOrNegative(int residenceDuration)
        {
            Assert.That(() =>
            {
                hotel.BookRoom(2, 2, residenceDuration, 200);

            }, Throws.TypeOf<ArgumentException>());

        }

        [Test]
        public void BookRoom_IncresesBookingsCountIfAllDataValid() 
        {
            hotel.AddRoom(room);
            hotel.BookRoom(2, 1, BookingResidenceDuration, 1100);

            Assert.AreEqual(hotel.Bookings.Count, 1);
        }

        [Test]
        public void BookRoom_ProperlyGeneratesTurnOver() 
        {
            hotel.AddRoom(room);
            hotel.BookRoom(2, 1, BookingResidenceDuration, 1100);
            double expectedTurnOver = BookingResidenceDuration * RoomPricePerNight;

            Assert.AreEqual(hotel.Turnover, expectedTurnOver);
        }

    }
}