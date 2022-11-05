namespace BorderControl.Models.Interfaces
{
    public interface IDentifiable
    {
        string Name { get; }
        string Id { get; }

        string CheckIds(string fakeIdsLast);
    }
}
