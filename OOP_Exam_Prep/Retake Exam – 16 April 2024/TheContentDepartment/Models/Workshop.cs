namespace TheContentDepartment.Models;

public class Workshop : Resource
{
    private const int WORKSHOP_PRIORITY = 2;
    public Workshop(string name, string creator) : base(name, creator, WORKSHOP_PRIORITY)
    {
    }
}
