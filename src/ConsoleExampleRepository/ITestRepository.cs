using System.Collections.Generic;

namespace NCommand.ConsoleExampleRepository
{
    public interface ITestRepository
    {
        IEnumerable<string> GetPeople();
    }
}
