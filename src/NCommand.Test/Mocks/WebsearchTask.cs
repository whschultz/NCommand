using System;
using Tectil.NCommand.Contract;

namespace Tectil.NCommand.Test.Mocks
{
    public class WebsearchTask
    {

        [Command ()]
        public string DummySearch(
            [Argument(description: "Search string")] string s,
            [Argument(defaultValue: 10)] int take,
            [Argument(defaultValue: "de")] string language
            )
        {
            return string.Format("Website requested. {0}, {1}, {2}", s, take, language);
        }

        [Command(name: "ThrowExceptionTask")]
        public static string ThrowExceptionTask2()
        {
            throw new Exception("Exception by purpose.");
        }
        
    }
}
