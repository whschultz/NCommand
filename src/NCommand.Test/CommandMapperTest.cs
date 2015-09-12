﻿using System.Linq;
using System.Reflection;
using Tectil.NCommand.Contract;
using Xunit;

namespace Tectil.NCommand.Test
{
    public class CommandMapperTest
    {
        [Theory]
        [InlineData("dummysearch /s:weather cupertino", ResultState.Success, null)]
        [InlineData("dummysearch /s:weather cupertino /take:11 /language:de", ResultState.Success, null)]
        [InlineData("dummysearch  /take:11 /language:de /s:weather Cupertino", ResultState.Success, "weather Cupertino")] // check sort order
        [InlineData("dummysearch /take:10 /language:de", ResultState.MissingArguments, null)] // check required arguments
        public void MappingTest(string commandLineArguments, ResultState expectedResult, string firstArgumentValue)
        {
            // Setup
            var parser = new CommandParser();
            var args = parser.Parse(commandLineArguments);
            var configuration = new CommandConfiguration();
            configuration.CommandAssemblies.Add(Assembly.GetExecutingAssembly());
            var lookup = new CommandLookup(configuration);
            var command = lookup.GetCommand(args.First().Key);
            Assert.NotNull(args);
            Assert.NotNull(command);

            // Test
            var target = new CommandMapper();
            var result = target.Map(args.Skip(1), command.Arguments);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.State == expectedResult);
            Assert.True(firstArgumentValue == null || firstArgumentValue == result?.ResultArguments?.First().ToString());
        }
    }
}
