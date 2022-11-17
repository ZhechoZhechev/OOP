
namespace ValidationAttributes.Models.Validators
{
    using System;
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public abstract class MyValidationAttribute : Attribute
    {
        public abstract bool IsValid(object obj);
    }
}
