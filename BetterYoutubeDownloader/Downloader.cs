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
        public void downloadVideo()
        {
            ytdlp(getValidLink(), "-f mp4 -P ~/Videos/YoutubeDownloads ");
        }

        public void downloadAudio()
        {
            ytdlp(getValidLink(), "-x --audio-format mp3 --embed-thumbnail -P ~/Music/YoutubeDownloads ");
        }

        private void ytdlp(string link, string arguments)
        {

            Process process = new Process();
            process.StartInfo.FileName = "yt-dlp";
            process.StartInfo.Arguments = arguments + " " + link;
            process.Start();
            process.WaitForExit();
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
