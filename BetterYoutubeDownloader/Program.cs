using BetterYoutubeDownloader;


List<string> MainMenuItems = new List<string>() {  "Youtube Video to Mp3","Download Youtube Video", "Edit Metadata", "About", "Exit" };
Menu menu = new Menu(MainMenuItems);
Downloader downloader = new Downloader();
Editor editor= new Editor();
int selected;
Console.Write("Welcome to "); Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Better Youtube Downloader");

//Main Loop, goes until the last item is selected
do
{
    selected= menu.displayMenu();
    switch (selected)
    {
        case 0:
            downloader.downloadAudio();
            break;
        case 1:
            downloader.downloadVideo();
            break;
        case 2:
            editor.editMetadataFromConsoleInput();
            break;
        case 3:
            about();
            break;
    }
}
while(selected!=MainMenuItems.Count-1);




void about()
{
    Console.WriteLine("\nBetter Youtube Downloader Version 1.2");
    Console.WriteLine("\nProject written by Maximizer02");
    Console.WriteLine(" > Github: https://github.com/Maximizer02");
    Console.WriteLine("\nPowered by yt-dlp, FFmpeg and ID3.NET");
    Console.WriteLine(" > Github: https://github.com/yt-dlp");
    Console.WriteLine(" > Github: https://github.com/FFmpeg");
    Console.WriteLine(" > Github: https://github.com/JeevanJames/Id3\n");
}
