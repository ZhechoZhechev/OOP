using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Repositories.Contracts;
using System.Collections.Generic;

namespace ChristmasPastryShop.Repositories
{
    public class CocktailRepository : IRepository<ICocktail>
    {
        private List<ICocktail> cocktails;

        public CocktailRepository()
        {
            cocktails = new List<ICocktail>();
        }
        public IReadOnlyCollection<ICocktail> Models => this.cocktails.AsReadOnly();

        public void AddModel(ICocktail model)
        {
            this.cocktails.Add(model);
        }
    }
}
