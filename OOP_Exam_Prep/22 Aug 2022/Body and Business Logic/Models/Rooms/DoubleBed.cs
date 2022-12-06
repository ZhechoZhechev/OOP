namespace BookingApp.Models.Rooms
{
    public class DoubleBed : Room
    {
        private const int DOUBLE_BED_CAPACITY = 2;
        public DoubleBed()
            : base(DOUBLE_BED_CAPACITY)
        {
        }
    }
}
