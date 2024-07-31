namespace SocialMediaManager.Tests;

public class Tests
{
    private InfluencerRepository inflRepository;
    private Influencer influencer;

    [SetUp]
    public void Setup()
    {
        inflRepository = new InfluencerRepository();
        influencer = new Influencer("Test", 1);
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
}