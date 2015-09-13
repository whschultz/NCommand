namespace Tectil.NCommand.Contract
{
    /// <summary>
    /// Command runner interface.
    /// </summary>
    public interface ICommandRunner
    {
        /// <summary>
        /// Validates if commandline input is valid.
        /// </summary>
        /// <param name="commandline">The commandline.</param>
        /// <returns></returns>
        CommandResult Validate(string commandline);

        /// <summary>
        /// Runs the specified command.
        /// </summary>
        /// <param name="commandline">The commandline.</param>
        /// <returns></returns>
        CommandResult Run(string commandline);
    }
}
