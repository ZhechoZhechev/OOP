using InfluencerManagerApp.Models.Contracts;
using InfluencerManagerApp.Utilities.Messages;

namespace InfluencerManagerApp.Models;

public abstract class Campaign : ICampaign
{
    private string brand;
    private double budget;
    private readonly List<string> contributors;

    protected Campaign(string brand, double budget)
    {
        this.Brand = brand;
        this.budget = budget;
        this.contributors = new List<string>();
    }
    public string Brand
    {
        get => this.brand;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(ExceptionMessages.BrandIsrequired);

            this.brand = value;
        }
    }

    public double Budget { get; private set; }

    public IReadOnlyCollection<string> Contributors => this.contributors.AsReadOnly();

    public void Engage(IInfluencer influencer)
    {
        this.contributors.Add(influencer.Username);
        var campaignPrice = influencer.CalculateCampaignPrice();
        this.Budget -= campaignPrice;
    }

    public void Gain(double amount)
    {
        this.Budget += amount;
    }

    public override string ToString()
    {
        return $"{this.GetType().Name} - Brand: {Brand}, Budget: {Budget}, Contributors: {this.contributors.Count}";
    }
}
