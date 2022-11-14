namespace P01.Stream_Progress
{
    public interface IProgressInfo
    {
        int Length { get; }
        int BytesSent { get; }
    }
}
