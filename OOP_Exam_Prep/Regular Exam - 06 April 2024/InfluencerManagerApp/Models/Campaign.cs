using InfluencerManagerApp.Models.Contracts;

namespace InfluencerManagerApp.Models
{
    public abstract class Campaign : ICampaign
    {
        private string brand;
        private double budget;
        private readonly List<string> contributors;


        public Campaign(string brand, double budget)
        {
            Brand = brand;
            Budget = budget;

            contributors = new List<string>();
        }

        public string Brand
        {
            get => brand;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Brand is required.");
                }
                brand = value;
            }
        }

        public double Budget
        {
            get => budget;
            private set
            {
                budget = value;
            }
        }

        public IReadOnlyCollection<string> Contributors => contributors;

        public void Engage(IInfluencer influencer)
        {
            contributors.Add(influencer.Username);
            budget -= influencer.CalculateCampaignPrice();
        }

        public void Gain(double amount)
        {
            budget += amount;
        }

        public override string ToString()
        {
            return $"{GetType().Name} - Brand: {Brand}, Budget: {Budget}, Contributors: {Contributors.Count}";
        }
    }
}
