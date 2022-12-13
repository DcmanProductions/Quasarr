namespace Quasarr.Tester.Tests;

internal class TestSABnzbd : TestBase
{
    public TestSABnzbd() : base("SABnzbd",
        new() {
            {
                "",
                new(() => {
                    return false;
                })
            }
        })
    {
    }
}
