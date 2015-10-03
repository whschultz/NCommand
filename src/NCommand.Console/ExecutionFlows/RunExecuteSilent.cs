using Tectil.NCommand.Contract;
using Tectil.NCommand.ExecutionFlows.Internals;

namespace Tectil.NCommand.ExecutionFlows
{
    /// <summary>
    /// Execute single command. If arguments are missing execution will be canceled.
    /// </summary>
    internal class RunExecuteSilent
        : ExecutionFlowBase, IExecutionFlow
    {
        /// <summary>
        /// Mode Execute silent. 
        /// </summary>
        /// <param name="context">Execution context</param>
        /// <returns></returns>
        public void Run(ExecutionContext context)
        {
            context.IO.WriteLine("> {0}", string.Join(" ", context.Arguments));
            context.IO.WriteLine("");
            if (context.Command?.State == ResultState.Success)
            {
                RunCommand(context);
                return;
            }
            context.IO.WriteLine("No success.");
        }
    }
}
