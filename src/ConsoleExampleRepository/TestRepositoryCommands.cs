using System.Collections.Generic;
using Tectil.NCommand.Contract;

namespace NCommand.ConsoleExampleRepository
{
    /// <summary>
    /// Commands
    /// </summary>
    public class TestRepositoryCommands
    {
        private readonly ITestRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRepositoryCommands"/> class.
        /// </summary>
        public TestRepositoryCommands()
        {
            _repository = new TestRepository();
        }

        /// <summary>
        /// Get people command
        /// </summary>
        /// <returns></returns>
        [Command()]
        public IEnumerable<string> GetPeople()
        {
            return _repository.GetPeople();
        }
    }
}
