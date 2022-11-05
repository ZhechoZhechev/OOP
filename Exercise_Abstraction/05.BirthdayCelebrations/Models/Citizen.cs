namespace BorderControl.Models
{
    using BirthdayCelebrations.Models.Interfaces;
    using Interfaces;
    using System.Linq;

    public class Citizen : IBuyer
    {
        public Citizen()
        {

        }
        public Citizen(int age, string name, string id, string birthDate)
        {
            Age = age;
            Name = name;
            Id = id;
            BirthDate = birthDate;
            Food = 0;
        }

        public int Age { get; private set; }
        public string Name { get; private set; }
        public string Id { get; private set; }
        public string BirthDate { get; private set; }
        public int Food { get; private set; }

        public void BuyFood()
        {
            Food += 10;
        }

        public string CheckIds(string fakeIdsLast)
        {
            if (this.Id.EndsWith(fakeIdsLast)) return this.Id;
            return null;
        }
        public string GetYearFromBD()
        {
            return BirthDate.Split("/").Last();
        }
    }
}
