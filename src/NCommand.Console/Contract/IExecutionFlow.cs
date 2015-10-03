namespace Tectil.NCommand.Contract
{
    /// <summary>
    /// Implements an execution flow. Interactive, execute, silent,..
    /// </summary>
    public interface IExecutionFlow
    {
        /// <summary>
        /// Run
        /// </summary>
        /// <param name="context">Execution context</param>
        void Run(ExecutionContext context);
    }
}
