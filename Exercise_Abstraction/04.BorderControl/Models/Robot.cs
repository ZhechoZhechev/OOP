
namespace BorderControl.Models
{
    using Interfaces;
    public class Robot : IDentifiable
    {
        public Robot()
        {

        }
        public Robot(string name, string id)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; private set; }

        public string Id { get; private set; }

        public string CheckIds(string fakeIdsLast)
        {
            if (this.Id.EndsWith(fakeIdsLast)) return this.Id;
            return null;
        }
    }
}
