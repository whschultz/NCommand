using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace Tectil.NCommand.Test
{
    public class CommandParserTest
    {
        [Theory]
        // Should parse
        [InlineData(@"websearch /s:weather /flagon", true)]
        [InlineData(@"websearch /s:weather", true)]
        [InlineData(@"websearch /s:weather cupertino /take:10 /language:de", true)]
        [InlineData(@"websearch /s:", true)]
        [InlineData(@"", true)]
        [InlineData(@"getpath /path:$DummyApp/CopyTest/SourceA", true)]
        // Should not parse
        [InlineData(@"/s:weather cupertino /take:10 /language:de", false)]
        [InlineData(@"aa dsf /s:weather", false)]
        [InlineData(@"dsf /s d:weather", false)]
        public void ParsingTest(string command, bool resultShouldParse)
        {
            var target = new CommandParser();
            var commandTree = target.Parse(command);

            // Assert
            if (resultShouldParse)
            {
                Assert.True(commandTree.Any());
            }
            else
            {
                Assert.False(commandTree.Any());
            }
        }

        [Theory]
        [InlineData("getpath /path:$DummyApp/CopyTest/SourceA", "(/[a-z,A-Z]+:)")]
        public void RegexTest(string text, string regex)
        {
            var result = Regex.Split(text, regex);
            Assert.NotNull(result);
        }
    }
}
