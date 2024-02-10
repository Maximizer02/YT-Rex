using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterYoutubeDownloader
{
    public class Downloader
    {
        private YesOrNo menu = new YesOrNo();
        private Editor editor = new Editor();
        public void downloadVideo()
        {
            do
            {
                ytdlp(getValidLink(), "-f mp4 -P ~/Videos/YoutubeDownloads ");
                Console.WriteLine("Download another?");
            } while (menu.displayMenu() == 0);
            Console.WriteLine();
        }

        public void downloadAudio()
        {
            do
            {
                ytdlp(getValidLink(), "-x --audio-format mp3 --embed-thumbnail --embed-metadata -P ~/Music/YoutubeDownloads ");
                Console.WriteLine("Edit Metadata?");
                if (menu.displayMenu() == 0) editor.editMetadataFromConsoleInput();
                Console.WriteLine("Download another?");
            } while (menu.displayMenu() == 0);
            Console.WriteLine();
        }

        private void ytdlp(string link, string arguments)
        {
            Process process = new Process();
            process.StartInfo.FileName = "yt-dlp";
            process.StartInfo.Arguments = arguments + " " + link;
            try
            {
                process.Start();
                process.WaitForExit();
                Console.ForegroundColor = process.ExitCode == 1 ? ConsoleColor.Red : ConsoleColor.Green;
                Console.WriteLine("Exit Code: " + process.ExitCode);
                Console.ResetColor();
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not access yt-dlp");
                Console.WriteLine("Be sure to have it installed and added to $PATH");
                Console.WriteLine("For more info look at the about section");
            }
            Console.WriteLine();
        }


        private string getValidLink()
        {
            Console.WriteLine("Enter Youtube URL here:");
            string link = Console.ReadLine();
            if (validateLink(link))
            {
                return link;
            }
            else
            {
                Console.WriteLine("Link invalid, try again");
                return getValidLink();
            }
        }

        private bool validateLink(string link)
        {
            Uri uriResult;
            return Uri.TryCreate(link, UriKind.Absolute, out uriResult)
                && uriResult.Scheme == Uri.UriSchemeHttps;
        }
    }
}
