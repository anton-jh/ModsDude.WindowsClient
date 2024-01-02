using MoonSharp.Interpreter;

namespace ModsDude.WindowsClient.Application.Adapters;
public static class ScriptDisposableExtensions
{
    private static Dictionary<Script, List<IDisposable>> _disposableLists = [];


    public static void TrackDisposable(this Script script, IDisposable disposable)
    {
        if (_disposableLists.TryGetValue(script, out List<IDisposable>? list))
        {
            list.Add(disposable);
        }
        else
        {
            _disposableLists.Add(script, [disposable]);
        }
    }

    public static void DisposeTracked(this Script script)
    {
        if (!_disposableLists.TryGetValue(script, out var list))
        {
            return;
        }

        foreach (var disposable in list)
        {
            disposable.Dispose();
        }
    }
}
