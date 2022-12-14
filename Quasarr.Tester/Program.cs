using Newtonsoft.Json;
using Quasarr.Networking.Utilities;
using Quasarr.Tester.Tests;
using System.Net;

namespace Quasarr.Tester;

internal class Program
{
    static void Main()
    {
        Console.WriteLine("Initializing Tests...");
        new TestSABnzbd().Start().Wait();
        new TestNetworking().Start().Wait();
    }
}