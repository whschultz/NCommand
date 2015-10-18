using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tectil.NCommand.Contract;
using Tectil.NCommand.Utilities;

namespace Tectil.NCommand
{
    /// <summary>
    /// Command lookup through reflection. Searching for methods with CommandAttribute.
    /// </summary>
    public class CommandLookup
        : ICommandLookup
    {

        #region ctx

        private readonly IEnumerable<Assembly> _assemblies;
        private List<Tuple<CommandInfo, MethodInfo, Type>> _commands = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLookup"/> class.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public CommandLookup(CommandConfiguration configuration)
        {
            _assemblies = configuration.CommandAssemblies;
        }

        #endregion
        
        /// <summary>
        /// Get commands available.
        /// </summary>
        public IEnumerable<CommandInfo> Commands
        {
            get
            {
                return GetCommands().Select(x => x.Item1).ToList();
            }
        }

        /// <summary>
        /// Get command.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CommandInfo GetCommand(string name)
        {
            return GetCommands().FirstOrDefault(x => x.Item1.CommandName.ToLower() == (name ?? "").ToLower().Trim())?.Item1;
        }

        /// <summary>
        /// Gets the methode.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public Tuple<CommandInfo, MethodInfo, Type> GetMethode(CommandInfo command)
        {
            return GetCommands().FirstOrDefault(x => x.Item1 == command);
        }

        #region Private

        private IEnumerable<Tuple<CommandInfo, MethodInfo, Type>> GetCommands ()
        {
            if (_commands == null)
            {
                var methods = AttributeUtil.GetMethodByAttribute<CommandAttribute>(_assemblies);
                _commands = methods.Methodes.Select(x => new Tuple<CommandInfo, MethodInfo, Type>( new CommandInfo()
                {
                    CommandName = x?.Attribute?.Name?.Trim() ?? x.MethodInfo?.Name?.Trim(),
                    Description = x?.Attribute?.Description,
                    Arguments = AttributeUtil.GetParametersAndAttributes<ArgumentAttribute>(x.MethodInfo).Select(y => new ArgumentInfo()
                    {
                        Name = y?.Item2?.Name?.Trim() ?? y?.Item1?.Name?.Trim(),
                        Description = y?.Item2?.Description,
                        DefaultValue = y?.Item2?.DefaultValue,
                        Type = y?.Item3
                    }).ToList()
                }, x.MethodInfo, x?.MethodInfo?.DeclaringType)).ToList();
            }
            //methods.First().Item1.DeclaringType
            return _commands;
        }

        #endregion

    }
}
