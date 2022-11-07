
namespace CollectionHierarchy.Models
{
    using System.Collections.Generic;

    using Interfaces;
    public class AddRemoveCollection : IRemoveAdd
    {
        private List<string> collection;

        public AddRemoveCollection()
        {
            this.collection = new List<string>();
        }

        public int Add(string item)
        {
            this.collection.Insert(0, item);
            return 0;
        }

        public string Remove()
        {
            int index = this.collection.Count - 1;
            string itemValue = this.collection[index];
            this.collection.RemoveAt(index);
            return itemValue;
        }
    }
}
