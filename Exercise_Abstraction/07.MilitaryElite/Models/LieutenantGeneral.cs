
namespace MilitaryElite.Models
{
    using System.Collections.Generic;
    using System.Text;
    using Interfaces;

    public class LieutenantGeneral : Private, ILieutenantGeneral
    {
        private readonly ICollection<IPrivate> privates;
        public LieutenantGeneral(string firstName, string lastName, int id,
            decimal salary, ICollection<IPrivate> privates)
            : base(firstName, lastName, id, salary)
        {
            this.privates = privates;
        }

        public IReadOnlyCollection<IPrivate> Privates =>
            (IReadOnlyCollection<IPrivate>)this.privates;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb
                .AppendLine(base.ToString())
                .AppendLine($"Privates:");

            foreach (IPrivate pr in this.Privates)
            {
                sb.AppendLine($"  {pr.ToString()}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
