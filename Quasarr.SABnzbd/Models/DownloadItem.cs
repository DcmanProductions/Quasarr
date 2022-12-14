// LFInteractive LLC. - All Rights Reserved
namespace Quasarr.SABnzbd.Models;

public readonly struct DownloadItem
{
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

    public long BytesRemaining { get; }
    public string Category { get; }
    public string FileName { get; }
    public int Index { get; }
    public double Percentage { get; }
    public string Status { get; }
    public TimeSpan Timeleft { get; }
    public long TotalSize { get; }
}