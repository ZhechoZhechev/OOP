namespace RecourceCloud.Tests;

public class Tests
{
    private DepartmentCloud departmentCloud;

    [SetUp]
    public void Setup()
    {
        departmentCloud = new DepartmentCloud();
    }

    [Test]
    public void LogTaskAddTasksCorectly()
    {
        string[] args = { "1", "bug", "huge hard bug" };

        string result =  this.departmentCloud.LogTask(args);

        Assert.That(result, Is.EqualTo("Task logged successfully."));
        Assert.That(departmentCloud.Tasks.Count, Is.EqualTo(1));
    }
}