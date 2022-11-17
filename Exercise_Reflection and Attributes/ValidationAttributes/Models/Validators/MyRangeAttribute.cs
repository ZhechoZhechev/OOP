namespace ValidationAttributes.Models.Validators
{
    using System;
    public class MyRangeAttribute : MyValidationAttribute
    {
        private readonly int minValue;
        private readonly int maxValue;

        public MyRangeAttribute(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public override bool IsValid(object obj)
        {
            if (obj is Int32)
            {
                int value = (int)obj;
                if (value < minValue || value > maxValue)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                throw new InvalidOperationException("Can not validate the given data!");
            }
        }

        public void ValidateRange(int minValue, int maxValue) 
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException("Invalid range!");
            }
        }
    }
}
