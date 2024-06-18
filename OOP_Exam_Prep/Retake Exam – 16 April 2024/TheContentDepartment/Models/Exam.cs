namespace TheContentDepartment.Models;

public class Exam : Resource
{
    private const int EXAM_PRIORITY = 1;
    public Exam(string name, string creator) : base(name, creator, EXAM_PRIORITY)
    {
    }
}
