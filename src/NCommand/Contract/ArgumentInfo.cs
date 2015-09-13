using System;

namespace Tectil.NCommand.Contract
{
    /// <summary>
    /// Argument info
    /// </summary>
    public class ArgumentInfo
    {
        /// <summary>
        /// Command name. Eg /path:/Temp/ -> path
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value. Eg /path:/Temp/ -> /Temp/
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Data type.
        /// </summary>
        public Type Type { get; set; }
    }
}
