namespace _01.ClassBoxData
{
    using System;
    public class Box
    {
        private double length;
        private double width;
        private double height;

        public Box(double length, double width, double height)
        {
            Length = length;
            Width = width;
            Height = height;
        }
        public double Length
        {
            get { return this.length; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(ErrorMassages.ZeroOrNullMsg, nameof(this.Length)));
                }

                this.length = value;
            }
        }
        public double Width
        {
            get { return this.width; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(ErrorMassages.ZeroOrNullMsg, nameof(this.Width)));
                }

                this.width = value;
            }
        }
        public double Height
        {
            get { return this.height; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(ErrorMassages.ZeroOrNullMsg, nameof(this.Height)));
                }

                this.height = value;
            }
        }
        public double SurfaceArea() 
        {
            return (2 * Length * Width) + LateralSurfaceArea();
        }
        public double LateralSurfaceArea() 
        {
            return (2 * Length * Height) + (2 * Width * Height);
        }
        public double Volume() 
        {
            return Length * Width * Height;
        }
    }
}
