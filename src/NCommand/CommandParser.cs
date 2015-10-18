using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Tectil.NCommand.Contract;
using Tectil.NCommand.Utilities;

namespace Tectil.NCommand
{
    /// <summary>
    /// Parses as string and constructs an expression tree.
    /// </summary>
    public class CommandParser
        : ICommandParser
    {
        private readonly List<string> _systemCommands = new List<string>() { "help", "exit", "quit", "cancel" }; // mode

        /// <summary>
        /// Default implementation. 
        /// Command format is "commandname /parameter:valueWithSpaceAllowed /parameter:valueWithSpaceAllowed"
        /// Case is ignored
        /// If command can not be parsed, null is returned.
        /// </summary>
        /// <example>
        /// websearch /s:weather cupertino /take:10 /language:de
        /// </example>
        /// <param name="command"></param>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<string, object>> Parse(string command)
        {
            // Helppage as default:
            if (string.IsNullOrWhiteSpace(command))
            {
                command = "ncommandsystem /help:true";
            }
            if (_systemCommands.Any(x => x == command?.Trim().Trim('/').ToLower()))
            {
                command = $"ncommandsystem /{command?.Trim().ToLower()}:true";
            }

            // Detect flags (eg ' /enable' -> ' /enable:true')
            command = command + " /";
            while (true)
            {
                var result = new Regex(" (/[a-z,A-Z]+)[ ]+/", RegexOptions.IgnoreCase).Match(command); // , command + ":true /"
                if (!result.Success) break;
                command = command.Replace(result.ToString(), result.ToString().TrimEnd('/').TrimEnd() + ":true /");
            }
            command = command.TrimEnd('/').TrimEnd();

            // Parse command
            return StringCommandUtil.ParseCommand(command, "^[a-z,A-Z]+$", "(/[a-z,A-Z]+:)", new [] { '/', ':' }, true);
        }
    }
}
