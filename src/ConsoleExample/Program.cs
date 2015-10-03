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
            NCommands commands = new NCommands();
            commands.Context.AutodetectCommandAssemblies(); // Loads all assemblies in bin folder and checks for CommandAttribute
            commands.RunConsole(args);
        }
    }
}
