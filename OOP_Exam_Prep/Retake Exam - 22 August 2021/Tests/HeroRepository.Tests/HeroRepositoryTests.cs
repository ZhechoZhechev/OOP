using System;
using NUnit.Framework;

public class HeroRepositoryTests
{
    private string HeroName = "RicarLudnica";
    private int HeroLevel = 99;

    private Hero hero;
    private HeroRepository repository;

    [SetUp]
    public void SetUp()
    {
        hero = new Hero(HeroName, HeroLevel);
        repository = new HeroRepository();
    }

    [Test]
    public void Test_ForArgumentNullExceptions() 
    {
        Hero hero = null;

        Assert.Throws<ArgumentNullException>(() => repository.Create(hero));
        Assert.Throws<ArgumentNullException>(() => repository.Remove(null));
        Assert.Throws<ArgumentNullException>(() => repository.Remove(""));
    }

    [Test]
    public void Create_ThrowsWhenHeroAlreadyExists()
    {
        repository.Create(hero);

        Assert.Throws<InvalidOperationException>(()=> repository.Create(hero)); 
    }

    [Test]
    public void Create_ReturnsCorrectMessage()
    {
        string actualMSG = repository.Create(hero);
        string expectedMSG = $"Successfully added hero {hero.Name} with level {hero.Level}";

        Assert.AreEqual(expectedMSG, actualMSG);
    }
    [Test]
    public void Reomve_ReturnsTrueIfRemoved() 
    {
        repository.Create(hero);

        Assert.AreEqual(true, repository.Remove(HeroName));
    }
    [Test]
    public void Reomve_ReturnsFalseIfRemoved()
    {
        Assert.AreEqual(false, repository.Remove(HeroName));
    }
    [Test]
    public void GetHeroWithHighestLevel_ReturnsCorrectEntity()
    {
        repository.Create(hero);
        repository.Create(new Hero("Gosho-Picha", 5));

        Hero actual = repository.GetHeroWithHighestLevel();

        Assert.AreEqual(hero, actual);
    }
    [TestCase("RicarLudnica")]
    public void GetHero_ReturnsCorrectEntity(string heroName) 
    {
        repository.Create(hero);

        Assert.AreEqual(hero, repository.GetHero(heroName));
    }
    [TestCase("Ricar")]
    public void GetHero_ReturnsNull(string heroName)
    {
        repository.Create(hero);

        Assert.AreEqual(null, repository.GetHero(heroName));
    }
}