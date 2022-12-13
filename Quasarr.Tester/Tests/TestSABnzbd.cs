using Quasarr.SABnzbd.Collections;

namespace Quasarr.Tester.Tests;

internal class TestSABnzbd : TestBase
{
    public TestSABnzbd() : base("SABnzbd",
        new() {
            {
                "Poll",
                new(() => {
                    return DownloadQueue.Poll() != null;
                })
            }
        })
    {
    }
}
