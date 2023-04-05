using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Booths.Models;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Linq;
using System.Text;

namespace ChristmasPastryShop.Core
{
    public class Controller : IController
    {
        private BoothRepository booths;

        public Controller()
        {
            this.booths = new BoothRepository();
        }
        public string AddBooth(int capacity)
        {
            int boothId = booths.Models.Count + 1;

            Booth booth = new Booth(boothId, capacity);

            this.booths.AddModel(booth);
            return $"Added booth number {boothId} with capacity {capacity} in the pastry shop!";
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            var boothToAddCock  = this.booths.Models.FirstOrDefault(x => x.BoothId == boothId);

            if (!new string[] { "Small", "Middle", "Large" }.Contains(size))
                return $"{size} is not recognized as valid cocktail size!";
            if (boothToAddCock.CocktailMenu.Models.Any(x => x.Name == cocktailName && x.Size == size))
                return $"{ size} { cocktailName} is already added in the pastry shop!";

            ICocktail cocktail;
            switch (cocktailTypeName)
            {
                case "Hibernation": cocktail = new Hibernation(cocktailName, size);
                    break;
                case "MulledWine": cocktail = new MulledWine(cocktailName, size);
                    break;
                default: return $"Cocktail type {cocktailTypeName} is not supported in our application!";
            }

            boothToAddCock.CocktailMenu.AddModel(cocktail);
            return $"{size} {cocktailName} {cocktailTypeName} added to the pastry shop!";
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            var boothToAddDelicacy = this.booths.Models.FirstOrDefault(x => x.BoothId == boothId);

            if (boothToAddDelicacy.DelicacyMenu.Models.Any(x => x.Name == delicacyName))
               return $"{delicacyName} is already added in the pastry shop!";

            IDelicacy delicacyToAdd;
            switch (delicacyTypeName)
            {
                case "Gingerbread":
                    delicacyToAdd = new Gingerbread(delicacyName);
                    break;
                case "Stolen":
                    delicacyToAdd = new Stolen(delicacyName);
                    break;
                default: return $"Delicacy type {delicacyTypeName} is not supported in our application!";
            }
            boothToAddDelicacy.DelicacyMenu.AddModel(delicacyToAdd);
            return $"{delicacyTypeName} {delicacyName} added to the pastry shop!";
        }

        public string BoothReport(int boothId)
        {
            var boothToReport = this.booths.Models.First(x => x.BoothId == boothId);
            return boothToReport.ToString();
        }

        public string LeaveBooth(int boothId)
        {
            var boothToLeave = this.booths.Models.First(x => x.BoothId == boothId);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Bill {boothToLeave.CurrentBill:f2} lv");

            boothToLeave.Charge();
            boothToLeave.ChangeStatus();

            sb.AppendLine($"Booth {boothId} is now available!");

            return sb.ToString().TrimEnd();
        }

        public string ReserveBooth(int countOfPeople)
        {
            var boothsNeeded = this.booths.Models.Where(x => !x.IsReserved && x.Capacity >= countOfPeople);
            var boothsOrdered = boothsNeeded.OrderBy(x => x.Capacity).ThenByDescending(y => y.BoothId);

            var boothToTake = boothsOrdered.FirstOrDefault();
            if (boothToTake == null)
            {
                return $"No available booth for {countOfPeople} people!";
            }
            else
            {
                boothToTake.ChangeStatus();
                return $"Booth {boothToTake.BoothId} has been reserved for {countOfPeople} people!";
            }
        }

        public string TryOrder(int boothId, string order)
        {
            var boothToTryOrder = this.booths.Models.First(x => x.BoothId == boothId);
            string[] orders = order.Split("/", StringSplitOptions.RemoveEmptyEntries);

            string itemTypeName = orders[0];
            string itemName = orders[1];

            if (!new string[] { "Hibernation", "MulledWine", "Gingerbread", "Stolen" }.Contains(itemTypeName))
                return $"{itemTypeName} is not recognized type!";

            if (itemTypeName == "MulledWine" || itemTypeName == "Hibernation") 
            {
                int countOfPiecesOrderd = int.Parse(orders[2]);
                string size = orders[3];
                if (!boothToTryOrder.CocktailMenu.Models.Any(x => x.Name == itemName))
                    return $"There is no {itemTypeName} {itemName} available!";
                if (!boothToTryOrder.CocktailMenu.Models.Any(x => x.Name == itemName && x.Size == size))
                    return $"There is no {size} {itemName} available!";
                ICocktail cocktail = null;
                switch (itemTypeName)
                {
                    case "Hibernation":
                        cocktail = new Hibernation(itemName, size);
                        break;
                    case "MulledWine":
                        cocktail = new MulledWine(itemName, size);
                        break;
                }

                boothToTryOrder.UpdateCurrentBill(cocktail.Price * countOfPiecesOrderd);
                return $"Booth {boothId} ordered {countOfPiecesOrderd} {itemName}!";
            }
            else
            {
                int countOfPiecesOrderd = int.Parse(orders[2]);
                if (!boothToTryOrder.DelicacyMenu.Models.Any(x => x.Name == itemName))
                    return $"There is no {itemTypeName} {itemName} available!";
                IDelicacy delicacyToAdd = null;
                switch (itemTypeName)
                {
                    case "Gingerbread":
                        delicacyToAdd = new Gingerbread(itemName);
                        break;
                    case "Stolen":
                        delicacyToAdd = new Stolen(itemName);
                        break;
                }
                boothToTryOrder.UpdateCurrentBill(delicacyToAdd.Price * countOfPiecesOrderd);
                return $"Booth {boothId} ordered {countOfPiecesOrderd} {itemName}!";
            }



           

        }
    }
}
