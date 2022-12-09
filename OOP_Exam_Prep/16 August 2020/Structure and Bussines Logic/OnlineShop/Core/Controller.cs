using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Computers;
using OnlineShop.Models.Products.Peripherals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Core
{
    public class Controller : IController
    {
        private List<IComputer> computers;
        private List<IComponent> components;
        private List<IPeripheral> peripherals;

        public Controller()
        {
            this.computers = new List<IComputer>();
            this.components = new List<IComponent>();
            this.peripherals = new List<IPeripheral>();
        }
        private IComputer FindComputerById(int id)
        {
            IComputer computer = computers.Find(x => x.Id == id);
            if (computer == null)
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            return computer;
        }
        public string AddComponent(int computerId, int id, string componentType, string manufacturer, string model,
            decimal price, double overallPerformance, int generation)
        {
            if (!this.computers.Any(x => x.Id == computerId))
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);

            if (components.Any(x => x.Id == id))
                throw new ArgumentException(ExceptionMessages.ExistingComponentId);

            IComponent component;
            switch (componentType)
            {
                case "CentralProcessingUnit":
                    component = new CentralProcessingUnit
                        (id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case "Motherboard":
                    component = new Motherboard
                        (id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case "PowerSupply":
                    component = new PowerSupply
                        (id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case "RandomAccessMemory":
                    component = new RandomAccessMemory
                        (id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case "SolidStateDrive":
                    component = new SolidStateDrive
                        (id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case "VideoCard":
                    component = new VideoCard
                        (id, manufacturer, model, price, overallPerformance, generation);
                    break;
                default: throw new ArgumentException(ExceptionMessages.InvalidComponentType);
            }

            var compToAddComponentTo = this.computers.Find(x => x.Id == computerId);
            compToAddComponentTo.AddComponent(component);
            this.components.Add(component);

            return string.Format(SuccessMessages.AddedComponent, componentType, id, computerId);
        }

        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            if (this.computers.Any(x => x.Id == id))
                throw new ArgumentException(ExceptionMessages.ExistingComputerId);

            IComputer computerToAdd;
            switch (computerType)
            {
                case "DesktopComputer":
                    computerToAdd = new DesktopComputer(id, manufacturer, model, price);
                    break;
                case "Laptop":
                    computerToAdd = new Laptop(id, manufacturer, model, price);
                    break;
                default: throw new ArgumentException(ExceptionMessages.InvalidComputerType);
            }

            this.computers.Add(computerToAdd);
            return string.Format(SuccessMessages.AddedComputer, id);
        }

        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer,
            string model, decimal price, double overallPerformance, string connectionType)
        {
            if (!this.computers.Any(x => x.Id == computerId))
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);

            if (this.peripherals.Any(x => x.Id == id))
                throw new ArgumentException(ExceptionMessages.ExistingPeripheralId);

            IPeripheral perToAdd;
            switch (peripheralType)
            {
                case "Headset":
                    perToAdd = new Headset(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;
                case "Keyboard":
                    perToAdd = new Keyboard(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;
                case "Monitor":
                    perToAdd = new Monitor(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;
                case "Mouse":
                    perToAdd = new Mouse(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;
                default: throw new ArgumentException(ExceptionMessages.InvalidPeripheralType);
            }

            var compToAdPerTo = this.computers.First(x => x.Id == computerId);
            compToAdPerTo.AddPeripheral(perToAdd);
            this.peripherals.Add(perToAdd);

            return string.Format(SuccessMessages.AddedPeripheral, peripheralType, id, computerId);
        }

        public string BuyBest(decimal budget)
        {
            var bestCompToBuy = this.computers.OrderByDescending(x => x.OverallPerformance).Where(y => y.Price <= budget).FirstOrDefault();
            if (bestCompToBuy == null)
                throw new ArgumentException(string.Format(ExceptionMessages.CanNotBuyComputer, budget));

            this.computers.Remove(bestCompToBuy);

            return bestCompToBuy.ToString();
        }

        public string BuyComputer(int id)
        {
            if (!this.computers.Any(x => x.Id == id))
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);

            var compToBuy = this.computers.Find(x => x.Id == id);
            this.computers.Remove(compToBuy);

            return compToBuy.ToString();
        }

        public string GetComputerData(int id)
        {
            IComputer computer = FindComputerById(id);
            return computer.ToString();
        }

        public string RemoveComponent(string componentType, int computerId)
        {
            if (!this.computers.Any(x => x.Id == computerId))
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);

            var computerToremoveComponentFrom = this.computers.First(x => x.Id == computerId);
            computerToremoveComponentFrom.RemoveComponent(componentType);

            var componentToRemove = this.components.First(x => x.GetType().Name == componentType);
            this.components.Remove(componentToRemove);

            return string.Format(SuccessMessages.RemovedComponent, componentType, componentToRemove.Id);
        }

        public string RemovePeripheral(string peripheralType, int computerId)
        {
            if (!this.computers.Any(x => x.Id == computerId))
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);

            var compToremovePerFrom = this.computers.First(x => x.Id == computerId);
            var peripherialsToremove = this.peripherals.FirstOrDefault(y => y.GetType().Name == peripheralType);

            compToremovePerFrom.RemovePeripheral(peripheralType);
            this.peripherals.Remove(peripherialsToremove);

            if (peripherals != null)
                return string.Format(SuccessMessages.RemovedPeripheral, peripheralType, peripherialsToremove.Id);
            else
                return string.Empty;
        }
    }
}
