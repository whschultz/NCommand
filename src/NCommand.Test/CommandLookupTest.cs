using System.Linq;
using System.Reflection;
using Xunit;

namespace Tectil.NCommand.Test
{
    public class CommandLookupTest
    {
        [Theory]
        [InlineData()]
        public void LookupTest()
        {
            var configuration = new CommandConfiguration();
            configuration.CommandAssemblies.Add(Assembly.GetExecutingAssembly());
            var target = new CommandLookup(configuration);
            var commandRepository = target.Commands;
            //target.Run(commandRepository.First(), new object[]{ "", 4, "de" });
            //Assert.NotNull(commandRepository);
        }

    }
}
