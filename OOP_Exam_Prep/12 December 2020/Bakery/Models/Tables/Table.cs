
namespace Bakery.Models.Tables
{
    using Bakery.Models.BakedFoods.Contracts;
    using Bakery.Models.Drinks.Contracts;
    using Bakery.Utilities.Messages;
    using Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class Table : ITable
    {
        private List<IBakedFood> foodOrders;
        private List<IDrink> drinkOrders;
        private int capacity;
        private int numberOfPeople;
        private int tableNumber;
        private bool isReserved;
        private decimal pricePerPerson;

        public Table(int tableNumber, int capacity, decimal pricePerPerson)
        {
            TableNumber = tableNumber;
            Capacity = capacity;
            PricePerPerson = pricePerPerson;
            foodOrders = new List<IBakedFood>();
            drinkOrders = new List<IDrink>();
        }
        public int TableNumber
        {
            get { return tableNumber; }
            private set { tableNumber = value; }
        }

        public int Capacity
        {
            get => this.capacity;
            private set
            {
                if (value < 0)
                    throw new ArgumentException(ExceptionMessages.InvalidTableCapacity);

                this.capacity = value;
            }
        }

        public int NumberOfPeople
        {
            get => this.numberOfPeople;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException(ExceptionMessages.InvalidNumberOfPeople);

                this.numberOfPeople = value;
            }
        }

        public decimal PricePerPerson
        {
            get { return pricePerPerson; }
            private set { pricePerPerson = value; }
        }

        public bool IsReserved
        {
            get { return isReserved; }
            private set { isReserved = value; }
        }

        public decimal Price => PricePerPerson * NumberOfPeople;

        public void Clear()
        {
            this.drinkOrders.Clear();
            this.foodOrders.Clear();
            numberOfPeople = 0;
            IsReserved = false;
        }

        public decimal GetBill()
        {
            var drinksCost = this.drinkOrders.Select(x => x.Price).Sum();
            var foodCost = this.foodOrders.Select(x => x.Price).Sum();

            return Price + drinksCost + foodCost;
        }

        public string GetFreeTableInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Table: {TableNumber}")
                .AppendLine($"Type: {this.GetType().Name}")
                .AppendLine($"Capacity: {Capacity}")
                .AppendLine($"Price per Person: {PricePerPerson}");

            return sb.ToString().TrimEnd();
        }

        public void OrderDrink(IDrink drink)
        {
            this.drinkOrders.Add(drink);
        }

        public void OrderFood(IBakedFood food)
        {
            this.foodOrders.Add(food);
        }

        public void Reserve(int numberOfPeople)
        {
            IsReserved = true;
            NumberOfPeople = numberOfPeople;
        }
    }
}
