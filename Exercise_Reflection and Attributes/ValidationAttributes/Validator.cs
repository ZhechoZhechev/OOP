namespace ValidationAttributes
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Models.Validators;

    public static class Validator
    {
        public static bool IsValid(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Type objType = obj.GetType();
            PropertyInfo[] propertyInfos = objType.GetProperties();

            foreach (var property in propertyInfos)
            {
                MyValidationAttribute[] attributes = property.GetCustomAttributes()
                    .Where(ca => ca is MyValidationAttribute)
                    .Cast<MyValidationAttribute>()
                    .ToArray();
                foreach (var attribute in attributes)
                {
                    if (!attribute.IsValid(property.GetValue(obj)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
