namespace Cars
{
    using System;
    public class Tesla : ICar, IElectricCar
    {
        private string model, color;
        private int battery;

        public Tesla(string model, string color, int battery)
        {
            this.model = model;
            this.color = color;
            this.battery = battery;
        }

        public string Model => this.model;

        public string Color => this.color;

        public int Battery => this.battery;

        public string Start()
        {
            return "Engine start";
        }

        public string Stop()
        {
            return "Breaaak!";
        }
        public override string ToString()
        {
            return $"{Color} Tesla {Model} with {Battery} Batteries{Environment.NewLine}{Start()}" +
                $"{Environment.NewLine}{Stop()}";
        }
    }
}
