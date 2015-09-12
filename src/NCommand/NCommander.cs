namespace Tectil.NCommand
{
    /// <summary>
    /// Entry point for NCommand clients (eg. NCommand.Console)
    /// </summary>
    public static class NCommander
    {
        /// <summary>
        /// Initializes the <see cref="NCommander"/> class.
        /// </summary>
        static NCommander()
        {
            App = new NCommanderApp();
        }

        /// <summary>
        /// Application object.
        /// </summary>
        /// <value>
        /// The application.
        /// </value>
        public static NCommanderApp App { get; }
    }
}
