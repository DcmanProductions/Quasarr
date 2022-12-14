// LFInteractive LLC. - All Rights Reserved

namespace Quasarr.Sonarr.Models;

public struct SeriesModel
{
    public DateTime Added { get; set; }
    public string BackgroundImage { get; set; }
    public string BannerImage { get; set; }
    public string Certification { get; set; }
    public string Description { get; set; }
    public bool Ended { get; set; }
    public DateTime FirstAirDate { get; set; }
    public string IMDB_ID { get; set; }
    public DateTime LastAirDate { get; set; }
    public bool Monitored { get; set; }
    public string Network { get; set; }
    public string Path { get; set; }
    public double PercentDownloaded { get; set; }
    public string PosterImage { get; set; }
    public SeasonModel[] Seasons { get; set; }
    public long Size { get; set; }
    public string Status { get; set; }
    public string Title { get; set; }
    public string TMDB_ID { get; set; }
    public int Year { get; set; }
}