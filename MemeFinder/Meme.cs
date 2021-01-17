using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace MemeFinder.Core
{
    public class Meme
    {
        public Meme(string name, string category, string source)
        {
            Name = name;
            Category = category;
            Source = source;
        }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Source { get; set; }
        public string HashTag { get; set; }
        public static string filePath = "../../../../Memes/"; 
        /// <summary>
        /// Функция скачивания мема из интернета по URL
        /// </summary>
        /// <param name="url">Ссылка на фотографию</param>
        /// <param name="filename">название файла</param>
        /// <returns></returns>
        public static bool DownloadMem(Uri url,string filename)
        {
            using WebClient client = new WebClient();
            try
            {
                client.DownloadFile(url, filePath +'\\' + filename + ".png");
                return true;
            }
            catch { return false; }


        }
    }
}
