namespace Tectil.NCommand.Contract
{
    /// <summary>
    /// Returns the appropriate execution flow.
    /// </summary>
    public interface IExecutionFlowFactory
    {
        /// <summary>
        /// Gets the appropriate execution flow.
        /// </summary>
        /// <param name="context">Execution context.</param>
        /// <returns></returns>
        IExecutionFlow Get(ExecutionContext context);
    }
}
