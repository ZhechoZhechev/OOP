namespace Stealer
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class Spy
    {
        public string RevealPrivateMethods(string className)
        {
            Type targetedClass = Type.GetType(className);

            MethodInfo[] privMethods = targetedClass.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"All Private Methods of Class: {targetedClass}");
            sb.AppendLine($"Base Class: {targetedClass.BaseType.Name}");
            foreach (var method in privMethods)
            {
                sb.AppendLine(method.Name);
            }

            return sb.ToString().TrimEnd();
        }
    }
}
