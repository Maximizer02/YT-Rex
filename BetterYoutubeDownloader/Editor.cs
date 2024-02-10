using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Id3;
using Id3.Frames;

namespace BetterYoutubeDownloader
{
    public class Editor
    {
        YesOrNo editMoreDatapoints = new YesOrNo();
        
        public void editMetadataFromConsoleInput()
        {

            Console.WriteLine("Enter file name or path:");
            string filePath = Console.ReadLine();
            editMetadata(filePath);
    
        }
        //Want to make it so that when you download a file you can edit the metadata directly by passing the file path into this method.
        //But idk how to get the path from yt-dlp
        public void editMetadataFromPath(string path)
        {

            Console.WriteLine("Enter file name or path:");
            editMetadata(path);

        }


        private void editMetadata(string filePath) 
        {
            Mp3 file;
            try
            {

                file = accessFile(filePath);

                //Idunno if IOException is really corrent here but I didn't know what else to pick as there is no specific Exception for this Situation lol
                if (!file.HasTags) { file.Dispose(); throw new IOException("The selected file has no Tags!"); }
                Id3Tag tag = file.GetTag(Id3TagFamily.Version2X);

                List<string> EditMetadataMenuOptions = new List<string>()
                    {
                     "Title: "+(!tag.Title.IsAssigned?"N/A":tag.Title),
                     "Artist: "+(!tag.Artists.IsAssigned?"N/A":tag.Artists),
                     "Album: "+(!tag.Album.IsAssigned?"N/A":tag.Album),
                     "Year: "+(!tag.Year.IsAssigned?"N/A":tag.Year),
                     "Genre: "+(!tag.Genre.IsAssigned?"N/A":tag.Genre),
                     "Nothing really"
                    };
                Menu editMetadataMenu = new Menu(EditMetadataMenuOptions);


                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nFile found.");
                Console.ResetColor();
                Console.Write(" What do you want to edit?\n");
                do
                {
                    switch (editMetadataMenu.displayMenu())
                    {
                        case 0: Console.WriteLine("Please enter new Title:"); tag.Title = Console.ReadLine(); break;
                        case 1:
                            Console.WriteLine("Please enter new Artist name:");
                            string newArtist = Console.ReadLine();
                            if (tag.Artists.Value.Count == 0)
                                tag.Artists.Value.Add(newArtist);
                            else
                                tag.Artists.Value[0] = newArtist;
                            break;
                        case 2: Console.WriteLine("Please enter new Album name:"); tag.Album = Console.ReadLine(); break;
                        case 3: Console.WriteLine("Please enter new Year:"); tag.Year.Value = int.Parse(Console.ReadLine()); break;
                        case 4: Console.WriteLine("Please enter new Genre:"); tag.Genre = Console.ReadLine(); break;
                    }
                    Console.WriteLine("\nEdit another datapoint?");
                }
                while (editMoreDatapoints.displayMenu() == 0);


                file.WriteTag(tag);
                file.Dispose();
                Console.WriteLine();

            }
            catch (FormatException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a correct number!\n");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message + "\n");
                Console.ResetColor();
            }
        }

        private Mp3 accessFile(string file)
        {
            string current = Directory.GetCurrentDirectory();

            //check if 'file' is valid path
            if (File.Exists(file)) return new Mp3(file, Mp3Permissions.ReadWrite);

            //check if 'file' is a valid file in current directory
            if (File.Exists(current + @"\"+file)) return new Mp3(current + @"\" + file, Mp3Permissions.ReadWrite);

            //check if 'file' is the name(without extension) of a valid file in current directory
            List<string> possibleFiles = Directory.GetFiles(current, "*.mp3")
                .Where(f => f.Substring(current.Length + 1).Split('.')[0].Equals(file))
                .ToList();
            if (possibleFiles.Count > 0)  return new Mp3(possibleFiles.First(), Mp3Permissions.ReadWrite); 

            throw new FileNotFoundException("This file does not exist!");
        }
       
    }
}
