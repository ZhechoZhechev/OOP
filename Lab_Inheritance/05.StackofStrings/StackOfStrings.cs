using System;
using System.Collections.Generic;
using System.Text;

namespace CustomStack
{
    public class StackOfStrings : Stack<string>
    {
        public bool IsEmpty() 
        {
            return Count == 0;
        }
        public Stack<string> AddRange(IEnumerable<string> range) 
        {
            //Stack<string> toReturn = new Stack<string>(); 
            foreach (var item in range)
            {
                Push(item);
            }
            return this;
        }
    }
}
