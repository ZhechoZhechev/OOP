
namespace AquaShop.Core
{
    using System;
    using System.Collections.Generic;

    using Repositories;
    using Contracts;
    using Models.Aquariums.Contracts;
    using Models.Aquariums;
    using Models.Decorations.Contracts;
    using Models.Decorations;
    using Models.Fish.Contracts;
    using Models.Fish;
    using System.Linq;

    public class Controller : IController
    {
        private DecorationRepository decorations;
        private List<IAquarium> aquariums;

        public Controller()
        {
            this.decorations = new DecorationRepository();
            this.aquariums = new List<IAquarium>();
        }
        public string AddAquarium(string aquariumType, string aquariumName)
        {

            IAquarium aquarium;

            switch (aquariumType)
            {
                case
                    "FreshwaterAquarium":
                    aquarium = new FreshwaterAquarium(aquariumName);
                    break;
                case "SaltwaterAquarium":
                    aquarium = new SaltwaterAquarium(aquariumName);
                    break;
                default: throw new InvalidOperationException("Invalid aquarium type.");
            }

            this.aquariums.Add(aquarium);
            return $"Successfully added {aquarium.GetType().Name}.";
        }

        public string AddDecoration(string decorationType)
        {
            IDecoration decoration;
            switch (decorationType)
            {
                case "Ornament": decoration = new Ornament();
                    break;
                case "Plant": decoration = new Plant();
                    break;
                default: throw new InvalidOperationException("Invalid decoration type.");
            }

            decorations.Add(decoration);
            return $"Successfully added {decoration.GetType().Name}.";
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            if (fishType != "FreshwaterFish" && fishType != "SaltwaterFish")
            {
                throw new InvalidOperationException("Invalid fish type.");
            }

            var aquariumToAdFish = aquariums.Find(x => x.Name == aquariumName);

            IFish fish;
            if (fishType == "FreshwaterFish")
            {
                fish = new FreshwaterFish(fishName, fishSpecies, price);

                if (aquariumToAdFish.GetType() != typeof(FreshwaterAquarium))
                    return "Water not suitable.";
            }
            else
            {
                fish = new SaltwaterFish(fishName, fishSpecies, price);

                if (aquariumToAdFish.GetType() != typeof(SaltwaterAquarium))
                    return "Water not suitable.";
            }
            
            aquariumToAdFish.AddFish(fish);

            return $"Successfully added {fishType} to {aquariumName}.";
        }

        public string CalculateValue(string aquariumName)
        {
            var aquarium = aquariums.Find(x => x.Name == aquariumName);
            var fishValue = aquarium.Fish.Select(x => x.Price).Sum();
            var decorationsValue = aquarium.Decorations.Select(x => x.Price).Sum();

            var totalValue = fishValue + decorationsValue;

            return $"The value of Aquarium {aquariumName} is {totalValue:f2}.";
        }

        public string FeedFish(string aquariumName)
        {
            var aquarium = aquariums.Find(x => x.Name == aquariumName);
            aquarium.Feed();
            return $"Fish fed: {aquarium.Fish.Count}";
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            if (this.decorations.FindByType(decorationType)== null)
            {
                throw new InvalidOperationException($"There isn't a decoration of type {decorationType}.");
            }

            var decToAdd = this.decorations.FindByType(decorationType);
            var aqToAddDecoration = aquariums.Find(x => x.Name == aquariumName);

            aqToAddDecoration.AddDecoration(decToAdd);
            this.decorations.Remove(decToAdd);

            return $"Successfully added {decorationType} to {aquariumName}.";
        }

        public string Report()
        {
          return string.Join(Environment.NewLine, aquariums.Select(x => x.GetInfo()));
        }
    }
}
