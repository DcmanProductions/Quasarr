// LFInteractive LLC. - All Rights Reserved

namespace Quasarr.Tester.Tests;

internal class TestBase
{
    public record TestResult(string name, string[]? success, string[]? failed);
    private readonly Dictionary<string, Func<bool>> _com;
    private readonly string _name;

    public TestBase(string name, Dictionary<string, Func<bool>> Components)
    {
        _com = Components;
        _name = name;
    }

    public Task<TestResult> Start() => Task.Run(() =>
        {
            Console.WriteLine($"\n-- {_name}");
            List<string> completed = new(), failed = new();
            foreach ((string name, Func<bool> component) in _com)
            {
                bool success = false;
                try
                {
                    Console.CursorLeft = 0;
                    Console.Write($"[-] {name}");
                    success = component.Invoke();
                    Console.CursorLeft = 0;
                }
                catch
                {
                    success = false;
                }
                Console.CursorLeft = 0;
                for (int i = 0; i < Console.WindowWidth; i++)
                {
                    Console.Write(' ');
                }
                Console.CursorLeft = 0;

                if (success)
                {
                    completed.Add(name);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[OK] {name}");
                }
                else
                {
                    failed.Add(name);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[FAIL] {name}");
                }
                Console.ResetColor();
            }
            return new TestResult(_name, completed.ToArray(), failed.ToArray());
        });
}