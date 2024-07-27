namespace InfluencerManagerApp.Repositories;

using InfluencerManagerApp.Models;
using InfluencerManagerApp.Repositories.Contracts;

public class InfluencerRepository : IRepository<Influencer>
{
    private readonly List<Influencer> models;

    public IReadOnlyCollection<Influencer> Models => models.AsReadOnly();

    public void AddModel(Influencer model)
    {
        models.Add(model);
    }

    public Influencer FindByName(string name)
    {
        return models.FirstOrDefault(i => i.Username == name)!;
    }

    public bool RemoveModel(Influencer model)
    {
        return models.Remove(model);
    }
}
