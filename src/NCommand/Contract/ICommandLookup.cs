using System;
using System.Collections.Generic;
using System.Reflection;

namespace Tectil.NCommand.Contract
{
    /// <summary>
    /// Command lookup interface.
    /// </summary>
    public interface ICommandLookup
    {
        /// <summary>
        /// Return commands.
        /// </summary>
        /// <returns></returns>
        IEnumerable<CommandInfo> Commands { get; }

        /// <summary>
        /// Get command.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        CommandInfo GetCommand(string name);

        /// <summary>
        /// Gets the methode.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        Tuple<CommandInfo, MethodInfo, Type> GetMethode(CommandInfo command);
    }
}
