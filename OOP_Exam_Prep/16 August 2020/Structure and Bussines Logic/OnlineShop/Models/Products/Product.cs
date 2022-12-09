
namespace OnlineShop.Models.Products
{
    using OnlineShop.Common.Constants;
    using System;
    public abstract class Product : IProduct
    {
        private int id;
        private string manufacturer;
        private string model;
        private decimal price;
        private double overallPerformance;

        protected Product(int id, string manufacturer, string model, decimal price, double overallPerformance)
        {
            Id = id;
            Manufacturer = manufacturer;
            Model = model;
            Price = price;
            OverallPerformance = overallPerformance;
        }

        public int Id
        {
            get => this.id;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException(ExceptionMessages.InvalidProductId);

                this.id = value;
            }
        }

        public string Manufacturer
        {
            get => this.manufacturer;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExceptionMessages.InvalidManufacturer);

                this.manufacturer = value;
            }
        }

        public string Model
        {
            get => this.model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExceptionMessages.InvalidModel);

                this.model = value;
            }
        }
        public virtual decimal Price
        {
            get => this.price;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException(ExceptionMessages.InvalidPrice);

                this.price = value;
            }
        }

        public virtual double OverallPerformance
        {
            get => this.overallPerformance;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException(ExceptionMessages.InvalidOverallPerformance);

                this.overallPerformance = value;
            }
        }
        public override string ToString()
          => string.Format(SuccessMessages.ProductToString, $"{OverallPerformance:f2}", $"{Price:f2}", this.GetType().Name, Manufacturer, Model, Id);
    }
}
