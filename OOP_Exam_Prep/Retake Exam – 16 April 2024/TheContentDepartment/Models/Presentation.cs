namespace TheContentDepartment.Models;

public class Presentation : Resource
{
    private const int PRESENTATION_PRIORITY = 3;
    public Presentation(string name, string creator) : base(name, creator, PRESENTATION_PRIORITY)
    {
    }
}
