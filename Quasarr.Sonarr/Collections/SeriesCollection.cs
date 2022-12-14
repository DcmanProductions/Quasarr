// LFInteractive LLC. - All Rights Reserved
using Newtonsoft.Json.Linq;
using Quasarr.Sonarr.Models;
using Quasarr.Sonarr.Utilities;

namespace Quasarr.Sonarr.Collections;

public sealed class SeriesCollection
{
    private readonly SeriesModel[] _series;

    private SeriesCollection(SeriesModel[] series)
    {
        _series = series;
    }

    public static SeriesCollection Poll()
    {
        List<SeriesModel> series = new();
        if (Connection.IsValidConfig())
        {
            using HttpResponseMessage response = Connection.MakeRequest("/series?includeSeasonImages=true");
            if (response.IsSuccessStatusCode)
            {
                JArray json = JArray.Parse(response.Content.ReadAsStringAsync().Result);
                Parallel.ForEach(json, item =>
                {
                    int seriesId = item["id"]?.ToObject<int>() ?? 0;

                    string bg = Connection.GetUrl($"/MediaCover/{seriesId}/fanart.jpg?apikey={Config.Instance.API}");
                    string poster = Connection.GetUrl($"/MediaCover/{seriesId}/poster.jpg?apikey={Config.Instance.API}");
                    string banner = Connection.GetUrl($"/MediaCover/{seriesId}/banner.jpg?apikey={Config.Instance.API}");

                    List<EpisodeModel> episodes = new();
                    using (HttpResponseMessage episodeResponse = Connection.MakeRequest($"/episode?seriesId={seriesId}&includeImages=true"))
                    {
                        JArray episodeJArray = JArray.Parse(episodeResponse.Content.ReadAsStringAsync().Result);
                        foreach (JObject episode in episodeJArray)
                        {
                            episodes.Add(new()
                            {
                                Title = episode["title"]?.ToObject<string>() ?? "",
                                AirDate = episode["airDateUtc"]?.ToObject<DateTime>() ?? DateTime.MinValue,
                                Description = episode["overview"]?.ToObject<string>() ?? "",
                                EpisodeNumber = episode["episodeNumber"]?.ToObject<int>() ?? 0,
                                IsDownloaded = episode["hasFile"]?.ToObject<bool>() ?? false,
                                Monitored = episode["monitored"]?.ToObject<bool>() ?? false,
                                SeasonNumber = episode["seasonNumber"]?.ToObject<int>() ?? 0,
                            });
                        }
                    }

                    List<SeasonModel> seasons = new();

                    if (item["seasons"] != null)
                    {
                        JArray seasonJArray = item["seasons"].ToObject<JArray>();
                        foreach (JObject season in seasonJArray)
                        {
                            int seasonNumber = season["seasonNumber"]?.ToObject<int>() ?? 0;
                            seasons.Add(new()
                            {
                                Monitored = season["monitored"]?.ToObject<bool>() ?? false,
                                PercentDownloaded = season["statistics"]?["percentOfEpisodes"]?.ToObject<double>() ?? 0,
                                Size = season["statistics"]?["sizeOnDisk"]?.ToObject<long>() ?? 0,
                                SeasonNumber = seasonNumber,
                                Episodes = episodes.Where(i => i.SeasonNumber == seasonNumber).ToArray()
                            });
                        }
                    }

                    series.Add(new()
                    {
                        Title = item["title"]?.ToObject<string>() ?? "",
                        Added = item["added"]?.ToObject<DateTime>() ?? DateTime.Now,
                        Certification = item["certification"]?.ToObject<string>() ?? "NR",
                        Description = item["overview"]?.ToObject<string>() ?? "",
                        IMDB_ID = item["imdbId"]?.ToObject<string>() ?? "",
                        TMDB_ID = item["tvdbId"]?.ToObject<string>() ?? "",
                        Year = item["year"]?.ToObject<int>() ?? 0,
                        FirstAirDate = item["firstAired"]?.ToObject<DateTime>() ?? DateTime.MinValue,
                        LastAirDate = item["previousAiring"]?.ToObject<DateTime>() ?? DateTime.MinValue,
                        Monitored = item["monitored"]?.ToObject<bool>() ?? false,
                        Status = item["status"]?.ToObject<string>() ?? "",
                        Ended = item["ended"]?.ToObject<bool>() ?? true,
                        Network = item["network"]?.ToObject<string>() ?? "",
                        Path = item["path"]?.ToObject<string>() ?? "",
                        Size = item["statistics"]?["sizeOnDisk"]?.ToObject<long>() ?? 0,
                        PercentDownloaded = item["statistics"]?["percentOfEpisodes"]?.ToObject<double>() ?? 0d,
                        BackgroundImage = bg,
                        PosterImage = poster,
                        BannerImage = banner,
                        Seasons = seasons.ToArray()
                    });
                });
            }
        }
        return new(series.OrderBy(i => i.Title).ToArray());
    }
}