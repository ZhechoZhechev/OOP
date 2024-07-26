namespace InfluencerManagerApp.Models;

public class BloggerInfluencer : Influencer
{
    private const double ENGAGEMENT_RATE = 2.0;
    private const double FACTOR = 0.2;

    public BloggerInfluencer(string username, int followers)
        : base(username, followers, ENGAGEMENT_RATE)
    {
    }

    public override int CalculateCampaignPrice()
    {
        return (int)(Math.Floor(this.Followers * this.EngagementRate * FACTOR));
    }
}
