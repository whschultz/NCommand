using System;
using System.Collections.Generic;

namespace Tectil.NCommand.Contract
{
    /// <summary>
    /// Command result.
    /// </summary>
    public class CommandResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResult"/> class.
        /// </summary>
        public CommandResult()
        {
            MissingArguments = new List<ArgumentInfo>();
            MissingDefaultArguments = new List<ArgumentInfo>();
            Exceptions = new List<Exception>();
        }

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
        public List<ArgumentInfo> MissingArguments { get; set; }

        /// <summary>
        /// Missing default arguments. Default arguments not passed through command.
        /// </summary>
        public List<ArgumentInfo> MissingDefaultArguments { get; set; }

        /// <summary>
        /// Command info.
        /// </summary>
        public CommandInfo CommandInfo { get; set; }

        /// <summary>
        /// Gets or sets the exceptions.
        /// </summary>
        /// <value>
        /// The exceptions.
        /// </value>
        public List<Exception> Exceptions { get; set; }
    }
}
