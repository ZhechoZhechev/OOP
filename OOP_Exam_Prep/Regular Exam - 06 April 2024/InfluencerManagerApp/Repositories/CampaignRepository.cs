using InfluencerManagerApp.Models.Contracts;
using InfluencerManagerApp.Repositories.Contracts;

namespace InfluencerManagerApp.Repositories
{
    public class CampaignRepository : IRepository<ICampaign>
    {
        private readonly List<ICampaign> models;

        public CampaignRepository()
        {
            models = new List<ICampaign>();
        }

        public IReadOnlyCollection<ICampaign> Models => models;

        public void AddModel(ICampaign model)
        {
            models.Add(model);
        }

        public bool RemoveModel(ICampaign model)
        {
            return models.Remove(model);
        }

        public ICampaign FindByName(string brand)
        {
            return models.FirstOrDefault(m => m.Brand == brand);
        }
    }
}