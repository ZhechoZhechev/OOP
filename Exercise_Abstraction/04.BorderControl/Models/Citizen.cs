
namespace BorderControl.Models
{
    using Interfaces;
    public class Citizen : IDentifiable
    {
        public Citizen()
        {

        }
        public Citizen(int age, string name, string id)
        {
            Age = age;
            Name = name;
            Id = id;
        }

        public int Age { get; private set; }
        public string Name { get; private set; }
        public string Id { get; private set; }

        public string CheckIds(string fakeIdsLast)
        {
            if (this.Id.EndsWith(fakeIdsLast)) return this.Id;
            return null;
        }
    }
}
