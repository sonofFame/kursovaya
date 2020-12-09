using System;
using System.Net;

namespace kursovaya.Core
{
    public class Cmem
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Source { get; set; }
        public string HashTag { get; set; }

        public static string filePath = "../../../../Memes/";

        public Cmem(string name, string category, string source)
        {
            Name = name;
            Category = category;
            Source = source;
        }

        //функция скачивания мема из интернета по URL
        public static bool DownloadMem(Uri url, string filename)
        {
            using WebClient client = new WebClient();
            try
            {
                client.DownloadFile(url, filePath + '\\' + filename + ".png");
                return true;
            }
            catch { return false; }
        }

    }
}
