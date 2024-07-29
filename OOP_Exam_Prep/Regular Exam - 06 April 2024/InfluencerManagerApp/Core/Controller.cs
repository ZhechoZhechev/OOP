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
    private readonly string[] campaignTypes = new string[] { "ProductCampaign", "ServiceCampaign" };

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
        if (this.influencers.FindByName(username) == null) 
        {
            return string.Format(OutputMessages.InfluencerNotFound, influencers.GetType().Name, username);
        }

        if (this.campaigns.FindByName(brand) == null)
        {
            return string.Format(OutputMessages.CampaignNotFound, brand);
        }
    }

    public string BeginCampaign(string typeName, string brand)
    {
        if (!campaignTypes.Contains(typeName))
        {
            return string.Format(OutputMessages.CampaignTypeIsNotValid, typeName);
        }

        if (this.campaigns.Models.Any(b => b.Brand == brand))
        {
            return string.Format(OutputMessages.CampaignDuplicated, brand);
        }

        ICampaign campaign = null!;
        switch (typeName)
        {
            case "ProductCampaign":
                campaign = new ProductCampaign(brand);
                break;
            case "ServiceCampaign":
                campaign = new ServiceCampaign(brand);
                break;
        }

        this.campaigns.AddModel(campaign);

        return string.Format(OutputMessages.CampaignStartedSuccessfully, brand, typeName);
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
