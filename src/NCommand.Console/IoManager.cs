using System;
using System.IO;
using System.Text;

namespace Tectil.NCommand
{
    /// <summary>
    /// Input and output to console. All commands are buffered and need to be flushed to be displayed.
    /// </summary>
    public sealed class IoManager
    {

        #region ctx

        private StringWriter _consoleOut;
        private readonly TextWriter _consoleOutDefault;

        /// <summary>
        /// Initializes a new instance of the <see cref="IoManager"/> class.
        /// </summary>
        public IoManager()
        {
            _consoleOutDefault = Console.Out;
            ClearBuffer(); // Clear cache
        }

        #endregion
        
        /// <summary>
        /// Write line.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="args"></param>
        public void WriteLine(string value, params object[] args)
        {
            if (value == null) return;
            Console.WriteLine(value, args);
        }

        /// <summary>
        /// Write to consule.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="args">The arguments.</param>
        public void Write(string value, params object[] args)
        {
            if (value == null) return;
            Console.Write(value, args);
        }

        /// <summary>
        /// Read line.
        /// </summary>
        public string ReadLine(Action fEscPressed = null)
        {
            Write("> ");
            Flush(); // Flush before read.
            var inp = ReadLineWithEsc();
            if (inp.Item2 == ConsoleKey.Escape && fEscPressed != null)
            {
                fEscPressed();
            }
            Console.WriteLine();
            return inp.Item1;
        }
        
        /// <summary>
        /// Flush
        /// </summary>
        public void Flush()
        {
            Console.SetOut(_consoleOutDefault);
            Console.Write(_consoleOut.ToString());
            Console.SetOut(_consoleOut);
            ClearBuffer(); // Clear cache
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void ClearBuffer()
        {
            _consoleOut = new StringWriter();
            Console.SetOut(_consoleOut);
        }

        /// <summary>
        /// Clears the window.
        /// </summary>
        public void ClearWindow()
        {
            Console.Clear();
        }

        #region Private

        private Tuple<string, ConsoleKey> ReadLineWithEsc()
        {
            var txt = new StringBuilder();
            ConsoleKeyInfo lastChar;
            do
            {
                lastChar = Console.ReadKey(true);
                if (lastChar.Key == ConsoleKey.Escape) // Exit
                {
                    Console.Write(Environment.NewLine);
                    Flush();
                    break;
                }
                if (lastChar.Key == ConsoleKey.Backspace)
                {
                    if (txt.Length > 0)
                    {
                        Console.Write(lastChar.KeyChar);
                        Console.Write(" ");
                        Console.Write(lastChar.KeyChar);
                        txt.Length--;
                    }
                }
                if (lastChar.KeyChar >= 32)
                {
                    Console.Write(lastChar.KeyChar);
                    txt.Append(lastChar.KeyChar);
                }
                Flush();
            } while (lastChar.Key != ConsoleKey.Enter); // Exit
            return new Tuple<string, ConsoleKey>(txt.ToString(), lastChar.Key); 
        }

        #endregion

    }
}
