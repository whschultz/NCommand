namespace Tectil.NCommand
{
    /// <summary>
    /// Entry point for NCommand clients (eg. NCommand.Console)
    /// </summary>
    public class NCommands
    {
        /// <summary>
        /// Initializes the <see cref="NCommands"/> class.
        /// </summary>
        public NCommands()
        {
            Context = new NCommanderContext();
        }

        /// <summary>
        /// Application object.
        /// </summary>
        /// <value>
        /// The application.
        /// </value>
        public NCommanderContext Context { get; }
    }
}
