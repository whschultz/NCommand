using Tectil.NCommand.Contract;
using Tectil.NCommand.ExecutionFlows.Internals;

namespace Tectil.NCommand.ExecutionFlows
{
    /// <summary>
    /// Execute single command. Missing arguments will be asked for.
    /// </summary>
    internal class RunExecute
        : ExecutionFlowBase, IExecutionFlow
    {
        /// <summary>
        /// Mode Execute. Not silent, missing parameters will be asked.
        /// </summary>
        /// <param name="context">Execution context</param>
        /// <returns></returns>
        public void Run(ExecutionContext context)
        {
            context.IO.WriteLine("> {0}", string.Join(" ", context.Arguments));
            context.IO.WriteLine("");

            bool keepon = true;
            while (keepon)
            {
                keepon = RunCommand(context);
                if (context.Command != null && context.Command.State == ResultState.PromptForCommand) return;
            }
        }
    }
}
