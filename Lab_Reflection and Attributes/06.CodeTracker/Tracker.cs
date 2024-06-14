using System;
using System.Linq;
using System.Reflection;

namespace AuthorProblem
{
    public class Tracker
    {
        public void PrintMethodsByAuthor() 
        {
            var classType = typeof(StartUp);
            var methods = classType.GetMethods(BindingFlags.Instance | BindingFlags.Static 
                | BindingFlags.Public);
            foreach (var method in methods) 
            {
                if (method.CustomAttributes.Any(x => x.AttributeType == typeof(AuthorAttribute)))
                {
                    var attributes = method.GetCustomAttributes(false);
                    foreach (AuthorAttribute attribute in attributes) 
                    {
                        Console.WriteLine($"{method.Name} is writen by {attribute.Name}");
                    }
                }
            }
        }
    }
}
