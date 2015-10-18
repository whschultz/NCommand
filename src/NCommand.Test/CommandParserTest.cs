using System.Linq;
using System.Text.RegularExpressions;
using Tectil.NCommand.Contract;
using Xunit;

namespace Tectil.NCommand.Test
{
    public class CommandParserTest
    {
        [Theory]
        // Should parse
        [InlineData(@"websearch /s:weather /flagon", true, ParserNotation.Windows)]
        [InlineData(@"websearch /s:weather", true, ParserNotation.Windows)]
        [InlineData(@"websearch /s:weather cupertino /take:10 /language:de", true, ParserNotation.Windows)]
        [InlineData(@"websearch /s:", true, ParserNotation.Windows)]
        [InlineData(@"", true, ParserNotation.Windows)]
        [InlineData(@"getpath /path:$DummyApp/CopyTest/SourceA", true, ParserNotation.Windows)]
        [InlineData(@"websearch -s=weather -flagon", true, ParserNotation.Unix)]
        [InlineData(@"websearch -s=weather", true, ParserNotation.Unix)]
        [InlineData(@"websearch -s=weather cupertino -take=10 -language=de", true, ParserNotation.Unix)]
        [InlineData(@"websearch -s=", true, ParserNotation.Unix)]
        [InlineData(@"", true, ParserNotation.Unix)]
        [InlineData(@"getpath -path=$DummyApp-CopyTest-SourceA", true, ParserNotation.Unix)]
        // Should not parse
        [InlineData(@"/s:weather cupertino /take:10 /language:de", false, ParserNotation.Windows)]
        [InlineData(@"aa dsf /s:weather", false, ParserNotation.Windows)]
        [InlineData(@"dsf /s d:weather", false, ParserNotation.Windows)]
        public void ParsingTest(string command, bool resultShouldParse, ParserNotation notation)
        {
            var target = new CommandParser(notation);
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
