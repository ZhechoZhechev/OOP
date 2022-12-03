namespace Bakery.Models.Tables
{
    public class OutsideTable : Table
    {
        private const decimal OUTSIDE_TABLE_PRICE_PER_PRESON = 3.50m;
        public OutsideTable(int tableNumber, int capacity)
            : base(tableNumber, capacity, OUTSIDE_TABLE_PRICE_PER_PRESON)
        {
        }
    }
}
