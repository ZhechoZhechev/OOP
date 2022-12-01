
namespace CarRacing.Models.Maps
{
    using Racers.Contracts;
    using Utilities.Messages;
    using Contracts;

    public class Map : IMap
    {
        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {
            if (!racerOne.IsAvailable() && !racerTwo.IsAvailable())
                return OutputMessages.RaceCannotBeCompleted;
            else if (!racerOne.IsAvailable())
                return string.Format
                    (OutputMessages.OneRacerIsNotAvailable, racerTwo.Username, racerOne.Username);
            else if(!racerTwo.IsAvailable())
                return string.Format
                    (OutputMessages.OneRacerIsNotAvailable, racerOne.Username, racerTwo.Username);

            racerOne.Race();
            racerTwo.Race();

            IRacer winner = GetRacerScore(racerOne) > GetRacerScore(racerTwo) ? racerOne : racerTwo;

            return string.Format
                (OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username, winner.Username);

        }
        private double GetRacerScore(IRacer racer) 
        {
            double behaviourMultiplier = racer.RacingBehavior == "strict" ? 1.2 : 1.1;

            return racer.Car.HorsePower * racer.DrivingExperience * behaviourMultiplier;
        }
    }
}
