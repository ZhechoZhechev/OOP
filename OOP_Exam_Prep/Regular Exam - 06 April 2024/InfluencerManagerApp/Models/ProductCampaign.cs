namespace InfluencerManagerApp.Models
{
    public class ProductCampaign : Campaign
    {
        private const double BUDGET = 60000;
        public ProductCampaign(string brand)
            : base(brand, BUDGET)
        {
        }
    }
}
