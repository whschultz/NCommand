using System.Collections.Generic;

namespace Tectil.NCommand.Contract
{
    public class MappingResult
    {
        public MappingResult()
        {
            ResultArguments = new List<object>();
            MissingArguments = new List<ArgumentInfo>();
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
    }
}
