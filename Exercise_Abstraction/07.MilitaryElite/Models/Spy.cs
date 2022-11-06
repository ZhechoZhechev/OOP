
namespace MilitaryElite.Models
{
    using System;

    using Interfaces;

    public class Spy : Soldier, ISpy
    {
        public Spy(string firstName, string lastName, int id, int codeNumber)
            : base(firstName, lastName, id)
        {
            CodeNumber = codeNumber;
        }

        public int CodeNumber { get; private set; }
        public override string ToString()
        {
            return base.ToString() + Environment.NewLine +
                $"Code Number: {this.CodeNumber}";
        }
    }
}
