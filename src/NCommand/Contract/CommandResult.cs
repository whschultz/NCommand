using System.Collections.Generic;

namespace Tectil.NCommand.Contract
{
    public class CommandResult
    {
        /// <summary>
        /// Command result.
        /// </summary>
        public dynamic Result { get; set; }

        /// <summary>
        /// Result state. Success, missing input or parser error.
        /// </summary>
        public ResultState State { get; set; }

        /// <summary>
        /// Missing arguments.
        /// </summary>
        public IEnumerable<ArgumentInfo> MissingArguments { get; set; }

        /// <summary>
        /// Command info.
        /// </summary>
        public CommandInfo CommandInfo { get; set; }
    }
}
