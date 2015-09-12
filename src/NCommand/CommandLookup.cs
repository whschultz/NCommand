﻿using System;
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
            return GetCommands().FirstOrDefault(x => x.Item1.CommandName == (name ?? "").ToLower().Trim())?.Item1;
        }

        /// <summary>
        /// Run command.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="arguments"></param>
        public Tuple<bool, object> Run(CommandInfo command, object[] arguments)
        {
            var method = GetCommands().FirstOrDefault(x => x.Item1 == command);
            if (method != null)
            {
                try
                {
                    var obj = Activator.CreateInstance(method.Item3, null);
                    var result = method.Item2.Invoke(obj, arguments);
                    return new Tuple<bool, object>(true, result);
                }
                catch (Exception) { } // todo: log
            }
            return new Tuple<bool, object>(false, null);
        }

        #region Private

        private IEnumerable<Tuple<CommandInfo, MethodInfo, Type>> GetCommands ()
        {
            if (_commands == null)
            {
                var methods = AttributeUtil.GetMethodByAttribute<CommandAttribute>(_assemblies);
                _commands = methods.Select(x => new Tuple<CommandInfo, MethodInfo, Type>( new CommandInfo()
                {
                    CommandName = x?.Item2?.Name?.ToLower().Trim() ?? x.Item1?.Name?.ToLower().Trim(),
                    Description = x?.Item2?.Description,
                    Arguments = AttributeUtil.GetParametersAndAttributes<ArgumentAttribute>(x.Item1).Select(y => new ArgumentInfo()
                    {
                        Name = y?.Item2?.Name?.ToLower().Trim() ?? y?.Item1?.Name?.ToLower().Trim(),
                        Description = y?.Item2?.Description,
                        DefaultValue = y?.Item2?.DefaultValue,
                        Type = y?.Item3
                    }).ToList()
                }, x.Item1, x?.Item1?.DeclaringType)).ToList();
            }
            //methods.First().Item1.DeclaringType
            return _commands;
        }

        #endregion

    }
}
