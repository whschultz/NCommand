using System;
using System.Text;

namespace Tectil.NCommand
{
    /// <summary>
    /// Utility functions for console
    /// </summary>
    public class ConsoleUtility
    {
        #region Static
        /// <summary>
        /// Static instance.
        /// </summary>
        /// <value>
        /// The io.
        /// </value>
        public static ConsoleUtility Instance
        {
            get
            {
                _instance = _instance ?? new ConsoleUtility();
                return _instance;
            }
        }
        private static ConsoleUtility _instance;
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
        /// Read line with trigger for ESC.
        /// </summary>
        public string ReadLine(Action fEscPressed = null)
        {
            Write("> ");
            var inp = ReadLineWithEsc();
            if (inp.Item2 == ConsoleKey.Escape && fEscPressed != null)
            {
                fEscPressed();
            }
            Console.WriteLine();
            return inp.Item1;
        }

        /// <summary>
        /// Clears the last line.
        /// </summary>
        public void ClearLastLine()
        {
            if (Console.CursorTop == 0) return;
            ClearLine(Console.CursorTop - 1, true);
        }

        /// <summary>
        /// Clears the specified line.
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="moveToClearedLine">if set to <c>true</c> [move to cleared line].</param>
        public void ClearLine(int top, bool moveToClearedLine = false)
        {
            if (top < 0) return;
            var topLast = Console.CursorTop;
            Console.SetCursorPosition(0, top);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, moveToClearedLine ? top : topLast);
        }

        /// <summary>
        /// Clears the window.
        /// </summary>
        public void Clear()
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
            } while (lastChar.Key != ConsoleKey.Enter); // Exit
            return new Tuple<string, ConsoleKey>(txt.ToString(), lastChar.Key);
        }

        #endregion

    }
}
