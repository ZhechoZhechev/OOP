namespace TheContentDepartment.Models;

using TheContentDepartment.Utilities.Messages;

public class TeamLead : TeamMember
{
    public TeamLead(string name, string path) : base(name, path)
    {
        if (path != "Master")
        {
            throw new ArgumentException(ExceptionMessages.PathIncorrect);
        }
    }

    public override string ToString()
    {
        return $"{this.Name} ({GetType().Name}) – Currently working on {this.InProgress.Count()} tasks.";
    }
}
