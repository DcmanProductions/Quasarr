// LFInteractive LLC. - All Rights Reserved
using Quasarr.Tester.Tests;

namespace Quasarr.Tester;

internal class Program
{
    private static void Main()
    {
        Console.WriteLine("Initializing Tests...");
        //new TestSABnzbd().Start().Wait();
        //new TestNetworking().Start().Wait();
        new TestSonarr().Start().Wait();
    }
}