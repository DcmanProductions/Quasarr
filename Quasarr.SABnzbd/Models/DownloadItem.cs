namespace Quasarr.SABnzbd.Models;

public readonly struct DownloadItem
{
    public int Index { get; }
    public string Status { get; }
    public string FileName { get; }
    public string Category { get; }
    public TimeSpan Timeleft { get; }
    public double Percentage { get; }
    public long TotalSize { get; }
    public long BytesRemaining { get; }
    internal DownloadItem(int index, string status, string filename, string category, TimeSpan timeLeft, double percentage, long totalSize, long bytesRemaining)
    {
        Index = index;
        Status = status;
        FileName = filename;
        Category = category;
        Timeleft = timeLeft;
        Percentage = percentage;
        TotalSize = totalSize;
        BytesRemaining = bytesRemaining;
    }
}
