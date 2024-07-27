namespace InfluencerManagerApp.Models;

public class ServiceCampaign : Campaign
{
    private const double BUDGET = 30000;

    public ServiceCampaign(string brand)
        : base(brand, BUDGET)
    {
    }
}
