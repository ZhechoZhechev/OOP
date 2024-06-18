namespace TheContentDepartment.Models;

using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Utilities.Messages;

public abstract class TeamMember : ITeamMember
{
    private string name = null!;
    private string path = null!;
    private List<string> inProgress;

    protected TeamMember(string name, string path)
    {
        this.Name = name;
        this.Path = path;
        this.inProgress = new List<string>();
    }
    public string Name
    {
        get => this.name;

        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.NameNullOrWhiteSpace);
            }

            this.name = value;
        }
    }

    public IReadOnlyCollection<string> InProgress => this.inProgress.AsReadOnly();
    public string Path 
    { 
        get => this.path;

        protected set 
        {
            this.path = value;
        } 
    }

    public void FinishTask(string resourceName)
    {
        this.inProgress.Remove(resourceName);
    }

    public void WorkOnTask(string resourceName)
    {
        if (this.InProgress.Contains(resourceName))
        {
            this.inProgress.Add(resourceName);
        }
    }
}
