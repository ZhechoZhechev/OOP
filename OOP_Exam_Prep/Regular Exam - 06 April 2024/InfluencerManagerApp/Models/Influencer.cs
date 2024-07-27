using InfluencerManagerApp.Models.Contracts;

namespace InfluencerManagerApp.Models
{
    public abstract class Influencer : IInfluencer
    {
        private string username;
        private int followers;
        private double engagementRate;
        private double income;
        private readonly List<string> participations;

        protected Influencer(string username, int followers, double engagmentRate)
        {
            Username = username;
            Followers = followers;
            EngagementRate = engagmentRate;

            Income = 0;
            participations = new List<string>();
        }

        public string Username
        {
            get => username;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"Username is required.");
                }
                username = value;
            }
        }

        public int Followers
        {
            get => followers;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Followers count cannot be a negative number.");
                }
                followers = value;
            }
        }

        public double EngagementRate
        {
            get => engagementRate;
            private set
            {
                engagementRate = value;
            }
        }

        public double Income
        {
            get => income;
            private set
            {
                income = value;
            }
        }

        public IReadOnlyCollection<string> Participations => participations;

        public void EarnFee(double amount) => income += amount;

        public void EnrollCampaign(string brand) => participations.Add(brand);

        public abstract int CalculateCampaignPrice();

        public override string ToString() => $"{Username} - Followers: {Followers}, Total Income: {Income}";

        public void EndParticipation(string brand)
        {
            participations.Remove(brand);
        }
    }
}
