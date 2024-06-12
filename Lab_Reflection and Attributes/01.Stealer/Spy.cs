namespace Stealer
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class Spy
    {
        public string StealFieldInfo(string className, params string[] fieldsNames)
        {
            Type classType = Type.GetType(className);

            FieldInfo[] fields = classType.GetFields(BindingFlags.Instance | BindingFlags.Public
                | BindingFlags.NonPublic | BindingFlags.Static);

            object instance = Activator.CreateInstance(classType, []);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Class under investigation: {className}");
            foreach (FieldInfo field in fields.Where(f => fieldsNames.Contains(f.Name)))
            {
                sb.AppendLine($"{field.Name} = {field.GetValue(instance)}");
            }

            return sb.ToString().Trim();
        }
    }
}
