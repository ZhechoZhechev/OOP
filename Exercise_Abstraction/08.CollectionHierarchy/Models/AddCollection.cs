
namespace CollectionHierarchy.Models
{
    using Interfaces;
    using System.Collections.Generic;

    public class AddCollection : IAdd
    {
        private List<string> collection;
        public AddCollection()
        {
            collection = new List<string>();
        }
        public int Add(string item)
        {
            this.collection.Add(item);
            return this.collection.Count - 1;
        }
    }
}
