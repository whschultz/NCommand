namespace Tectil.NCommand.ConsoleExample
{
    /// <summary>
    /// Console example.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        /// <param name="args">The arguments.</param>
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
