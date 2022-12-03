
namespace Bakery.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Bakery.Models.BakedFoods;
    using Bakery.Models.Drinks;
    using Bakery.Models.Tables;
    using Bakery.Utilities.Messages;
    using Contracts;
    using Models.BakedFoods.Contracts;
    using Models.Drinks.Contracts;
    using Models.Tables.Contracts;

    public class Controller : IController
    {
        private List<IBakedFood> bakedFoods;
        private List<IDrink> drinks;
        private List<ITable> tables;
        private decimal income;

        public Controller()
        {
            bakedFoods = new List<IBakedFood>();
            drinks = new List<IDrink>();
            tables = new List<ITable>();
        }
        public string AddDrink(string type, string name, int portion, string brand)
        {
            IDrink drinkToAdd = null;
            switch (type)
            {
                case "Tea": drinkToAdd = new Tea(name, portion, brand);
                    break;
                case "Water": drinkToAdd = new Water(name, portion, brand);
                    break;
            }

            this.drinks.Add(drinkToAdd);
            return string.Format(OutputMessages.DrinkAdded, name, brand);
        }

        public string AddFood(string type, string name, decimal price)
        {
            IBakedFood foodToAdd = null;
            switch (type)
            {
                case "Bread": foodToAdd = new Bread(name, price);
                    break;
                case "Cake": foodToAdd = new Cake(name, price);
                    break;
            }

            this.bakedFoods.Add(foodToAdd);
            return string.Format(OutputMessages.FoodAdded, name, type);
        }

        public string AddTable(string type, int tableNumber, int capacity)
        {
            ITable table = null;
            switch (type)
            {
                case "InsideTable": table = new InsideTable(tableNumber, capacity);
                    break;
                case "OutsideTable": table = new OutsideTable(tableNumber, capacity);
                    break;
            }

            this.tables.Add(table);
            return string.Format(OutputMessages.TableAdded, tableNumber);
        }

        public string GetFreeTablesInfo()
        {
            var freeTables = tables.FindAll(x => x.IsReserved == false);

            return string.Join(Environment.NewLine, freeTables.Select(x => x.GetFreeTableInfo()));
        }

        public string GetTotalIncome()
        {
            return $"Total income: {this.income}lv";
        }

        public string LeaveTable(int tableNumber)
        {
            var tableToLeave = tables.Find(x => x.TableNumber == tableNumber);

            var theBill =  tableToLeave.GetBill();
            tableToLeave.Clear();
            this.income += theBill;
            return $"Table: {tableNumber}{Environment.NewLine}Bill: {theBill:f2}";
        }

        public string OrderDrink(int tableNumber, string drinkName, string drinkBrand)
        {
            var tableToOrderDrinks = this.tables.Find(x => x.TableNumber == tableNumber);
            if (tableToOrderDrinks == null)
                return string.Format(OutputMessages.WrongTableNumber, tableNumber);

            var drinksToOrder = this.drinks.Find
                (x => x.Name == drinkName && x.Brand == drinkBrand);
            if (drinksToOrder == null)
                return string.Format(OutputMessages.NonExistentDrink, drinkName, drinkBrand);

            tableToOrderDrinks.OrderDrink(drinksToOrder);
            return $"Table {tableNumber} ordered {drinkName} {drinkBrand}";
        }

        public string OrderFood(int tableNumber, string foodName)
        {
            var tableToOrderFood = this.tables.Find(x => x.TableNumber == tableNumber);
            if (tableToOrderFood == null)
                return string.Format(OutputMessages.WrongTableNumber, tableNumber);

            var foodToOrder = this.bakedFoods.Find(z => z.Name == foodName);
            if (foodToOrder == null)
                return string.Format(OutputMessages.NonExistentFood, foodName);

            tableToOrderFood.OrderFood(foodToOrder);
            return string.Format(OutputMessages.FoodOrderSuccessful, tableNumber, foodName);
        }

        public string ReserveTable(int numberOfPeople)
        {
            ITable table = tables.Find(x => !x.IsReserved && x.Capacity >= numberOfPeople);
            if (table == null)
                return String.Format(OutputMessages.ReservationNotPossible, numberOfPeople);
            table.Reserve(numberOfPeople);
            return String.Format(OutputMessages.TableReserved, table.TableNumber, numberOfPeople);

        }
    }
}
