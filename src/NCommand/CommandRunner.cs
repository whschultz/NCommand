using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Tectil.NCommand.Contract;

namespace Tectil.NCommand
{
    /// <summary>
    /// Entrypoint to run a command.
    /// </summary>
    public class CommandRunner
        : ICommandRunner
    {

        #region Private
        private readonly ICommandParser _parser;
        private readonly ICommandLookup _lookup;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandRunner"/> class.
        /// </summary>
        /// <param name="parser">The parser.</param>
        /// <param name="lookup">The lookup.</param>
        public CommandRunner(ICommandParser parser, ICommandLookup lookup)
        {
            _parser = parser;
            _lookup = lookup;
        }
        
        /// <summary>
        /// Validate command (parse, find and compare arguments)
        /// </summary>
        /// <param name="commandline"></param>
        /// <returns></returns>
        public CommandResult Validate(string commandline)
        {
            var result = ValidateDo(commandline);
            return result.Item1;
        }

        /// <summary>
        /// Run command (parse, find, compare arguments and execute if ok)
        /// </summary>
        /// <param name="commandline"></param>
        /// <returns></returns>
        public CommandResult Run(string commandline)
        {
            // Validate
            var result = ValidateDo(commandline);

            // Run
            if (result.Item1.State == ResultState.Success)
            {
                var method = _lookup.GetMethode(result.Item3);
                if (method != null)
                {
                    try
                    {
                        var obj = Activator.CreateInstance(method.Item3, null);
                        var methodResult = method.Item2.Invoke(obj, result.Item2.ToArray());
                        result.Item1.Result = methodResult;
                        result.Item1.State = ResultState.Success;
                        return result.Item1;
                    }
                    catch (Exception ex)
                    {
                        result.Item1.State = ResultState.ErrorWhileExecuting;
                        result.Item1.Exceptions.Add(ex);
                        return result.Item1;
                    } 
                }
            }

            // Result
            return result.Item1;
        }

        #region Private

        private Tuple<CommandResult, List<object>, CommandInfo> ValidateDo(string commandline)
        {
            // Prepare
            var result = new CommandResult();

            // Parse input
            List<KeyValuePair<string, object>> args = _parser.Parse(commandline).ToList();
            if (!args.Any())
            {
                // Parsing error
                result.State = ResultState.ParsingError;
                return new Tuple<CommandResult, List<object>, CommandInfo>(result, null, null);
            }

            // System commands
            if (args.First().Key == "ncommandsystem")
            {
                // Show help / info: overview of available functions
                if (args.Skip(1).FirstOrDefault().Key?.ToLower() == "help")
                {
                    result.State = ResultState.ShowHelpOverview;
                    result.CommandInfo = new CommandInfo() { CommandName = "ncommandsystem.help" };
                    result.Result = _lookup.Commands;
                    return new Tuple<CommandResult, List<object>, CommandInfo>(result, null, null);
                }
                if (args.Skip(1).FirstOrDefault().Key?.ToLower() == "exit" || args.Skip(1).FirstOrDefault().Key?.ToLower() == "quit" || args.Skip(1).FirstOrDefault().Key?.ToLower() == "cancel")
                {
                    result.State = ResultState.Canceled;
                    return new Tuple<CommandResult, List<object>, CommandInfo>(result, null, null);
                }
            }

            // Command by name
            var command = _lookup.GetCommand(args.First().Key);
            result.CommandInfo = command;
            if (command == null)
            {
                result.State = ResultState.UnknownCommand;
                return new Tuple<CommandResult, List<object>, CommandInfo>(result, null, null);
            }

            // Find missing arguments
            var mapper = new CommandMapper();
            var mapped = mapper.Map(args.Skip(1), command.Arguments);
            if (mapped.State == ResultState.MissingArguments)
            {
                result.State = ResultState.MissingArguments;
                result.MissingArguments = mapped.MissingArguments;
                result.MissingDefaultArguments = mapped.MissingDefaultArguments;
                return new Tuple<CommandResult, List<object>, CommandInfo>(result, null, null);
            }

            // Result
            result.State = ResultState.Success;
            return new Tuple<CommandResult, List<object>, CommandInfo>(result, mapped.ResultArguments, command);
        }

        #endregion
    }
}
