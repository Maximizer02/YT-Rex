using BetterYoutubeDownloader;


List<string> MainMenuItems = new List<string>() { "Download Youtube Video", "Youtube Video to Mp3", "About", "Exit" };
Menu menu = new Menu();
Downloader downloader = new Downloader();
int selected;
Console.Write("Welcome to "); Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Better Youtube Downloader");

//Main Loop, goes until the last item is selected
do
{
    selected= menu.interactveMenu(MainMenuItems);
    switch (selected)
    {
        case 0:
            downloader.downloadVideo();
            break;
        case 1:
            downloader.downloadAudio();
            break;
        case 2:
            about();
            break;
        case 3:
            Environment.Exit(0);
            break;
    }
}
while(selected!=MainMenuItems.Count-1);




void about()
{
    Console.WriteLine("\nBetter Youtube Downloader Version 1.0");
    Console.WriteLine("\nProject written by Maximizer02");
    Console.WriteLine(" > Github: https://github.com/Maximizer02");
    Console.WriteLine("\nPowered by yt-dlp and FFmpeg");
    Console.WriteLine(" > Github: https://github.com/yt-dlp");
    Console.WriteLine(" > Github: https://github.com/FFmpeg\n");
    
}
