namespace TheContentDepartment.Models;

using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Utilities.Messages;

public abstract class Resource : IResource
{
    private string creator;
    private string name;
    private int priority;

    private bool isTested;
    private bool isApproved;

    protected Resource(string name, string creator, int priority)
    {
        this.Name = name;
        this.Creator = creator;
        this.priority = priority;
        isApproved = false;
        isTested = false;
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

    public bool IsApproved => this.isApproved;
    public bool IsTested => this.isTested;
    public string Creator { get => this.creator; private set => this.name = value; }

    public int Priority { get => this.priority; private set => this.priority = value; }

    public void Approve()
    {
        this.isApproved = true;
    }

    public void Test()
    {
        this.isTested = !this.isTested;
    }

    public override string ToString()
    {
        return $"{this.Name} ({GetType().Name}), Created By: {this.Creator}";
    }
}
