namespace Cars
{
    using System;
    class Seat : ICar
    {
        private string model;
        private string color;

        public Seat(string model, string color)
        {
            this.model = model;
            this.color = color;
        }

        public string Model => this.model;

        public string Color => this.color;

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
            return $"{Color} Seat {Model}{Environment.NewLine}{Start()}" +
                $"{Environment.NewLine}{Stop()}";
        }
    }
}
