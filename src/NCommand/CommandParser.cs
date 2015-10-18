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

        #region Private
        private readonly List<string> _systemCommands = new List<string>() { "help", "exit", "quit", "cancel" }; // mode
        private readonly ParserNotation _notation;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandParser"/> class.
        /// </summary>
        /// <param name="notation">The notation.</param>
        public CommandParser(ParserNotation notation)
        {
            _notation = notation;
        }

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
            var seperator1 = _notation == ParserNotation.Windows ? '/' : '-';
            var seperator2 = _notation == ParserNotation.Windows ? ':' : '=';

            // Helppage as default:
            if (string.IsNullOrWhiteSpace(command))
            {
                command = $"ncommandsystem {seperator1}help{seperator2}true";
            }
            if (_systemCommands.Any(x => x == command?.Trim().Trim(seperator1).ToLower()))
            {
                command = $"ncommandsystem {seperator1}{command?.Trim().ToLower()}{seperator2}true";
            }

            // Detect flags (eg ' /enable' -> ' /enable:true')
            command = command + $" {seperator1}";
            while (true)
            {
                var result = new Regex($" ({seperator1}[a-z,A-Z]+)[ ]+{seperator1}", RegexOptions.IgnoreCase).Match(command); // , command + ":true /"
                if (!result.Success) break;
                command = command.Replace(result.ToString(), result.ToString().TrimEnd(seperator1).TrimEnd() + "{seperator2}true {seperator1}");
            }
            command = command.TrimEnd(seperator1).TrimEnd();

            // Parse command
            return StringCommandUtil.ParseCommand(command, "^[a-z,A-Z]+$", $"({seperator1}[a-z,A-Z]+{seperator2})", new [] { seperator1, seperator2 }, true);
        }
    }
}
