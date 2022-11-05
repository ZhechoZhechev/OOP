namespace BirthdayCelebrations.Models
{
    using Interfaces;
    using System.Linq;

    class Pet : IBirthable
    {
        public Pet(string birthDate, string name)
        {
            BirthDate = birthDate;
            Name = name;
        }

        public string BirthDate { get; private set; }
        public string Name { get; set; }

        public string GetYearFromBD() 
        {
            return BirthDate.Split("/").Last();
        }
    }
}
