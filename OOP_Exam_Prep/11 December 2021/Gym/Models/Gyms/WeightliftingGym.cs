namespace Gym.Models.Gyms
{
    public class WeightliftingGym : Gym
    {
        private const int WEIGHTLIFTING_GYM = 20;
        public WeightliftingGym(string name)
            : base(name, WEIGHTLIFTING_GYM)
        {
        }
    }
}
