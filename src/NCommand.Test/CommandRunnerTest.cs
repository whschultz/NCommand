using System.Reflection;
using Tectil.NCommand.Contract;
using Xunit;

namespace Tectil.NCommand.Test
{
    public class CommandRunnerTest
    {
        [Theory]
        //[InlineData("", ResultState.ShowHelpOverview, ResultState.ShowHelpOverview)]
        //[InlineData("/help", ResultState.ShowHelpOverview, ResultState.ShowHelpOverview)]
        //[InlineData("dummysearch /s:weather cupertino", ResultState.Success, ResultState.Success)]
        [InlineData(@"dummysearch /s:weather cupertino /enabled /take:10 /language:de /show", ResultState.Success, ResultState.Success)]
        //[InlineData(@"throwexceptiontask", ResultState.Success, ResultState.ErrorWhileExecuting)]
        public void RunnerTest(string commandLineArguments, ResultState expectedResultValidate, ResultState expectedResultRun)
        {
            var runner = new CommandRunner(new CommandParser(), new CommandLookup(Assembly.GetExecutingAssembly()));
            var result1 = runner.Validate(commandLineArguments);
            var result2 = runner.Run(commandLineArguments);

            // Assert
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.True(result1.State == expectedResultValidate);
            Assert.True(result2.State == expectedResultRun);
            Assert.NotNull(result1.CommandInfo);
        }
    }
}
