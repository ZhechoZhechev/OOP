namespace TheContentDepartment.Models;

using TheContentDepartment.Utilities.Messages;

public class ContentMember : TeamMember
{
    private readonly string[] pathOptions = new string[] { "CSharp", "JavaScript", "Python", "Java" };
    public ContentMember(string name, string path) : base(name, path)
    {
        if (!pathOptions.Contains(path))
        {
            throw new ArgumentException(ExceptionMessages.PathIncorrect);
        }
    }

    public override string ToString()
    {
        return $"{this.Name} - {this.Path} path. Currently working on {this.InProgress.Count()} tasks.";
    }
}
