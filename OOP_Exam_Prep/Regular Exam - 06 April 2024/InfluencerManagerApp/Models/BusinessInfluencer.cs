namespace InfluencerManagerApp.Models;

public class BusinessInfluencer : Influencer
{
    private const double ENGAGEMENT_RATE = 3.0;
    private const double FACTOR = 0.15;

    public BusinessInfluencer(string username, int followers) : base(username, followers, ENGAGEMENT_RATE)
    {
    }

    public override int CalculateCampaignPrice()
    {
        return (int)(Math.Floor(this.Followers * this.EngagementRate * FACTOR));
    }
}
