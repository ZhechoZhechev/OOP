namespace BirthdayCelebrations.Models.Interfaces
{
   public interface IBirthable
    {
        string BirthDate { get; }

        string GetYearFromBD();
    }
}
