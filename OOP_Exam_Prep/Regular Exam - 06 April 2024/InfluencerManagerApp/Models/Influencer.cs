namespace InfluencerManagerApp.Models;

using InfluencerManagerApp.Models.Contracts;
using InfluencerManagerApp.Utilities.Messages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public abstract class Influencer : IInfluencer
{
    private string username;
    private int followers;
    private double engagementRate;
    private double income;
    private readonly List<string> participations;

    protected Influencer(string username, int followers, double engagementRate)
    {
        this.Username = username;
        this.Followers = followers;
        this.EngagementRate = engagementRate;

        this.participations = new List<string>();
        this.Income = 0;
    }

    public string Username
    {
        get => this.username;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(ExceptionMessages.UsernameIsRequired);
            this.username = value;
        }
    }

    public int Followers
    {
        get => this.followers;
        private set
        {
            if (value < 0)
                throw new ArgumentException(ExceptionMessages.FollowersCountNegative);
            this.followers = value;
        }
    }

    public double EngagementRate { get; private set; }

    public double Income { get; private set; }

    public IReadOnlyCollection<string> Participations => this.participations.AsReadOnly();

    public abstract int CalculateCampaignPrice();

    public void EarnFee(double amount)
    {
        this.income += amount;
    }

    public void EndParticipation(string brand)
    {
        this.participations.Remove(brand);
    }

    public void EnrollCampaign(string brand)
    {
        this.participations.Add(brand);
    }

    public override string ToString() 
    {
        return $"{Username} - Followers: {Followers}, Total Income: {Income}";
    }
}
