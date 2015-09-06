using System.Collections.Generic;

namespace Tectil.NCommand.Contract
{
    public interface ICommandMapper
    {
        /// <summary>
        /// Map arguments to parameters.
        /// </summary>
        /// <param name="arguments">Parsed from command.</param>
        /// <param name="parameters">From method.</param>
        /// <returns></returns>
        MappingResult Map(IEnumerable<KeyValuePair<string, object>> arguments, IEnumerable<ArgumentInfo> parameters);
    }
}
