// LFInteractive LLC. - All Rights Reserved
namespace Quasarr.Core;

public static class Locations
{
    public static string Data => Directory.CreateDirectory(Path.Combine(Root, "data")).FullName;
    public static string Logs => Directory.CreateDirectory(Path.Combine(Root, "logs")).FullName;
    public static string Root => Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ApplicationData.Company, ApplicationData.ApplicationName)).FullName;
    public static string Temp => Directory.CreateDirectory(Path.Combine(Root, "tmp")).FullName;
}