using System;
using System.Collections.Generic;
using System.Linq;
using Tectil.NCommand.Contract;
using Tectil.NCommand.Utilities;

namespace Tectil.NCommand
{
    public class CommandMapper
        : ICommandMapper
    {
        public MappingResult Map(IEnumerable<KeyValuePair<string, object>> arguments, IEnumerable<ArgumentInfo> parameters)
        {
            // Prepare
            var result = new MappingResult();
            if (arguments == null) arguments = new List<KeyValuePair<string, object>>();
            if (parameters == null) parameters = new List<ArgumentInfo>();

            // Map
            var mapped = parameters.GroupJoin(arguments.DefaultIfEmpty(), x => x.Name, x => x.Key, (x, y) => new Tuple<ArgumentInfo, object>(x, y.ToList().FirstOrDefault().Value) ).ToList();
            var missing = mapped.Where(x => x.Item1.DefaultValue == null && x.Item2 == null).ToList();

            // Has missing
            if (missing.Any())
            {
                result.State = ResultState.MissingArguments;
                result.MissingArguments = missing.Select(x => x.Item1).ToList();
                return result;
            }

            // Result
            result.State = ResultState.Success;
            result.ResultArguments = mapped.Select(x => DataCastUtil.Convert(x.Item2?.ToString() ?? x.Item1.DefaultValue?.ToString(), x.Item1.Type)).ToList();
            return result;
        }
    }
}
