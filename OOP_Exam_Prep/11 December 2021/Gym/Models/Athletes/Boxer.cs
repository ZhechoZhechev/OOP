using Gym.Utilities.Messages;
using System;

namespace Gym.Models.Athletes
{
    public class Boxer : Athlete
    {
        private const int BOXER_STAMINA = 60;
        public Boxer(string fullName, string motivation, int numberOfMedals)
            : base(fullName, motivation, numberOfMedals, BOXER_STAMINA)
        {
        }

        public override void Exercise()
        {
            Stamina += 15;

            if (Stamina > 100)
            {
                Stamina = 100;
                throw new ArgumentException(ExceptionMessages.InvalidStamina);
            }
        }
    }
}
