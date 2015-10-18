using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tectil.NCommand.Contract;

namespace Tectil.NCommand.ExecutionFlows.Internals
{
    /// <summary>
    /// Base class for flow execution.
    /// </summary>
    public abstract class ExecutionFlowBase
    {
        /// <summary>
        /// Runs the command.
        /// </summary>
        public bool RunCommand(ExecutionContext context)
        {
            switch (context.Command.State)
            {
                case ResultState.Success:
                    var res = context.Command.Result;
                    if (context.Command.Result is Task) res = context.Command.Result.Result;
                    if (res is IEnumerable && !(res is IEnumerable<char>))
                    {
                        foreach (var item in (res as IEnumerable))
                        {
                            context.IO.WriteLine(item?.ToString());
                        }
                        context.IO.WriteLine("");
                    }
                    else
                    {
                        context.IO.WriteLine(res?.ToString());
                        context.IO.WriteLine("");
                    }
                    context.Command.State = ResultState.PromptForCommand;
                    break;

                case ResultState.MissingArguments:

                    context.IO.WriteLine(@"{0} arguments missing. ", context.Command.MissingArguments.Count());
                    context.IO.WriteLine("");
                    var cancelThisCommand = false;
                    var missingArguments = new List<string>();
                    for (int i = 0; i < context.Command.MissingArguments.Count(); i++)
                    {
                        var argument = context.Command.MissingArguments.ElementAt(i);
                        if (!string.IsNullOrWhiteSpace(argument.Description))
                        {
                            context.IO.WriteLine($"DescriptiOn: {argument.Description}");
                            context.IO.WriteLine($"Enter value for '{argument.Name}':");
                        }
                        else
                        {
                            context.IO.WriteLine($"Enter value for '{argument.Name}':");
                        }
                        var input = context.IO.ReadLine(() => cancelThisCommand = true);
                        if (cancelThisCommand)
                        {
                            context.Command.State = ResultState.PromptForCommand;
                            break;
                        }
                        missingArguments.Add("/" + argument.Name + ":" + input); // todo: ReRun (enteredValues) method on CommandRunner (Initial paramaters saved in a state)
                        context.IO.WriteLine("");
                    }
                    if (cancelThisCommand) break;
                    context.IO.WriteLine("");
                    context.Arguments.AddRange(missingArguments);
                    context.Command = context.Runner.Run(string.Join(" ", context.Arguments));
                    break;

                case ResultState.UnknownCommand:
                    context.IO.WriteLine(@"Unknown command.");
                    context.IO.WriteLine("");
                    context.Command.State = ResultState.PromptForCommand;
                    break;

                case ResultState.ParsingError:
                    context.IO.WriteLine(@"Parsing error.");
                    context.IO.WriteLine("");
                    context.Command.State = ResultState.PromptForCommand;
                    break;

                case ResultState.ErrorWhileExecuting:
                    context.IO.WriteLine(@"Error while executing.");
                    context.IO.WriteLine("");
                    if (context.Configuration.DisplayExceptionDetails)
                    {
                        context.IO.WriteLine(string.Join(Environment.NewLine, context.Command.Exceptions.Select(x => x.Message + Environment.NewLine + x.StackTrace)));
                    }
                    context.IO.WriteLine("");
                    if (context.Configuration.TraceExceptions)
                    {
                        Trace.WriteLine(context.Command.Exceptions, "Exceptions");
                    }
                    context.Command.State = ResultState.PromptForCommand;
                    break;

                case ResultState.Canceled:
                    return false;

                default:
                    throw new Exception(@"Error in NCommands. Unknown state: {currentState}");
            }

            //
            return true;
        }
    }
}
