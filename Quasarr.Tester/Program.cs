using Quasarr.Tester.Tests;

namespace Quasarr.Tester;

internal class Program
{
    static void Main()
    {
        new TestSABnzbd().Start().Wait();
    }
}