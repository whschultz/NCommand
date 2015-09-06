using System.Collections.Generic;
using System.Reflection;

namespace Tectil.NCommand.Contract
{
    public class CommandInfo
    {
        /// <summary>
        /// Command name.
        /// </summary>
        public string CommandName { get; set; }

        /// <summary>
        /// Desciption.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Command attributes.
        /// </summary>
        public IEnumerable<ArgumentInfo> Arguments { get; set; }
    }
}
