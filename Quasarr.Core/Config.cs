using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Quasarr.Core;

public sealed class Config
{
    public static Config Instance = new("quasarr");
    private readonly string _file;

    public int Port { get; set; } = 2248;
    public bool AllowPortforward { get; set; } = false;
    public bool UseAuthentication { get; set; } = false;

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
    public void Save()
    {
        using FileStream fs = new(_file, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
        using StreamWriter writer = new(fs);
        writer.Write(JsonConvert.SerializeObject(new
        {
            Port,
            AllowPortforward,
            UseAuthentication,
        }, Formatting.Indented));
    }
    public void Load()
    {
        using FileStream fs = new(_file, FileMode.Open, FileAccess.Read, FileShare.None);
        using StreamReader reader = new(fs);

        JObject json = JObject.Parse(reader.ReadToEnd());
        Port = json["Port"]?.ToObject<int>() ?? Port;
        AllowPortforward = json["AllowPortforward"]?.ToObject<bool>() ?? AllowPortforward;
        UseAuthentication = json["UseAuthentication"]?.ToObject<bool>() ?? UseAuthentication;
    }
}
