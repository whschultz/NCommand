using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Tectil.NCommand.Utilities
{
    /// <summary>
    /// String command utility.
    /// </summary>
    internal static class StringCommandUtil
    {

        /// <summary>
        /// Parser for format commandname [argumentValue] [argumentValue]. Basic validation implemented.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="commandRegex">([a-z,A-Z]*)</param>
        /// <param name="argumentRegex">(/[a-z,A-Z]*:)</param>
        /// <param name="trimKeyValues"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, object>> ParseCommand(string command, string commandRegex, string argumentRegex, char[] trimKeyValues, bool ignoreCase)
        {
            // Split command
            var lst = Regex.Split(command, argumentRegex); // Match: "/Command:"
            var commandValueLst = lst.Skip(1)
                .Select((value, index) => new { Index = index, Value = value })
                .GroupBy(x => x.Index / 2)
                .Select(x =>
                    new KeyValuePair<string, object>(
                    (x.ElementAt(0)?.Value ?? "").Trim(),
                    (x.ElementAt(1)?.Value ?? "").Trim())
                ).ToList();

            // Validate: check command and arguments
            if (commandValueLst.Any(x => !Regex.Match(x.Key, argumentRegex).Success)) return new List<KeyValuePair<string, object>>(); // todo return error message
            if (!Regex.Match((lst.First() ?? "").Trim(), commandRegex).Success) return new List<KeyValuePair<string, object>>(); // todo return error message

            // Command name as first item
            commandValueLst.Insert(0, new KeyValuePair<string, object>((lst.First() ?? "").Trim(), null));

            // Format
            if (ignoreCase)
            {
                commandValueLst = commandValueLst.Select(x => new KeyValuePair<string, object>(x.Key.ToLower().Trim(trimKeyValues), x.Value)).ToList();
            }
            else
            {
                commandValueLst = commandValueLst.Select(x => new KeyValuePair<string, object>(x.Key.Trim(trimKeyValues), x.Value)).ToList();
            }

            // Result
            return commandValueLst;
        }
    }
}
