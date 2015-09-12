using System.Collections.Generic;

namespace NCommand.ConsoleExampleRepository
{
    public class TestRepository
        : ITestRepository
    {
        public IEnumerable<string> GetPeople()
        {
            return new List<string>() { "Maya", "Mary", "Peter" };
        }
    }
}
