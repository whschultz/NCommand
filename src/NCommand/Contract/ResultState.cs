namespace Tectil.NCommand.Contract
{
    /// <summary>
    /// Result state for command runner.
    /// </summary>
    public enum ResultState
    {
        /// <summary>
        /// Undefined state.
        /// </summary>
        Undefined,

        /// <summary>
        /// Successfully executed.
        /// </summary>
        Success,

        /// <summary>
        /// Required arguments are missing.
        /// </summary>
        MissingArguments,

        /// <summary>
        /// Command name provided but not found.
        /// </summary>
        UnknownCommand,

        /// <summary>
        /// Command name has not been specified (usually no args passed).
        /// </summary>
        SpecifyCommand,

        /// <summary>
        /// Parsing errors.
        /// </summary>
        ParsingError,

        /// <summary>
        /// Error inside custom command code.
        /// </summary>
        ErrorWhileExecuting, // error inside command

        /// <summary>
        /// Command has been canceled.
        /// </summary>
        Canceled,

        /// <summary>
        /// Request overview.
        /// </summary>
        ShowHelpOverview,

        /// <summary>
        /// Prompt for command.
        /// </summary>
        PromptForCommand
    }
}
