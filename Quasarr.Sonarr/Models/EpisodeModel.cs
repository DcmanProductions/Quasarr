// LFInteractive LLC. - All Rights Reserved

namespace Quasarr.Sonarr.Models;

public struct EpisodeModel
{
    public DateTime AirDate { get; set; }
    public string Description { get; set; }
    public int EpisodeNumber { get; set; }
    public bool IsDownloaded { get; set; }
    public bool Monitored { get; set; }
    public int SeasonNumber { get; set; }
    public string Title { get; set; }
}