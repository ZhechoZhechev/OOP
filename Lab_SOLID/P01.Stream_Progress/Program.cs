using System;

namespace P01.Stream_Progress
{
    public class Program
    {
        static void Main()
        {
            IProgressInfo file = new File("File", 20, 1024);
            IProgressInfo music = new Music("Bon Jovi", "Its my life", 40, 1024);

            StreamProgressInfo someInfo = new StreamProgressInfo(file);

            int percent = someInfo.CalculateCurrentPercent();

            Console.WriteLine(percent.ToString());
        }
    }
}
