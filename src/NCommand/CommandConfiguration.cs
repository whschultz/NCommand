using System.Collections.Generic;
using System.Reflection;
using Tectil.NCommand.Contract;

namespace Tectil.NCommand
{
    /// <summary>
    /// Configuration
    /// </summary>
    public class CommandConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandConfiguration"/> class.
        /// </summary>
        public CommandConfiguration()
        {
            CommandAssemblies = new List<Assembly>();
        }

        /// <summary>
        /// Gets or sets the assemblies to detect commands.
        /// </summary>
        /// <value>
        /// The command assemblies.
        /// </value>
        public List<Assembly> CommandAssemblies { get; set; }

        /// <summary>
        /// Gets or sets the notation.
        /// </summary>
        /// <value>
        /// The notation.
        /// </value>
        public ParserNotation Notation { get; set; } = ParserNotation.Windows;

        /// <summary>
        /// Gets or sets a value indicating whether [display exceptions].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [display exceptions]; otherwise, <c>false</c>.
        /// </value>
        public bool DisplayExceptionDetails { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether [trace exceptions].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [trace exceptions]; otherwise, <c>false</c>.
        /// </value>
        public bool TraceExceptions { get; set; } = true;
    }
}
