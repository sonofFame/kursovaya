using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace MemeFinder.Core
{
    static class DataManager
    {
        static readonly string fileName ="../../../Data/Data.json";

        public static ObservableCollection<Meme> Memes { get; set; }
        
        public class Data
        {
            public ObservableCollection<Meme> Memes { get; set; }

        }
        /// <summary>
        /// Функция загружает данные в память программы 
        /// </summary>
        public static void LoadData()
        {
            var data = Deserialize<Data>(fileName);
            if (data != null)
                Memes = data.Memes;
            else
                Memes = new ObservableCollection<Meme>();
        }
        /// <summary>
        /// Функция сохраняет данные в json файл
        /// </summary>
        public static void SaveData()
        {
            var data = new Data
            {
                Memes = DataManager.Memes
            };
            Serialize(fileName, data);
        }
        /// <summary>
        /// Функция которая читает файл  
        /// </summary>
        /// <typeparam name="T"> Тип данных поданых который нужно вернуть </typeparam>
        /// <param name="filename">Название файла,который нужно прочитать</param>
        /// <returns></returns>
        private static T Deserialize<T>(string filename)  
        {
            using var sr = new StreamReader(filename);
            using var jsonReader = new JsonTextReader(sr);
            var serializer = new JsonSerializer();
            return serializer.Deserialize<T>(jsonReader);
        }
        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <typeparam name="T">тип данных,которые нужно сохранить</typeparam>
        /// <param name="fileName">Название файла,который нужно прочитать</param>
        /// <param name="data">какие данные нужно сохранить</param>
        private static void Serialize<T>(string fileName, T data) 
        {
            using var sw = new StreamWriter(fileName);
            using var jsonWriter = new JsonTextWriter(sw);
            var serializer = new JsonSerializer();
            serializer.Serialize(jsonWriter, data);
        }
    }
}
