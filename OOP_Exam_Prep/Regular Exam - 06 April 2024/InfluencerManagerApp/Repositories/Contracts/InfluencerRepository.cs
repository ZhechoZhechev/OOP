using InfluencerManagerApp.Models;

namespace InfluencerManagerApp.Repositories.Contracts;

public class InfluencerRepository : IRepository<Influencer>
{
    private readonly List<Influencer> models;

    public IReadOnlyCollection<Influencer> Models => this.models.AsReadOnly();

    public void AddModel(Influencer model)
    {
        this.models.Add(model);
    }

    public Influencer FindByName(string name)
    {
        return this.models.FirstOrDefault(i => i.Username == name)!;
    }

    public bool RemoveModel(Influencer model)
    {
         return this.models.Remove(model);
    }
}
