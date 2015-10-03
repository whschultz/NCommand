using System.Collections.Generic;

namespace Tectil.NCommand.Contract
{
    /// <summary>
    /// Execution context
    /// </summary>
    public class ExecutionContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionContext"/> class.
        /// </summary>
        /// <param name="io">The io.</param>
        /// <param name="command">The command.</param>
        /// <param name="runner">The runner.</param>
        /// <param name="arguments">The arguments.</param>
        public ExecutionContext(ConsoleUtility io, CommandResult command, CommandRunner runner, List<string> arguments)
        {
            IO = io;
            Command = command;
            Runner = runner;
            Arguments = arguments;
        }

        /// <summary>
        /// Gets or sets the io.
        /// </summary>
        /// <value>
        /// The io.
        /// </value>
        // ReSharper disable once InconsistentNaming
        public ConsoleUtility IO { get; set; }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public CommandResult Command { get; set; }

        /// <summary>
        /// Gets or sets the runner.
        /// </summary>
        /// <value>
        /// The runner.
        /// </value>
        public CommandRunner Runner { get; set; }

        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        /// <value>
        /// The arguments.
        /// </value>
        public List<string> Arguments { get; set; }

        /// <summary>
        /// Gets or sets the commands.
        /// </summary>
        /// <value>
        /// The commands.
        /// </value>
        public List<CommandInfo> Commands { get; set; }
    }
}
