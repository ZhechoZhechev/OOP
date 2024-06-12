namespace Stealer
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class Spy
    {
        public string AnalyzeAccessModifiers(string className)
        {
            Type classType = Type.GetType(className);

            FieldInfo[] fields = classType.GetFields(BindingFlags.Public | BindingFlags.Instance
                | BindingFlags.Static);
            MethodInfo[] publicMethods = classType.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            MethodInfo[] privateMethods = classType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);

            StringBuilder sb = new StringBuilder();
            foreach (FieldInfo field in fields)
            {
                sb.AppendLine($"{field.Name} must be private!");
            }
            foreach (MethodInfo privateMethod in privateMethods.Where(m => m.Name.StartsWith("get")))
            {
                sb.AppendLine($"{privateMethod.Name} must be public!");
            }
            foreach (MethodInfo publicMethod in publicMethods.Where(m => m.Name.StartsWith("set")))
            {
                sb.AppendLine($"{publicMethod.Name} must be private!");
            }

            return sb.ToString().Trim();
        }
    }
}
