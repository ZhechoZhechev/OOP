using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Peripherals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Models.Products.Computers
{
    public abstract class Computer : Product, IComputer
    {
        private List<IComponent> components;
        private List<IPeripheral> peripherals;
        protected Computer(int id, string manufacturer, string model, decimal price, double overallPerformance)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            this.components = new List<IComponent>();
            this.peripherals = new List<IPeripheral>();
        }

        public override double OverallPerformance
        {
            get
            {
                if (components.Count == 0)
                    return base.OverallPerformance;
                else
                    return base.OverallPerformance +
                        (this.components.Select(x => x.OverallPerformance).Sum() / this.components.Count);
            }
        }

        public override decimal Price
        {
            get
            {
                var componenentsPrice = this.components.Select(x => x.Price).Sum();
                var peripherialsPrice = this.peripherals.Select(y => y.Price).Sum();

                return base.Price + componenentsPrice + peripherialsPrice;
            }
        }

        public IReadOnlyCollection<IComponent> Components => this.components.AsReadOnly();

        public IReadOnlyCollection<IPeripheral> Peripherals => this.peripherals.AsReadOnly();

        public void AddComponent(IComponent component)
        {
            if (components.Any(x => x.GetType().Name == component.GetType().Name))
                throw new ArgumentException(string.Format
                    (ExceptionMessages.ExistingComponent, component.GetType().Name, this.GetType().Name, Id));

            this.components.Add(component);
        }
        public void AddPeripheral(IPeripheral peripheral)
        {
            if (peripherals.Any(x => x.GetType().Name == peripheral.GetType().Name))
                throw new ArgumentException(string.Format
                    (ExceptionMessages.ExistingPeripheral, peripheral.GetType().Name, this.GetType().Name, Id));

            this.peripherals.Add(peripheral);
        }

        public IComponent RemoveComponent(string componentType)
        {
            if (components.Count == 0 || !components.Any(x => x.GetType().Name == componentType))
                throw new ArgumentException(string.Format
                    (ExceptionMessages.NotExistingComponent, componentType, this.GetType().Name, Id));

            var compToremove = this.components.Find(x => x.GetType().Name == componentType);
            this.components.Remove(compToremove);

            return compToremove;
        }

        public IPeripheral RemovePeripheral(string peripheralType)
        {
            if (peripherals.Count == 0 || !peripherals.Any(x => x.GetType().Name == peripheralType))
                throw new ArgumentException(string.Format
                    (ExceptionMessages.NotExistingPeripheral, peripheralType, this.GetType().Name, Id));

            var perToremove = this.peripherals.Find(x => x.GetType().Name == peripheralType);
            this.peripherals.Remove(perToremove);

            return perToremove;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine($" Components ({components.Count}):");
            if (components.Any())
                sb.AppendLine(string.Join(Environment.NewLine, components.Select(x => $"  {x}")));

            double averagePerformance = peripherals.Count > 0 ? peripherals.Average(x => x.OverallPerformance) : 0;
            sb.AppendLine($" Peripherals ({peripherals.Count}); Average Overall Performance ({averagePerformance:f2}):");
            if (peripherals.Any())
                sb.Append(string.Join(Environment.NewLine, peripherals.Select(x => $"  {x}")));

            return sb.ToString().TrimEnd();
        }
    }
}
