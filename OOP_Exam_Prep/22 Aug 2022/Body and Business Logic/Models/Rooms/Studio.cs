namespace BookingApp.Models.Rooms
{
    public class Studio : Room
    {
        private const int STUDIO_CAPACITY = 4;

        public Studio()
            : base(STUDIO_CAPACITY)
        {
        }
    }
}
