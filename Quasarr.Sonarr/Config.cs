// LFInteractive LLC. - All Rights Reserved
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quasarr.Core;

namespace Quasarr.Sonarr;

public sealed class Config
{
    public static Config Instance = new("sonarr");
    private readonly string _file;

    internal Config(string name)
    {
        Instance = this;
        _file = Path.Combine(Locations.Data, $"{name}.json");

        if (File.Exists(_file))
        {
            Load();
        }
        else
        {
            Save();
        }
    }

    public string API { get; set; } = "";
    public string Host { get; set; } = "127.0.0.1";
    public int Port { get; set; } = 8989;

    public void Load()
    {
        using FileStream fs = new(_file, FileMode.Open, FileAccess.Read, FileShare.None);
        using StreamReader reader = new(fs);

        JObject json = JObject.Parse(reader.ReadToEnd());
        Port = json["Port"]?.ToObject<int>() ?? Port;
        Host = json["Host"]?.ToObject<string>() ?? Host;
        API = json["API"]?.ToObject<string>() ?? API;
    }

    public void Save()
    {
        using FileStream fs = new(_file, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
        using StreamWriter writer = new(fs);
        writer.Write(JsonConvert.SerializeObject(new
        {
            Port,
            Host,
            API,
        }, Formatting.Indented));
    }
}