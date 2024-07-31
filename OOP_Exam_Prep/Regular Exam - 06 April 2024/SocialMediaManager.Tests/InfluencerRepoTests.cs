namespace SocialMediaManager.Tests;

public class Tests
{
    private InfluencerRepository inflRepository;

    [SetUp]
    public void Setup()
    {
        inflRepository = new InfluencerRepository();
    }

    [Test]
    public void RegisterInfluencerThrowsIfNull()
    {
        Influencer influencer = null;

        var msg = Assert.Throws <ArgumentNullException>(() => this.inflRepository.RegisterInfluencer(influencer));

        Assert.That(msg.Message, Does.Contain("Influencer is null"));
        Assert.That(msg.ParamName, Is.EqualTo(nameof(influencer)));
    }
}