using System.Collections.Generic;

namespace NCommand.ConsoleExampleRepository
{
    /// <summary>
    /// Test repository implementation.
    /// </summary>
    public class TestRepository
        : ITestRepository
    {
        /// <summary>
        /// Gets the people.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetPeople()
        {
            return new List<string>() { "Maya", "Mary", "Peter" };
        }
    }
}
