using Tectil.NCommand.Contract;

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
            commands.Context.Configuration.DisplayExceptionDetails = false;
            commands.Context.Configuration.Notation = ParserNotation.Unix;
            commands.RunConsole(args);
        }
    }
}
