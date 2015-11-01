using System;
using System.Collections.Generic;
using System.Linq;
using Tectil.NCommand.Contract;
using Tectil.NCommand.ExecutionFlows.Internals;

namespace Tectil.NCommand.ExecutionFlows
{
    /// <summary>
    /// Run in interactive mode. Commands will be listed and can be executed multiple times.
    /// </summary>
    internal class RunInteractive
        : ExecutionFlowBase, IExecutionFlow
    {

        /// <summary>
        /// Runs the command in interactive mode.
        /// </summary>
        /// <param name="context">Execution context</param>
        /// <returns></returns>
        public void Run(ExecutionContext context)
        {
            bool keepon = true;
            while (keepon)
            {
                keepon = RunCommand(context);
            }
        }

        #region Private

        private new bool RunCommand(ExecutionContext context)
        {
            switch (context.Command.State)
            {
                case ResultState.PromptForCommand:
                    context.Arguments = new List<string>();
                    context.IO.WriteLine("Which command do you want to execute?");
                    context.IO.WriteLine("");
                    var cancel = false;
                    var line = context.IO.ReadLine(() => cancel = true);
                    if (cancel) return false;
                    var num = ParsePlus(line);
                    if (num > -1 && context.Commands != null)
                    {
                        line = context.Commands.ElementAt(num - 1).CommandName;
                        context.IO.ClearLastLine();
                        context.IO.WriteLine("> " + line);
                    }
                    context.Arguments.Add(line);
                    context.IO.WriteLine("");
                    context.Command = context.Runner.Run(line);
                    break;

                case ResultState.ShowHelpOverview:
                    var seperator1 = context.Configuration.Notation == ParserNotation.Windows ? '/' : '-';
                    var seperator2 = context.Configuration.Notation == ParserNotation.Windows ? ':' : '=';
                    context.Commands = context.Command.Result as List<CommandInfo>;
                    int ii = 1;
                    context.IO.WriteLine("Commands available:");
                    context.IO.WriteLine("");
                    context.IO.WriteLine("help                     Command overview (this view)");
                    context.IO.WriteLine($"{seperator1}mode                    silent - No prompting");
                    context.IO.WriteLine("[ESC]|exit|quit|cancel   Cancel command");
                    context.IO.WriteLine("");
                    context.Commands?.ForEach(cmd =>
                    {
                        context.IO.Write("[" + ii + "]" + string.Join(" ", new String(' ', 8 - ("[" + ii + "]").Length)) +
                                 cmd.CommandName);
                        cmd?.Arguments.ToList().ForEach(arg =>
                        {
                            context.IO.Write($" {seperator1}" + arg.Name + $"{seperator2}" +
                                     (!string.IsNullOrWhiteSpace(arg.DefaultValue?.ToString())
                                         ? arg.DefaultValue?.ToString()
                                         : "?"));
                        });
                        context.IO.WriteLine("");
                        ii += 1;
                    });
                    context.IO.WriteLine("");
                    context.Command.State = ResultState.PromptForCommand;
                    break;

                default:
                    return base.RunCommand(context);
            }

            //
            return true;
        }

        private static int ParsePlus(string line)
        {
            new List<string>() { ".", ")", "[", "]" }.ForEach(x => line = line.Replace(x, ""));
            line = line.Trim();
            int num;
            if (int.TryParse(line, out num)) return num;
            return -1;
        }

        #endregion

    }
}
