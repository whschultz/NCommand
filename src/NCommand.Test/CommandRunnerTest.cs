using System.Reflection;
using Tectil.NCommand.Contract;
using Xunit;

namespace Tectil.NCommand.Test
{
    public class CommandRunnerTest
    {
        [Theory]
        //[InlineData("", ResultState.ShowHelpOverview, ResultState.ShowHelpOverview, ParserNotation.Windows)]
        //[InlineData("/help", ResultState.ShowHelpOverview, ResultState.ShowHelpOverview, ParserNotation.Windows)]
        //[InlineData("dummysearch /s:weather cupertino", ResultState.Success, ResultState.Success, ParserNotation.Windows)]
        [InlineData(@"dummysearch /s:weather cupertino /enabled /take:10 /language:de /show", ResultState.Success, ResultState.Success, ParserNotation.Windows)]
        //[InlineData(@"throwexceptiontask", ResultState.Success, ResultState.ErrorWhileExecuting, ParserNotation.Windows)]
        //[InlineData("", ResultState.ShowHelpOverview, ResultState.ShowHelpOverview, ParserNotation.Unix)]
        //[InlineData("-help", ResultState.ShowHelpOverview, ResultState.ShowHelpOverview, ParserNotation.Unix)]
        //[InlineData("dummysearch -s=weather cupertino", ResultState.Success, ResultState.Success, ParserNotation.Unix)]
        [InlineData(@"dummysearch -s=weather cupertino -enabled -take=10 -language=de -show", ResultState.Success, ResultState.Success, ParserNotation.Unix)]
        //[InlineData(@"throwexceptiontask", ResultState.Success, ResultState.ErrorWhileExecuting, ParserNotation.Unix)]
        public void RunnerTest(string commandLineArguments, ResultState expectedResultValidate, ResultState expectedResultRun, ParserNotation notation)
        {
            var configuration = new CommandConfiguration();
            configuration.CommandAssemblies.Add(Assembly.GetExecutingAssembly());
            var runner = new CommandRunner(new CommandParser(notation), new CommandLookup(configuration));
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
