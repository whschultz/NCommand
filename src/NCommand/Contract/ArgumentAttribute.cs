
using System;

namespace Tectil.NCommand.Contract
{
    public class ArgumentAttribute
        : Attribute
    {
        public ArgumentAttribute(string name = null, object defaultValue = null, string description = null)
        {
            Name = name;
            DefaultValue = defaultValue;
            Description = description;
        }

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

    }
}
