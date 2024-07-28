namespace InfluencerManagerApp.Core;

using Repositories;
using Core.Contracts;
using Models.Contracts;
using Repositories.Contracts;
using InfluencerManagerApp.Utilities.Messages;
using InfluencerManagerApp.Models;

public class Controller : IController
{
    private IRepository<IInfluencer> influencers;
    private IRepository<ICampaign> campaigns;

    private readonly string[] infuencerTypes = new string[] { "BusinessInfluencer", "FashionInfluencer", "BloggerInfluencer" }; 

    public Controller()
    {
        this.campaigns = new CampaignRepository();
        this.influencers = new InfluencerRepository();
    }
    public string ApplicationReport()
    {
        throw new NotImplementedException();
    }

    public string AttractInfluencer(string brand, string username)
    {
        throw new NotImplementedException();
    }

    public string BeginCampaign(string typeName, string brand)
    {
        throw new NotImplementedException();
    }

    public string CloseCampaign(string brand)
    {
        throw new NotImplementedException();
    }

    public string ConcludeAppContract(string username)
    {
        throw new NotImplementedException();
    }

    public string FundCampaign(string brand, double amount)
    {
        throw new NotImplementedException();
    }

    public string RegisterInfluencer(string typeName, string username, int followers)
    {
        if (!infuencerTypes.Contains(typeName))
        {
            return string.Format(OutputMessages.InfluencerInvalidType, typeName);
        }

        if (this.influencers.Models.Any(i => i.Username == username))
        {
            return string.Format(OutputMessages.UsernameIsRegistered, username, this.influencers.GetType().Name);
        }

        IInfluencer influencer = null!;
        switch (typeName)
        {
            case "BusinessInfluencer": 
                influencer = new BusinessInfluencer(username, followers);
                break;
            case "FashionInfluencer":
                influencer = new FashionInfluencer(username, followers);
                break;
            case "BloggerInfluencer":
                influencer = new BloggerInfluencer(username, followers);
                break;
        }
        
        this.influencers.AddModel(influencer);

        return string.Format(OutputMessages.InfluencerRegisteredSuccessfully, username);
    }
}
