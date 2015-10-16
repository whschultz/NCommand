using System;
using System.Collections.Generic;

namespace Tectil.NCommand.Contract
{
    /// <summary>
    /// Method result
    /// </summary>
    /// <typeparam name="T">Method attribute</typeparam>
    public class MethodResult<T>
    {
        /// <summary>
        /// Gets or sets the methodes.
        /// </summary>
        /// <value>
        /// The methodes.
        /// </value>
        public List<MethodeAttribute<T>> Methodes { get; set; }

        /// <summary>
        /// Gets or sets the exceptions.
        /// </summary>
        /// <value>
        /// The exceptions.
        /// </value>
        public List<Exception> Exceptions { get; set; }
    }
}
