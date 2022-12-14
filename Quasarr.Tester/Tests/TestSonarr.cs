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
                        SeriesCollection series = SeriesCollection.Poll((s, e) =>
                        {
                            //Console.CursorLeft = 0;
                            //for (int i = 0; i < Console.WindowWidth; i++)
                            //{
                            //    Console.Write(' ');
                            //}
                            Console.CursorLeft = 0;
                            Console.Write($"[-] Poll - {e.Percentage:p2}");
                        });
                        return true;
                    }
                    catch
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