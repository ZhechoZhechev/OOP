namespace TheContentDepartment.Core;

using TheContentDepartment.Models;
using TheContentDepartment.Repositories;
using TheContentDepartment.Core.Contracts;
using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Utilities.Messages;
using TheContentDepartment.Repositories.Contracts;
using System.Text;

/// <summary>
/// Not working properly 2 point missing
/// </summary>
public class Controller : IController
{
    private IRepository<IResource> resources = new ResourceRepository();
    private IRepository<ITeamMember> members = new MemberRepository();
    private readonly string[] resourceTypes = new string[] { "Exam", "Workshop", "Presentation" };

    public string ApproveResource(string resourceName, bool isApprovedByTeamLead)
    {
        var resourseToApprove = resources.TakeOne(resourceName);

        if (resourseToApprove.IsTested == false)
        {
            return string.Format(OutputMessages.ResourceNotTested, resourceName);
        }

        var teamLead = members.Models.FirstOrDefault(x => x.GetType().Name == nameof(TeamLead));
        if (isApprovedByTeamLead)
        {
            resourseToApprove.Approve();
            teamLead!.FinishTask(resourceName);
            return string.Format(OutputMessages.ResourceApproved, teamLead.Name, resourceName);
        }
        else
        {
            resourseToApprove.Test();
            return string.Format(OutputMessages.ResourceReturned, teamLead!.Name, resourceName);
        }
    }

    public string CreateResource(string resourceType, string resourceName, string path)
    {

        ITeamMember availableMember = null;
        foreach (var member in members.Models)
        {
            if (member.Path == path)
            {
                if (member.InProgress.Contains(resourceName))
                {
                    return string.Format(OutputMessages.ResourceExists, resourceName);
                }

                availableMember = member;
                break;
            }

        }
        if (availableMember == null) 
        {
            return string.Format(OutputMessages.NoContentMemberAvailable, resourceName);
        }

        IResource resource = null;
        switch (resourceType)
        {
            case "Exam":
                resource = new Exam(resourceName, availableMember.Name);
                break;
            case "Workshop":
                resource = new Workshop(resourceName, availableMember.Name);
                break;
            case "Presentation":
                resource = new Presentation(resourceName, availableMember.Name);
                break;
            default:
                return string.Format(OutputMessages.ResourceTypeInvalid, resourceType);
        }

        resources.Add(resource);
        availableMember.WorkOnTask(resource.Name);
        return string.Format(OutputMessages.ResourceCreatedSuccessfully, availableMember.Name, resourceType, resourceName);
    }

    public string DepartmentReport()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("Finished Tasks:");
        foreach (var resource in resources.Models.Where(x => x.IsApproved))
        {
            sb.AppendLine($"--{resource.ToString()}");
        }

        sb.AppendLine("Team Report:");
        var teamLead = members.Models.FirstOrDefault(x => x.GetType().Name == nameof(TeamLead));
        sb.AppendLine($"--{teamLead!.Name} (TeamLead) - Currently working on {teamLead.InProgress.Count} tasks.");
        foreach (var member in members.Models.Where(x => x.GetType().Name != nameof(TeamLead)))
        {
            sb.AppendLine(member.ToString());
        }

        return sb.ToString().TrimEnd();
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
        var member = this.members.TakeOne(memberName);
        if (member == null)
        {
            return string.Format(OutputMessages.WrongMemberName);
        }

        var highestPriorityResourse = resources.Models.Where(x => x.IsTested == false
        && x.Creator == memberName)
            .OrderBy(x => x.Priority).FirstOrDefault();
        if (highestPriorityResourse == null)
        {
            return string.Format(OutputMessages.NoResourcesForMember, memberName);
        }

        var teamLead = members.Models.FirstOrDefault(x => x.GetType().Name == nameof(TeamLead));

        highestPriorityResourse.Test();
        member.FinishTask(highestPriorityResourse.Name);
        teamLead!.WorkOnTask(highestPriorityResourse.Name);

        return string.Format(OutputMessages.ResourceTested, highestPriorityResourse.Name);
    }
}