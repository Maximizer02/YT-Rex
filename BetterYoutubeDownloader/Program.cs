using System.Diagnostics;

List<string> MainMenu = new List<string>() { "Download Youtube Video", "Youtube Video to Mp3", "About", "Exit" };

Console.Write("Welcome to "); Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Better Youtube Downloader");

int selected;
do
{
selected= interactveMenu(MainMenu);
    switch (selected)
    {
        case 0:
            downloadVideo();
            break;
        case 1:
            downloadAudio();
            break;
        case 2:
            about();
            break;
        case 3:
            Environment.Exit(0);
            break;
    }
}while(selected!=MainMenu.Count-1);


void writeMenu(List<string> options, int i, bool overwrite)
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

int interactveMenu(List<string> options)
{
    int index = 0;
    writeMenu(options, index,false);
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

            writeMenu(options, index,true);
        }
        if (keyinfo.Key == ConsoleKey.UpArrow)
        {
            index--;
            if (index < 0)
            {
                index = options.Count - 1;
            }

            writeMenu(options, index,true);
        }

    } while (keyinfo.Key != ConsoleKey.Enter);
    return index;
}


void downloadVideo() 
{
    ytdlp(getValidLink(), "-f mp4 -P ~/Videos/YoutubeDownloads ");
}

void downloadAudio() 
{
    ytdlp(getValidLink(), "-x --audio-format mp3 -P ~/Music/YoutubeDownloads ");
}

void ytdlp(string link, string arguments) {
    
    Process process = new Process();
    process.StartInfo.FileName = "yt-dlp";
    process.StartInfo.Arguments = arguments + " " + link;
    process.Start();
    process.WaitForExit();
}

bool validateLink(string link)
{
    Uri uriResult;
    return  Uri.TryCreate(link, UriKind.Absolute, out uriResult)
        && uriResult.Scheme == Uri.UriSchemeHttps;
}

string getValidLink() 
{
    Console.WriteLine("Enter Youtube URL here:");
    string link = Console.ReadLine();
    if(validateLink(link))
    {
        return link;
    }
    else 
    {
        Console.WriteLine("Link invalid, try again");
        return getValidLink(); 
    }
}
void about()
{
    Console.WriteLine("\nProject written by Maximizer02");
    Console.WriteLine(" > Github: https://github.com/Maximizer02");
    Console.WriteLine("Powered by yt-dlp and FFmpeg");
    Console.WriteLine(" > Github: https://github.com/yt-dlp");
    Console.WriteLine(" > Github: https://github.com/FFmpeg\n");
    
}
