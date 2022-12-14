// LFInteractive LLC. - All Rights Reserved
using Quasarr.SABnzbd.Collections;

namespace Quasarr.Tester.Tests;

internal class TestSABnzbd : TestBase
{
    public TestSABnzbd() : base("SABnzbd",
        new()
        {
            {
                "Poll",
                new(() =>
                {
                    return DownloadQueueCollection.Poll() != null;
                })
            }
        })
    {
    }
}