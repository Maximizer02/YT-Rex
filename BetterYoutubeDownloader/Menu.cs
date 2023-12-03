using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterYoutubeDownloader
{
    public class Menu
    {
        public int interactveMenu(List<string> options)
        {
            int index = 0;
            writeMenu(options, index, false);
            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();

                if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    index++;
                    if (index > options.Count - 1)
                    {
                        index = 0;
                    }
                    writeMenu(options, index, true);
                }
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    index--;
                    if (index < 0)
                    {
                        index = options.Count - 1;
                    }
                    writeMenu(options, index, true);
                }

            } while (keyinfo.Key != ConsoleKey.Enter);
            return index;
        }


        private void writeMenu(List<string> options, int i, bool overwrite)
        {
            if (overwrite)
            {
                Console.SetCursorPosition(0, Console.CursorTop - options.Count);
            }
            foreach (string option in options)
            {
                if (option.Equals(options[i]))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(">" + option);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.WriteLine(" " + option);
                }
            }
        }

        
    }
}
