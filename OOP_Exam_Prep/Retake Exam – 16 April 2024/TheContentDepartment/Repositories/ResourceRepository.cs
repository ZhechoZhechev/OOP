namespace TheContentDepartment.Repositories;

using System.Collections.Generic;

using Models.Contracts;
using Repositories.Contracts;

public class ResourceRepository : IRepository<IResource>
{
    private readonly List<IResource> models = new List<IResource>();

    public IReadOnlyCollection<IResource> Models => this.models.AsReadOnly();

    public void Add(IResource model)
    {
        this.models.Add(model);
    }

    public IResource TakeOne(string modelName)
    {
        return this.models.FirstOrDefault(x => x.Name == modelName);
    }
}
