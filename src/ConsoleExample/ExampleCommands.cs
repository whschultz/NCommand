using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tectil.NCommand.Contract;

namespace Tectil.NCommand.ConsoleExample
{
    /// <summary>
    /// Example commands.
    /// </summary>
    public class ExampleCommands
    {

        /// <summary>
        /// Dummy search command.
        /// </summary>
        /// <example>
        /// ConsoleExample.exe DummySearch /s:Weather zurich
        /// ConsoleExample.exe DummySearch 
        /// ConsoleExample.exe DummySearch /s:Weather zurich /language:de /take:11
        /// </example>
        /// <param name="s"></param>
        /// <param name="language"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [Command ()]
        public string DummySearch(
            [Argument(description: "Search string")] string s,
            string language,
            [Argument(defaultValue: 10)] int take
            )
        {
            return $"Website requested with parameters '{s}', {take}, '{language}'";
        }

        /// <summary>
        /// Throw exception command.
        /// </summary>
        /// <example>
        /// ConsoleExample.exe ThrowExceptionTask
        /// </example>
        /// <returns></returns>
        [Command(name: "ThrowExceptionTask")]
        public string ThrowExceptionTask2()
        {
            throw new Exception("Exception by purpose.");
        }

        /// <summary>
        /// Throw exception command.
        /// </summary>
        /// <example>
        /// ConsoleExample.exe HelloWorld /myname:Bill
        /// </example>
        /// <returns></returns>
        [Command()]
        public void HelloWorld(string myName)
        {
            NCommanderConsoleApp.IO.WriteLine("Hello World. My name is {0}.", myName);
        }

        /// <summary>
        /// List of fruits.
        /// </summary>
        /// <returns></returns>
        [Command()]
        public List<string> ListOfFruits()
        {
            return new List<string>() { "Apple", "Orange", "Banana" };
        }


        /// <summary>
        /// List of fruits.
        /// </summary>
        /// <returns></returns>
        [Command()]
        public Task<List<string>> ListOfAnimals()
        {
            return Task.FromResult(new List<string>() { "Cat", "Dog", "Ant" });
        }
    }
}
