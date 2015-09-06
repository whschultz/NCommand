using System;

namespace Tectil.NCommand.ConsoleExample
{
    static class Program
    {
        static void Main(string[] args)
        {
            var b = Consoler.Run(args);
#if DEBUG
            Consoler.IO.ReadLine(null);
#endif
        }
    }
}
