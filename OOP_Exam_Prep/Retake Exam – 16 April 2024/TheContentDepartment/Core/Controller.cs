namespace TheContentDepartment.Core;

using TheContentDepartment.Core.Contracts;
using TheContentDepartment.Models;
using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Repositories;
using TheContentDepartment.Repositories.Contracts;
using TheContentDepartment.Utilities.Messages;

public class Controller : IController
{
    private IRepository<IResource> resources = new ResourceRepository();
    private IRepository<ITeamMember> members = new MemberRepository();
    //private readonly string[] contentTeamTypw = new string[] { "CSharp", "JavaScript", "Python", "Java" };

    public string ApproveResource(string resourceName, bool isApprovedByTeamLead)
    {
        throw new NotImplementedException();
    }

    public string CreateResource(string resourceType, string resourceName, string path)
    {
        throw new NotImplementedException();
    }

    public string DepartmentReport()
    {
        throw new NotImplementedException();
    }

    public string JoinTeam(string memberType, string memberName, string path)
    {
        ITeamMember newMember;
        switch (memberType)
        {
            case nameof(TeamLead):
                newMember = new TeamLead(memberName, path);
                break;
            case nameof(ContentMember):
                newMember = new ContentMember(memberName, path);
                break;
            default: return string.Format(OutputMessages.MemberTypeInvalid, memberType);
        }

        if (members.Models.Any(x => x.Path == path))
        {
            return OutputMessages.PositionOccupied;
        }

        if (members.Models.Any(x => x.Name == memberName))
        {
            return string.Format(OutputMessages.MemberExists, memberName);
        }

        members.Add(newMember);

        return string.Format(OutputMessages.MemberJoinedSuccessfully, memberName);
    }

    public string LogTesting(string memberName)
    {
        throw new NotImplementedException();
    }
}