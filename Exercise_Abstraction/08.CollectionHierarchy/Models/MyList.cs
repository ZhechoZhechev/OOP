
namespace CollectionHierarchy.Models
{
    using System.Collections.Generic;

    using Interfaces;

    public class MyList : IList
    {
        List<string> collection;
        public int Used => this.collection.Count;

        public MyList()
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
            string itemValue = this.collection[0];
            this.collection.RemoveAt(0);
            return itemValue;
        }
    }
}
