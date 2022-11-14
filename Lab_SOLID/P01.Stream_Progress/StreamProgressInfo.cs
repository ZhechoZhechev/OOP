namespace P01.Stream_Progress
{
    public class StreamProgressInfo
    {
        private IProgressInfo file;

        // If we want to stream a music file, we can't
        public StreamProgressInfo(IProgressInfo file)
        {
            this.file = file;
        }

        public int CalculateCurrentPercent()
        {
            return (this.file.BytesSent * 100) / this.file.Length;
        }
    }
}
