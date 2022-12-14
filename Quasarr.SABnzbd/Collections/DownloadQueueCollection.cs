// LFInteractive LLC. - All Rights Reserved
using Newtonsoft.Json.Linq;
using Quasarr.SABnzbd.Models;

namespace Quasarr.SABnzbd.Collections;

public sealed class DownloadQueueCollection
{
    private DownloadQueueCollection(string status, bool isPaused, long bytesPerSecond, long totalSize, long bytesRemaining, double speedLimit, TimeSpan timeLeft, DownloadItemModel[] downloads)
    {
        Status = status;
        IsPaused = isPaused;
        BytesPerSecond = bytesPerSecond;
        SpeedLimit = speedLimit;
        TimeLeft = timeLeft;
        Downloads = downloads;
        TotalSize = totalSize;
        BytesRemaining = bytesRemaining;
    }

    public long BytesPerSecond { get; }
    public long BytesRemaining { get; }
    public DownloadItemModel[] Downloads { get; }
    public bool IsPaused { get; }
    public double SpeedLimit { get; }
    public string Status { get; }
    public TimeSpan TimeLeft { get; }
    public long TotalSize { get; }

    public static DownloadQueueCollection? Poll()
    {
        if (!string.IsNullOrWhiteSpace(Config.Instance.API) && !string.IsNullOrWhiteSpace(Config.Instance.Host))
        {
            using HttpClient client = new();
            using HttpResponseMessage response = client.GetAsync($"http://{Config.Instance.Host}:{Config.Instance.Port}/sabnzbd/api?output=json&apikey={Config.Instance.API}&mode=queue").Result;
            if (response.IsSuccessStatusCode)
            {
                JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                string status = json["status"]?.ToObject<string>() ?? "";
                bool isPaused = json["paused"]?.ToObject<bool>() ?? false;
                long bytesPerSecond = (long)(Convert.ToDouble(json["kbpersec"]?.ToObject<string>() ?? "1") / 1024);
                long totalSize = (long)(Convert.ToDouble(json["mb"]?.ToObject<string>() ?? "1") / 1024 / 1024);
                long bytesRemaining = (long)(Convert.ToDouble(json["mbleft"]?.ToObject<string>() ?? "1") / 1024 / 1024);
                double speedLimit = Convert.ToDouble(json["speedlimit_abs"]?.ToObject<string>() ?? "0");
                TimeSpan timeLeft = json["timeleft"]?.ToObject<TimeSpan>() ?? TimeSpan.Zero;
                List<DownloadItemModel> downloads = new();

                if (json["slots"] != null)
                {
                    foreach (JObject item in json["slots"].Cast<JObject>())
                    {
                        int index = item["index"]?.ToObject<int>() ?? 0;
                        string s = item["status"]?.ToObject<string>() ?? "";
                        string filename = item["filename"]?.ToObject<string>() ?? "";
                        string category = item["cat"]?.ToObject<string>() ?? "";
                        double percentage = item["percentage"]?.ToObject<double>() ?? 0d;
                        TimeSpan tl = item["timeleft"]?.ToObject<TimeSpan>() ?? TimeSpan.Zero;
                        long t = (long)(Convert.ToDouble(item["mb"]?.ToObject<string>() ?? "1") / 1024 / 1024);
                        long r = (long)(Convert.ToDouble(item["mbleft"]?.ToObject<string>() ?? "1") / 1024 / 1024);

                        downloads.Add(new(index, s, filename, category, tl, percentage, t, r));
                    }
                }

                return new(status, isPaused, bytesPerSecond, totalSize, bytesRemaining, speedLimit, timeLeft, downloads.ToArray());
            }
        }
        return null;
    }
}