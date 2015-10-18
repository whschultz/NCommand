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
        /// <param name="configuration">The configuration.</param>
        public ExecutionContext(ConsoleUtility io, CommandResult command, CommandRunner runner, List<string> arguments, CommandConfiguration configuration)
        {
            IO = io;
            Command = command;
            Runner = runner;
            Arguments = arguments;
            Configuration = configuration;
        }

        /// <summary>
        /// Gets or sets the io.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public ConsoleUtility IO { get; set; }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        public CommandResult Command { get; set; }

        /// <summary>
        /// Gets or sets the runner.
        /// </summary>
        public CommandRunner Runner { get; set; }

        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        public List<string> Arguments { get; set; }

        /// <summary>
        /// Gets or sets the commands.
        /// </summary>
        /// <value>
        /// The commands.
        /// </value>
        public List<CommandInfo> Commands { get; set; }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        public CommandConfiguration Configuration { get; set; }
    }
}
