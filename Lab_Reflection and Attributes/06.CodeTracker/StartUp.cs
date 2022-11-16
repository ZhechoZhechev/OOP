using System;

namespace AuthorProblem
{
    public class StartUp
    {
        [Author("John")]
        public static void Main()
        {
            Tracker tracker = new Tracker();
            tracker.PrintMethodsByAuthor();
        }

        [Author("Jane")]
        public void Test() { }
    }
}
