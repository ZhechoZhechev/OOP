
namespace CommandPattern.Models
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Core.Contracts;

    public class CommandInterpreter : ICommandInterpreter
    {
        public string Read(string args)
        {
            string[] commandargs = args
                .Split(" ", System.StringSplitOptions.RemoveEmptyEntries);
            string command = commandargs[0];
            string[] arguments = commandargs
                .Skip(1).ToArray();

            Assembly assembly = Assembly.GetCallingAssembly();

            Type commandType = assembly
                .GetTypes().FirstOrDefault(x => x.Name.ToLower().StartsWith(command.ToLower()));
            if (commandType == null)
            {
                throw new ArgumentException("There is no such commands!");
            }

            ICommand comnadInstance = (ICommand)Activator.CreateInstance(commandType);

            return comnadInstance.Execute(arguments);
        }
    }
}
