// LFInteractive LLC. - All Rights Reserved
namespace Quasarr.Sonarr.Utilities;

internal static class Connection
{
    public static string GetUrl(string path) => $"http://{Config.Instance.Host}:{Config.Instance.Port}/api/v3{path}";

    public static bool IsValidConfig()
    {
        using HttpResponseMessage response = MakeRequest("/system/status");
        return response.IsSuccessStatusCode;
    }

    public static HttpResponseMessage MakeRequest(string path)
    {
        using HttpClient client = new();
        using HttpRequestMessage request = new(HttpMethod.Get, GetUrl(path));
        request.Headers.Add("X-Api-Key", "969d9051d1f64b5cb52d96362cd8f603");
        return client.Send(request);
    }
}