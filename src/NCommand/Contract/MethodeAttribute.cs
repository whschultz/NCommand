using System.Reflection;

namespace Tectil.NCommand.Contract
{
    /// <summary>
    /// Methode and attribute
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MethodeAttribute<T>
    {
        /// <summary>
        /// Gets or sets the method information.
        /// </summary>
        /// <value>
        /// The method information.
        /// </value>
        public MethodInfo MethodInfo { get; set; }

        /// <summary>
        /// Gets or sets the attribute.
        /// </summary>
        /// <value>
        /// The attribute.
        /// </value>
        public T Attribute { get; set; }
    }
}
