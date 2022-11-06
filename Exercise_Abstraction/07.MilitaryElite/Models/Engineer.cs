
namespace MilitaryElite.Models
{
    using Interfaces;
    using MilitaryElite.Models.Enums;
    using System.Collections.Generic;
    using System.Text;

    public class Engineer : SpecialisedSoldier, IEngineer
    {
        private readonly ICollection<IRepair> repairs;
        public Engineer(string firstName, string lastName, int id,
            decimal salary, Corps corps, ICollection<IRepair> repairs)
            : base(firstName, lastName, id, salary, corps)
        {
            this.repairs = repairs;
        }

        public IReadOnlyCollection<IRepair> Repairs
            => (IReadOnlyCollection<IRepair>)this.repairs;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb
                .AppendLine(base.ToString())
                .AppendLine("Repairs:");

            foreach (IRepair repair in this.Repairs)
            {
                sb.AppendLine($"  {repair.ToString()}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
