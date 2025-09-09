using System.Diagnostics;
using System.Text.RegularExpressions;
using YTRex.UI;

namespace YTRex
{
    public class Downloader
    {
        private YesOrNo menu = new YesOrNo();
        private Editor editor = new Editor();
        public void downloadVideo()
        {
            do
            {
                string youtubeFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Videos\YoutubeDownloads";
                string[] alreadyDownloadedMedia = Directory.GetFiles(youtubeFolder);
                ytdlp(getValidLink(), "-f mp4 --restrict-filenames -P ~/Videos/YoutubeDownloads ");
                sanitizeFilenameAndEdit(youtubeFolder, alreadyDownloadedMedia, false);

                Console.WriteLine("Download another?");
            } while (menu.displayMenu() == 0);
            Console.WriteLine();
        }

        public void downloadAudio()
        {
            do
            {
                string youtubeFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Music\YoutubeDownloads";
                string[] alreadyDownloadedMedia = Directory.GetFiles(youtubeFolder);
                ytdlp(getValidLink(), "-x --audio-format mp3 --restrict-filenames --embed-thumbnail --embed-metadata -P ~/Music/YoutubeDownloads ");
                sanitizeFilenameAndEdit(youtubeFolder, alreadyDownloadedMedia, true);
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

        private void sanitizeFilenameAndEdit(string youtubeFolder, string[] alreadyDownloadedMedia, bool edit)
        {
            try
            {
                string[] alreadyDownloadedMediaIncludingNew = Directory.GetFiles(youtubeFolder);
                string newFileName = alreadyDownloadedMediaIncludingNew.Where(x => !alreadyDownloadedMedia.Contains(x)).First();
                string newFileNameSanitized = Regex.Replace(newFileName, @"\[(.*?)\]", "");
                File.Move(newFileName, newFileNameSanitized);
                if (edit)
                {
                    Console.WriteLine("Edit Metadata?");
                    if (menu.displayMenu() == 0) editor.editMetadataFromPath(newFileNameSanitized);
                }
            }
            catch (IOException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cannot sanitize file name, file already exists!");
                Console.ResetColor();
            }
            catch (InvalidOperationException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("File was already downloaded");
                Console.ResetColor();
            }
        }
    }
}
