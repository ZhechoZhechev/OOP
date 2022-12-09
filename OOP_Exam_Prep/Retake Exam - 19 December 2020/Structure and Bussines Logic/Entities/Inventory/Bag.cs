using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WarCroft.Constants;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Inventory
{
    public abstract class Bag : IBag
    {
        private List<Item> items;

        public Bag(int capacity)
        {
            Capacity = capacity;
            this.items = new List<Item>();
        }
        public int Capacity { get; set; }

        public int Load => Items.Sum(x => x.Weight);

        public IReadOnlyCollection<Item> Items => this.items.AsReadOnly();

        public void AddItem(Item item)
        {
            if (Load + item.Weight > Capacity)
                throw new InvalidOperationException
                    (ExceptionMessages.ExceedMaximumBagCapacity);

            this.items.Add(item);
        }

        public Item GetItem(string name)
        {
            if (this.Items.Count == 0)
                throw new InvalidOperationException(ExceptionMessages.EmptyBag);

            var itemToGet = this.items.Find(x => x.GetType().Name == name);
            if (itemToGet == null)
                throw new ArgumentException(string.Format(ExceptionMessages.ItemNotFoundInBag, name));

            this.items.Remove(itemToGet);
            return itemToGet;
        }
    }
}
