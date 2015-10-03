using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tectil.NCommand.Contract;

namespace Tectil.NCommand
{
    /// <summary>
    /// Starting point for your console.
    /// </summary>
    /// <example>
    /// static void Main(string[] args)
    /// {
    ///     var b = Consoler.Run(args);
    /// }
    /// </example>
    public static class NCommanderConsoleApp
    {
        /// <summary>
        /// IO. Do console commands on this.
        /// </summary>
        public static IoManager IO { get; set; }

        /// <summary>
        /// Run command in console.
        /// </summary>
        /// <param name="commander"></param>
        /// <param name="args"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static bool RunConsole(this NCommanderApp commander, string[] args)
        {
            // Context
            IO = new IoManager();

            // Run
            var arguments = args.ToList();
            var runner = new CommandRunner(new CommandParser(), new CommandLookup(commander.Configuration));
            var result = runner.Run(string.Join(" ", arguments));
            IO.WriteLine("> {0}", result?.CommandInfo?.CommandName);
            IO.WriteLine("");

            // Mode: Execute
            if (result.State == ResultState.Success)
            {
                return true;
            }
            if (result.State == ResultState.ErrorWhileExecuting)
            {
                return false;
            }

            // Mode: Interactive
            var cancel = false;
            List<CommandInfo> commands = null;
            while (!cancel)
            {
                CurrentState = result.State;
                switch (result.State)
                {
                    case ResultState.Undefined: // Next command
                        // Read
                        arguments.Clear();
                        var line = IO.ReadLine(() =>
                        {
                            CurrentState = ResultState.Canceled;
                            cancel = true;
                        });
                        var num = ParsePlus(line);
                        if (num > -1 && commands != null)
                        {
                            line = commands.ElementAt(num - 1).CommandName;
                            IO.WriteLine("> " + line);
                        }
                        arguments.Add(line);
                        IO.WriteLine("");
                        IO.Flush();
                        result = runner.Run(line);
                        break;

                    case ResultState.ShowHelpOverview:
                        IO.ClearWindow();
                        IO.ClearBuffer();
                        commands = result.Result as List<CommandInfo>;
                        int ii = 1;
                        IO.WriteLine("Commands available:");
                        IO.WriteLine("");
                        IO.WriteLine("/help   Command overview (this view)");
                        IO.WriteLine("[ESC]   Cancel command");
                        IO.WriteLine("");
                        commands?.ForEach(cmd =>
                        {
                            IO.WriteLine("[" + ii + "]" + string.Join(" ", new String(' ',  8 - ("[" + ii + "]").Length)) + cmd.CommandName);
                            cmd?.Arguments.ToList().ForEach(arg =>
                            {
                                IO.WriteLine(new String(' ', 8) + arg.Name + ": " + (!string.IsNullOrWhiteSpace(arg.DefaultValue?.ToString()) ? arg.DefaultValue?.ToString() : "?"));
                            });
                            IO.WriteLine("");
                            ii += 1;
                        });
                        IO.WriteLine("");
                        IO.WriteLine("Which command do you want to execute?");
                        IO.WriteLine("");
                        IO.Flush();
                        result.State = ResultState.Undefined;
                        break;                        

                    case ResultState.Success:
                        var res = result.Result;
                        if (result.Result is Task) res = result.Result.Result;
                        if (res is IEnumerable && !(res is IEnumerable<char>))
                        {
                            foreach (var item in (res as IEnumerable))
                            {
                                IO.WriteLine(item?.ToString());
                            }
                            IO.WriteLine("");
                            IO.Flush();
                        }
                        else
                        {
                            IO.WriteLine(res?.ToString());
                            IO.WriteLine("");
                            IO.Flush();
                        }
                        result.State = ResultState.Undefined;
                        break;

                    case ResultState.MissingArguments:

                        IO.WriteLine(@"{0} arguments missing. ", result.MissingArguments.Count());
                        IO.WriteLine("");
                        for (int i = 0; i < result.MissingArguments.Count(); i++)
                        {
                            var argument = result.MissingArguments.ElementAt(i);
                            if (!string.IsNullOrWhiteSpace(argument.Description))
                            {
                                IO.WriteLine($"Description: {argument.Description}");
                                IO.WriteLine($"Enter value for '{argument.Name}':");
                            }
                            else
                            {
                                IO.WriteLine($"Enter value for '{argument.Name}':");
                            }
                            var input = IO.ReadLine(() =>
                            {
                                CurrentState = ResultState.Canceled;
                                cancel = true;
                            });
                            arguments.Add("/" + argument.Name + ":" + input); // todo: ReRun (enteredValues) method on CommandRunner (Initial paramaters saved in a state)
                            IO.WriteLine("");
                        }
                        IO.WriteLine("");
                        IO.Flush();
                        result = runner.Run(string.Join(" ", arguments));
                        break;

                    case ResultState.UnknownCommand:
                        IO.WriteLine(@"Unknown command.");
                        IO.WriteLine("");
                        IO.Flush();
                        result.State = ResultState.Undefined;
                        break;

                    case ResultState.ParsingError:
                        IO.WriteLine(@"Parsing error.");
                        IO.WriteLine("");
                        IO.Flush();
                        result.State = ResultState.Undefined;
                        break;

                    case ResultState.ErrorWhileExecuting:
                        IO.WriteLine(@"Error while executing.");
                        IO.WriteLine("");
                        IO.Flush();
                        result.State = ResultState.Undefined;
                        break;

                    default:
                        throw new Exception(@"Error in NCommand.Consoler. Unknown state: {result.State}");
                }
            }
            return true;
        }

        /// <summary>
        /// Current state.
        /// </summary>
        public static ResultState CurrentState { get; private set; }


        #region Private

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
