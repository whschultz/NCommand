namespace Tectil.NCommand.Contract
{
    /// <summary>
    /// Result state for command runner.
    /// </summary>
    public enum ResultState
    {
        Undefined,
        Success,
        MissingArguments,
        UnknownCommand,
        ParsingError,
        ErrorWhileExecuting, // error inside command
        Canceled,
        ShowHelpOverview
    }
}
