using System.Collections.Generic;
using System.Reflection;

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
        /// Gets or sets a value indicating whether [display exceptions].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [display exceptions]; otherwise, <c>false</c>.
        /// </value>
        public bool DisplayExceptionDetails { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether {CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}[trace exceptions].
        /// </summary>
        /// <value>
        /// {D255958A-8513-4226-94B9-080D98F904A1}  <c>true</c> if [trace exceptions]; otherwise, <c>false</c>.
        /// </value>
        public bool TraceExceptions { get; set; } = true;
    }
}
