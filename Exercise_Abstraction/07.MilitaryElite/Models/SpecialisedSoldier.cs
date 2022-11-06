namespace MilitaryElite.Models
{
    using System;

    using Interfaces;
    using MilitaryElite.Models.Enums;

    public abstract class SpecialisedSoldier : Private, ISpecialisedSoldier
    {
        public SpecialisedSoldier(string firstName, string lastName, int id,
            decimal salary, Corps corps)
            : base(firstName, lastName, id, salary)
        {
            Corps = corps;
        }

        public Corps Corps { get; private set; }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine +
                $"Corps: {this.Corps.ToString()}";
        }
    }
}
