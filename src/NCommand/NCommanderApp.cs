using System;
using System.Linq;
using System.Reflection;

namespace Tectil.NCommand
{
    /// <summary>
    /// Application instance. Extensions available inside client packages.
    /// </summary>
    public class NCommanderApp
    {

        #region ctx
        
        /// <summary>
        /// Initializes a new instance of the <see cref="NCommanderApp"/> class.
        /// </summary>
        public NCommanderApp()
        {
            Configuration = new CommandConfiguration();
            AddEntryAssembly(); // default value
        }

        #endregion

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public CommandConfiguration Configuration { get; set; }


        /// <summary>
        /// Autodetects assemblies containing commands. All assemblies will be reflected.
        /// </summary>
        public void AutodetectCommandAssemblies()
        {
            Configuration.CommandAssemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies().Where(x => !Configuration.CommandAssemblies.Contains(x)));
        }

        /// <summary>
        /// Adds executing assembly for command detection.
        /// </summary>
        public void AddEntryAssembly()
        {
            AddCommandAssembly(Assembly.GetEntryAssembly());
        }

        /// <summary>
        /// Adds assembly containing commands.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public void AddCommandAssembly(Assembly assembly)
        {
            if(!Configuration.CommandAssemblies.Contains(assembly))
            {
                Configuration.CommandAssemblies.Add(assembly);
            }
        }

        /// <summary>
        /// Adds assemblies containing commands.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public void AddCommandAssembly(Assembly[] assemblies)
        {
            Configuration.CommandAssemblies.AddRange(assemblies.Where(x => !Configuration.CommandAssemblies.Contains(x)));
        }

    }
}
