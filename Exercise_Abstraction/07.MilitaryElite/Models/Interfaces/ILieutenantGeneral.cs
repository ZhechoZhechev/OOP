
namespace MilitaryElite.Models.Interfaces
{
    using System.Collections.Generic;
    interface ILieutenantGeneral : IPrivate
    {
        IReadOnlyCollection<IPrivate> Privates { get; }
    }
}
