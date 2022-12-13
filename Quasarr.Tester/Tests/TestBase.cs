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
            List<string> completed = new(), failed = new();
            foreach ((string name, Func<bool> component) in _com)
            {
                bool success = false;
                try
                {
                    success = component.Invoke();
                }
                catch
                {
                    success = false;
                }
                if (success)
                {
                    completed.Add(name);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"[✔️] {name}");
                }
                else
                {
                    failed.Add(name);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"[❌] {name}");
                }
                Console.ResetColor();
            }
            return new TestResult(_name, completed.ToArray(), failed.ToArray());
        });
}
