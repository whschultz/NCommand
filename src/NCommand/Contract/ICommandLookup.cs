using System;
using System.Collections.Generic;

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
        /// Run command.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        Tuple<bool, object> Run(CommandInfo command, object[] arguments);
    }
}
