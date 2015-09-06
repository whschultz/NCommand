using System;
namespace Tectil.NCommand.Contract
{
    public class CommandAttribute
        : Attribute
    {
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
