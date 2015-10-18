using System.Linq;
using Tectil.NCommand.Contract;
using Tectil.NCommand.ExecutionFlows.Internals;

namespace Tectil.NCommand
{
    /// <summary>
    /// Starting point for your console.
    /// </summary>
    public static class NCommandsConsole
    {
        /// <summary>
        /// Gets console io manager.
        /// </summary>
        /// <value>
        /// The io.
        /// </value>
        // ReSharper disable once InconsistentNaming
        public static ConsoleUtility IO(this NCommands commands)
        {
            return ConsoleUtility.Instance;
        }

        /// <summary>
        /// Run command in console.
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static void RunConsole(this NCommands commands, string[] args)
        {
            // Run
            var arguments = args.ToList();
            var runner = new CommandRunner(new CommandParser(commands.Context.Configuration.Notation), new CommandLookup(commands.Context.Configuration));
            var result = runner.Run(string.Join(" ", arguments));

            // Execution flow
            var context = new ExecutionContext(ConsoleUtility.Instance, result, runner, arguments, commands.Context.Configuration);
            var flowManager = new ExecutionFlowFactory().Get(context); // todo: make this injectable
            flowManager.Run(context);
        }
    }
}
