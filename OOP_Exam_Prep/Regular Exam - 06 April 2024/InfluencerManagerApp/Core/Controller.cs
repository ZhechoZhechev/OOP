﻿namespace InfluencerManagerApp.Core;

using Repositories;
using Core.Contracts;
using Models.Contracts;
using Repositories.Contracts;
using InfluencerManagerApp.Utilities.Messages;
using InfluencerManagerApp.Models;
using System.Text;

public class Controller : IController
{
    private IRepository<IInfluencer> influencers;
    private IRepository<ICampaign> campaigns;

    private readonly string[] infuencerTypes = new string[] { "BusinessInfluencer", "FashionInfluencer", "BloggerInfluencer" };
    private readonly string[] campaignTypes = new string[] { "ProductCampaign", "ServiceCampaign" };
    private const double budgetTreshold = 10000;
    private const double influncersFeeForSuccessfullCampaign = 2000;

    public Controller()
    {
        this.campaigns = new CampaignRepository();
        this.influencers = new InfluencerRepository();
    }
    public string ApplicationReport()
    {
        var influencersSorted = influencers.Models
            .OrderByDescending(i => i.Income)
            .ThenByDescending(i => i.Followers);

        StringBuilder sb = new StringBuilder();
        foreach (IInfluencer influencer in influencersSorted)
        {
            sb.AppendLine(influencer.ToString());

            if (influencer.Participations.Any())
            {
                var sortedParticipations = influencer.Participations.OrderBy(s => s);
                sb.AppendLine("Active Campaigns:");

                foreach (var campaignName in sortedParticipations)
                {
                    var campaign = campaigns.FindByName(campaignName);
                    sb.AppendLine($"--{campaign.ToString()}");
                }
            }

        }

        return sb.ToString().TrimEnd();
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

        var campaign = this.campaigns.FindByName(brand);
        var influencer = this.influencers.FindByName(username);
        if (campaign.Contributors.Contains(username))
        {
            return string.Format(OutputMessages.InfluencerAlreadyEngaged, username, brand);
        }

        var isEligible = true;
        switch (campaign.GetType().Name)
        {
            case "ProductCampaign":
                if (influencer.GetType().Name == "BloggerInfluencer")
                {
                    isEligible = false;
                }
                break;
            case "ServiceCampaign":
                if (influencer.GetType().Name == "FashionInfluencer")
                {
                    isEligible = false;
                }
                break;
        }
        if (!isEligible)
        {
            return string.Format(OutputMessages.InfluencerNotEligibleForCampaign, username, brand);
        }

        double profit = influencer.CalculateCampaignPrice();
        if (campaign.Budget < profit)
        {
            return string.Format(OutputMessages.UnsufficientBudget, brand, username);
        }

        campaign.Engage(influencer);
        influencer.EnrollCampaign(brand);
        influencer.EarnFee(profit);

        return string.Format(OutputMessages.InfluencerAttractedSuccessfully, username, brand);
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
        ICampaign campaign = this.campaigns.FindByName(brand);
        if (campaign == null)
        {
            return OutputMessages.InvalidCampaignToClose;
        }

        if (campaign.Budget <= budgetTreshold)
        {
            return string.Format(OutputMessages.CampaignCannotBeClosed, brand);
        }

        foreach (string influencer in campaign.Contributors)
        {
            IInfluencer curInfluencer = this.influencers.FindByName(influencer);
            curInfluencer.EarnFee(influncersFeeForSuccessfullCampaign);
            curInfluencer.EndParticipation(brand);
        }

        this.campaigns.RemoveModel(campaign);
        return string.Format(OutputMessages.CampaignClosedSuccessfully, brand);
    }

    public string ConcludeAppContract(string username)
    {
        IInfluencer influencer = this.influencers.FindByName(username);
        if (influencer == null)
        {
            return string.Format(OutputMessages.InfluencerNotSigned, username);
        }

        if (influencer.Participations.Any())
        {
            return string.Format(OutputMessages.InfluencerHasActiveParticipations, username);
        }

        this.influencers.RemoveModel(influencer);
        return string.Format(OutputMessages.ContractConcludedSuccessfully, username);
    }

    public string FundCampaign(string brand, double amount)
    {
        if (!this.campaigns.Models.Any(b => b.Brand == brand))
        {
            return OutputMessages.InvalidCampaignToFund;
        }

        if (amount <= 0)
        {
            return OutputMessages.NotPositiveFundingAmount;
        }

        ICampaign campaign = this.campaigns.FindByName(brand);
        campaign.Gain(amount);
        return string.Format(OutputMessages.CampaignFundedSuccessfully, brand, amount);
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
