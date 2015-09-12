
namespace Tectil.NCommand.ConsoleExample
{
    static class Program
    {
        static void Main(string[] args)
        {
            NCommander.App.RunConsole(args);
#if DEBUG
            NCommanderConsoleApp.IO.ReadLine(null);
#endif
        }
    }
}
