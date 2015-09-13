using System.Collections.Generic;

namespace NCommand.ConsoleExampleRepository
{
    /// <summary>
    /// Test repository interface.
    /// </summary>
    public interface ITestRepository
    {
        /// <summary>
        /// Gets the people.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetPeople();
    }
}
