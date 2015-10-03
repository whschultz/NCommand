using System.Linq;
using Tectil.NCommand.Contract;

namespace Tectil.NCommand.ExecutionFlows.Internals
{
    /// <summary>
    /// Returns the appropriate execution flow.
    /// </summary>
    public class ExecutionFlowFactory
        : IExecutionFlowFactory
    {
        /// <summary>
        /// Gets the appropriate execution flow.
        /// </summary>
        /// <param name="context">Execution context.</param>
        /// <returns></returns>
        public IExecutionFlow Get(ExecutionContext context)
        {
            // Silent mode
            if (context.Arguments.Any(x => x.ToLower() == "/mode:silent"))
            {
                return new RunExecuteSilent();
            }

            // Interactive interactive
            if (context.Command.State == ResultState.ShowHelpOverview)
            {
                return new RunInteractive();
            }

            // Execution mode 
            return new RunExecute();
        }
    }
}
