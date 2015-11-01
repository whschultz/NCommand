using System.Collections.Generic;

namespace Tectil.NCommand.Contract
{
    /// <summary>
    /// Mapping result.
    /// </summary>
    public class MappingResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingResult"/> class.
        /// </summary>
        public MappingResult()
        {
            ResultArguments = new List<object>();
            MissingArguments = new List<ArgumentInfo>();
            MissingDefaultArguments = new List<ArgumentInfo>();
        }

        /// <summary>
        /// State of mapping. MissingArguments or success.
        /// </summary>
        public ResultState State { get; set; }

        /// <summary>
        /// Result. If state = Success
        /// </summary>
        public List<object> ResultArguments { get; set; }

        /// <summary>
        /// Missing arguments. If state = MissingArguments
        /// </summary>
        public List<ArgumentInfo> MissingArguments { get; set; }

        /// <summary>
        /// Missing default arguments. Default arguments not enterd thorugh command line. Need to be prompted for in case of MissingArguments.
        /// </summary>
        public List<ArgumentInfo> MissingDefaultArguments { get; set; }
    }
}
