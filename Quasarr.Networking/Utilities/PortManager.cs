// LFInteractive LLC. - All Rights Reserved
using Mono.Nat;
using System.Net;
using System.Net.NetworkInformation;

namespace Quasarr.Networking.Utilities;

public class PortManager
{
    private static PortManager Instance = Instance ??= new();

    private INatDevice _device;

    private PortManager()
    {
        try
        {
            NatUtility.StartDiscovery(new[] { NatProtocol.Upnp });
        }
        catch
        {
            try
            {
                NatUtility.StartDiscovery(new[] { NatProtocol.Pmp });
            }
            catch
            {
                throw new WebException("Router either isn't found or doesn't support UPNP nor PMP");
            }
        }
        NatUtility.DeviceFound += (s, e) =>
        {
            NatUtility.StopDiscovery();
            _device = e.Device;
        };
        while (_device == null) { Task.Delay(100).Wait(); }
    }

    public static async Task<Mapping[]> ClosePort(int port) => new Mapping[]
    {
        await Instance._device.DeletePortMapAsync(new Mapping(Protocol.Udp, port, port)),
        await Instance._device.DeletePortMapAsync(new Mapping(Protocol.Tcp, port, port))
    };

    public static Task<Mapping[]> GetAllMappings() => Instance._device.GetAllMappingsAsync();

    public static IPAddress? GetLocalIPAddress()
    {
        NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
        foreach (NetworkInterface adapter in adapters)
        {
            IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
            GatewayIPAddressInformationCollection addresses = adapterProperties.GatewayAddresses;
            if (addresses.Any())
            {
                foreach (GatewayIPAddressInformation address in addresses)
                {
                    string root = string.Join('.', address.Address.ToString().Split('.').Take(3));
                    IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                    foreach (IPAddress ip in host.AddressList)
                    {
                        if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            if (ip.ToString().StartsWith(root))
                            {
                                return ip;
                            }
                        }
                    }
                }
            }
        }
        return null;
    }

    public static async Task<bool> IsPortOpen(int port)
    {
        Mapping[] mappings = await GetAllMappings();
        return mappings.Any(i => i.PrivatePort == port);
    }

    public static async Task<Mapping[]> OpenPort(int port) => new Mapping[]
            {
        await Instance._device.CreatePortMapAsync(new Mapping(Protocol.Udp, port, port)),
        await Instance._device.CreatePortMapAsync(new Mapping(Protocol.Tcp, port, port))
    };
}