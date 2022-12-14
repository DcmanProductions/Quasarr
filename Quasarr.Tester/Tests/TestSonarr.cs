// LFInteractive LLC. - All Rights Reserved
using Quasarr.Sonarr.Collections;

namespace Quasarr.Tester.Tests
{
    internal class TestSonarr : TestBase
    {
        public TestSonarr() : base("Sonarr",
            new()
            {
            {
                "Poll",
                new(() =>
                {
                    try
                    {
                        SeriesCollection series = SeriesCollection.Poll();
                        return true;
                    }
                    catch(Exception ex)
                    {
                        return false;
                    }
                })
            }
            })
        {
        }
    }
}