namespace InfluencerManagerApp.Repositories;

using InfluencerManagerApp.Models.Contracts;
using InfluencerManagerApp.Repositories.Contracts;

/// <summary>
/// Use the interface
/// </summary>
public class InfluencerRepository : IRepository<IInfluencer>
{
    private readonly List<IInfluencer> models;

    public InfluencerRepository()
    {
        this.models = new List<IInfluencer>();
    }

    public IReadOnlyCollection<IInfluencer> Models => models.AsReadOnly();

    public void AddModel(IInfluencer model)
    {
        models.Add(model);
    }

    public IInfluencer FindByName(string name)
    {
        return models.FirstOrDefault(i => i.Username == name)!;
    }

    public bool RemoveModel(IInfluencer model)
    {
        return models.Remove(model);
    }
}
