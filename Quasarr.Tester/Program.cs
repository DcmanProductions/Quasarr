// LFInteractive LLC. - All Rights Reserved
using Quasarr.Tester.Tests;

namespace Quasarr.Tester;

internal class Program
{
    private static void Main()
    {
        long start = DateTime.Now.Ticks;
        Console.WriteLine("Initializing Tests...");
        new TestSABnzbd().Start().Wait();
        new TestNetworking().Start().Wait();
        new TestSonarr().Start().Wait();
        TimeSpan after = new(DateTime.Now.Ticks - start);
        Console.WriteLine($"\n\n\nTest took {after.Minutes}m {after.Seconds}s {after.Milliseconds}ms");
    }
}