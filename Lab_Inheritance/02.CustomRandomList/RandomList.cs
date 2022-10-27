using System;
using System.Collections.Generic;
using System.Text;

namespace CustomRandomList
{
    public class RandomList : List<string>
    {
        public Random NewRandom { get; set; }
        public RandomList()
        {
            NewRandom = new Random();
        }
        public string RandomString()
        {
            int index = NewRandom.Next(0, Count + 1);
            string str = this[index];
            return str;
        }
    }
}
