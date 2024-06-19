namespace TheContentDepartment.Repositories;

using System.Collections.Generic;

using Models.Contracts;
using Repositories.Contracts;
public class MemberRepository : IRepository<ITeamMember>
{
    private readonly List<ITeamMember> members = new List<ITeamMember>();

    public IReadOnlyCollection<ITeamMember> Models => this.members.AsReadOnly();

    public void Add(ITeamMember model)
    {
        this.members.Add(model);
    }

    public ITeamMember TakeOne(string modelName)
    {
        return this.members.FirstOrDefault(x => x.Name == modelName)!;
    }
}
