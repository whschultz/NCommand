namespace Tectil.NCommand.ConsoleExample
{
    static class Program
    {
        static void Main(string[] args)
        {
            NCommander.App.AutodetectCommandAssemblies(); // Loads all assemblies in bin folder and checks for CommandAttribute
            NCommander.App.RunConsole(args);
#if DEBUG
            NCommanderConsoleApp.IO.ReadLine(null);
#endif
        }
    }
}
