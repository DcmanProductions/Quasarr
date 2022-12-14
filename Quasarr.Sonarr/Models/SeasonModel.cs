// LFInteractive LLC. - All Rights Reserved

namespace Quasarr.Sonarr.Models;

public struct SeasonModel
{
    public EpisodeModel[] Episodes { get; set; }
    public bool Monitored { get; set; }
    public double PercentDownloaded { get; set; }
    public int SeasonNumber { get; set; }
    public long Size { get; set; }
}