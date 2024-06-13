namespace Stealer
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class Spy
    {
        public string GettersAndSetters(string className)
        {
            Type classType = Type.GetType(className);

            MethodInfo[] allmethods = classType.GetMethods(BindingFlags.Instance | BindingFlags.Public 
                | BindingFlags.NonPublic);

            StringBuilder sb = new StringBuilder();
            foreach (MethodInfo method in allmethods) 
            {
                if (method.Name.Contains("get"))
                {
                    sb.AppendLine($"{method.Name} will return {method.ReturnType}");
                }
                else if (method.Name.Contains("set"))
                {
                    sb.AppendLine($"{method.Name} will set field of {method.GetParameters().First().ParameterType}");
                }        
            }

            return sb.ToString().TrimEnd();
        }
    }
}
