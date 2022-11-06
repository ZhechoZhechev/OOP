
namespace MilitaryElite.Models
{
    using Interfaces;
    using MilitaryElite.Models.Enums;
    using System.Collections.Generic;
    using System.Text;

    public class Commando : SpecialisedSoldier, ICommando
    {
        private readonly ICollection<IMission> missions;
        public Commando(string firstName, string lastName, int id,
            decimal salary, Corps corps, ICollection<IMission> missions)
            : base(firstName, lastName, id, salary, corps)
        {
            this.missions = missions;
        }

        public IReadOnlyCollection<IMission> Missions
            => (IReadOnlyCollection<IMission>)this.missions;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb
                .AppendLine(base.ToString())
                .AppendLine("Missions:");
            foreach (IMission mission in this.Missions)
            {
                sb.AppendLine($"  {mission.ToString()}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
