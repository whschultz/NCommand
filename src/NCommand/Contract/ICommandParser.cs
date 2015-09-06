﻿using System.Collections.Generic;

namespace Tectil.NCommand.Contract
{
    public interface ICommandParser
    {
        /// <summary>
        /// Parse.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        IEnumerable<KeyValuePair<string, object>> Parse(string command);

    }
}
