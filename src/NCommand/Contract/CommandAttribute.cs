using System;
namespace Tectil.NCommand.Contract
{
    /// <summary>
    /// Command attribute.
    /// </summary>
    public class CommandAttribute
        : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        public CommandAttribute(string name = null, string description = null)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Command name. Eg /path:/Temp/ -> path
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public Type Type { get; set; }
    }
}
