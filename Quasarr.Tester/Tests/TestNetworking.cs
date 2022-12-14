using Quasarr.Networking.Utilities;
using System.Net;

namespace Quasarr.Tester.Tests;

internal class TestNetworking : TestBase
{
    public TestNetworking() : base("Networking", new()
    {
        {
            "UPNP or PMP Support",
            new(() =>
            {
                try
                {
                    PortManager.GetAllMappings();
                    return true;
                }catch(WebException)
                {
                    return false;
                }
            })
        },
        {
            "Ports Opened",
            new(() =>
            {
                try
                {
                    return PortManager.OpenPort(Quasarr.Core.Config.Instance.Port).Result.Length == 2;
                }catch
                {
                    return false;
                }
            })
        },
        {
            "Check Open Ports",
            new(() =>
            {
                try
                {
                    return PortManager.IsPortOpen(Core.Config.Instance.Port).Result;
                }catch
                {
                    return false;
                }
            })
        },
        {
            "Ports Closed",
            new(() =>
            {
                try
                {
                    return PortManager.ClosePort(Quasarr.Core.Config.Instance.Port).Result.Length == 2;
                }catch
                {
                    return false;
                }
            })
        }
    })
    {

    }
}