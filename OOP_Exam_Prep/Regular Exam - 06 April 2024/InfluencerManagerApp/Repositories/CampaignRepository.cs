namespace InfluencerManagerApp.Repositories;

using InfluencerManagerApp.Models;
using InfluencerManagerApp.Repositories.Contracts;

public class CampaignRepository : IRepository<Campaign>
{
    private readonly List<Campaign> models;
    public IReadOnlyCollection<Campaign> Models => this.models.AsReadOnly();

    public void AddModel(Campaign model)
    {
        this.models.Add(model);
    }

    public Campaign FindByName(string name)
    {
        return this.models.FirstOrDefault(i => i.Brand == name)!;
    }

    public bool RemoveModel(Campaign model)
    {
        return this.models.Remove(model);
    }
}
