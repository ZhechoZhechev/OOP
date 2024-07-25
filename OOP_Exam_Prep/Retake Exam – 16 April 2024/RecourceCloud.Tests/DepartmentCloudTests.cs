using System.Threading.Tasks;

namespace RecourceCloud.Tests;

public class Tests
{
    private readonly string[] args = new string[] { "1", "bug", "huge hard bug" };

    private DepartmentCloud departmentCloud;

    [SetUp]
    public void Setup()
    {
        departmentCloud = new DepartmentCloud();
    }

    [Test]
    public void LogTaskAddTasksCorectly()
    {
        string result = this.departmentCloud.LogTask(args);

        Assert.That(result, Is.EqualTo("Task logged successfully."));
        Assert.That(departmentCloud.Tasks.Count, Is.EqualTo(1));
    }

    [TestCase("invalid", "input")]
    [TestCase("invalid", "input", "four", "args")]
    public void LogTaskThrowsIfArgsNotCorrectCount(params string[] args)
    {
        var exception = Assert.Throws<ArgumentException> (() => this.departmentCloud.LogTask(args));

        Assert.That(exception.Message, Is.EqualTo("All arguments are required."));
    }

    [TestCase(null, "valid", "input")]
    [TestCase("valid", null, "input")]
    [TestCase("valid", "input", null)]
    public void LogTaskThrowsIfAnyArgumetIsNull(params string[] args) 
    {
        var exception = Assert.Throws<ArgumentException>(() => this.departmentCloud.LogTask(args));

        Assert.That(exception.Message, Is.EqualTo("Arguments values cannot be null."));
    }

    [Test]
    public void LogTaskDoesNotAllowToAddTasksWithSameTaskName()
    {
        string[] args = { "1", "bug", "huge hard bug" };

        departmentCloud.LogTask(args);
        string message = departmentCloud.LogTask(args);

        Assert.That(message, Is.EqualTo($"{args[2]} is already logged."));
    }

    [Test]
    public void CreateResourceWithNoTasksReturnsFalse() 
    {
        bool result = departmentCloud.CreateResource();

        Assert.That(result, Is.False);
    }

    [Test]
    public void CreateResourceSuccessfully() 
    {
        this.departmentCloud.LogTask(args);
        bool result = this.departmentCloud.CreateResource();

        Assert.That(this.departmentCloud.Resources.Count, Is.GreaterThan(0));
        Assert.That(this.departmentCloud.Tasks, Is.Empty);
        Assert.That(result, Is.True);
    }

    [Test]
    public void TestResourceReturnsNullIfNotSuchResourceName()
    {
        var result = this.departmentCloud.TestResource("random");

        Assert.That(result, Is.Null);
    }
}