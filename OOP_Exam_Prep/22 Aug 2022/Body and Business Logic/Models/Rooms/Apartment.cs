namespace BookingApp.Models.Rooms
{
    public class Apartment : Room
    {
        private const int APARTMENT_CAPACITY = 6;
        public Apartment()
            : base(APARTMENT_CAPACITY)
        {
        }
    }
}
