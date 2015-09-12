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
    }
}
