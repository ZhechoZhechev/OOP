﻿namespace ValidationAttributes.Models.Validators
{
    public class MyRequiredAttribute : MyValidationAttribute
    {
        public override bool IsValid(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            return true;
        }
    }
}
