namespace SocialMediaManager.Tests;

using System;
using NUnit.Framework;
using System.Linq;
public class Tests
{
    private InfluencerRepository inflRepository;
    private Influencer influencer;
    private Influencer influencer1;

    [SetUp]
    public void Setup()
    {
        inflRepository = new InfluencerRepository();
        influencer = new Influencer("Test", 1);
        influencer1 = new Influencer("Test1", 100);
    }

    [Test]
    public void RegisterInfluencerThrowsIfNull()
    {
        Influencer influencer = null;

        var msg = Assert.Throws<ArgumentNullException>(() => this.inflRepository.RegisterInfluencer(influencer));

        Assert.That(msg.Message, Does.Contain("Influencer is null"));
        Assert.That(msg.ParamName, Is.EqualTo(nameof(influencer)));
    }

    [Test]
    public void RegisterInfluencerThrowsIfNameExists()
    {
        inflRepository.RegisterInfluencer(influencer);

        var msg = Assert.Throws<InvalidOperationException>(() => this.inflRepository.RegisterInfluencer(influencer));

        Assert.That(msg.Message, Is.EqualTo($"Influencer with username {influencer.Username} already exists"));
    }

    [Test]
    public void RegisterInfluencerReturnsCorrectMsgIfSucceeded()
    {
        var msg = this.inflRepository.RegisterInfluencer(influencer);

        Assert.Multiple(() =>
        {
            Assert.That(this.inflRepository.Influencers.Count > 0);
            Assert.That(msg, Is.EqualTo($"Successfully added influencer {influencer.Username} with {influencer.Followers}"));
        });
    }

    [TestCase(null)]
    [TestCase("")]
    public void RemoveInfluencerThrowsIfNameNullOrWhiteSpace(string username)
    {
        var msg = Assert.Throws<ArgumentNullException>(() => this.inflRepository.RemoveInfluencer(username));

        Assert.Multiple(() =>
        {
            Assert.That(msg.ParamName, Is.EqualTo(nameof(username)));
            Assert.That(msg.Message, Does.Contain("Username cannot be null"));
        });
    }

    [Test]

    public void RemoveInfluencerWorksAsIntended()
    {
        this.inflRepository.RegisterInfluencer(influencer);

        var result = this.inflRepository.RemoveInfluencer(influencer.Username);

        Assert.Multiple(() =>
        {
            Assert.That(this.inflRepository.Influencers.Count == 0);
            Assert.That(result, Is.True);
        });
    }

    [Test]
    public void GetInfluencerWithMostFollowersReturnCorrectInfluencer()
    {
        this.inflRepository.RegisterInfluencer(influencer);
        this.inflRepository.RegisterInfluencer(influencer1);

        var result = this.inflRepository.GetInfluencerWithMostFollowers();

        Assert.Multiple(() =>
        {
            Assert.That(result.Username, Is.EqualTo(influencer1.Username));
            Assert.That(result.Followers, Is.EqualTo(influencer1.Followers));
        });
    }

    [Test]
    public void GetInfluencerReturnsCorrectInfluencer() 
    {
        this.inflRepository.RegisterInfluencer(influencer);

        var result = this.inflRepository.GetInfluencer(influencer.Username);

        Assert.Multiple(() => 
        {
            Assert.That(result.Username, Is.EqualTo(influencer.Username));
            Assert.That(result.Followers, Is.EqualTo(influencer.Followers));
        });
    }
    [Test]
    public void GetInfluencerReturnsNull()
    {

        var result = this.inflRepository.GetInfluencer(influencer.Username);

        Assert.That(result, Is.Null);
    }
}