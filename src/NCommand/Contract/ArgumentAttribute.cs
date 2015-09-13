
using System;

namespace Tectil.NCommand.Contract
{
    /// <summary>
    /// Attribute for command arguments.
    /// </summary>
    public class ArgumentAttribute
        : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="description">The description.</param>
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
