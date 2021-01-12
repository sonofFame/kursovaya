using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Input;

namespace MemeFinder.Core
{
    public class MainViewModel:BaseViewModel
    {
        #region Public Properties
        public ObservableCollection<Meme> Memes { get; set; }

        public string MemeName { get; set; }
        public string MemeCategory { get; set; }
        public string MemeLocation { get; set; }
        public string MemeHashTag { get; set; } = "#";
        public Uri MemeUrl { get; set; }
        public string SearchString { get; set; }
        #endregion

        #region Commands
        public ICommand DeleteMemeCommand { get; set; }
        public ICommand AddNewMemeCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        #endregion

        public MainViewModel()
        {
            DataManager.LoadData();
            Memes = DataManager.Memes;
            DeleteMemeCommand = new RelayParameterizedCommand((meme) => DeleteMeme((Meme)meme));
            AddNewMemeCommand = new RelayCommand(AddMeme);
            SearchCommand = new RelayCommand(Search);
        }
        /// <summary>
        /// Поиск по названию мема,категории и хештега
        /// </summary>
        private void Search()
        {
            if (string.IsNullOrEmpty(SearchString))
            {
                Memes = null;
                Memes = DataManager.Memes;
            }
            else
            {
                Memes = null;
                Memes = new ObservableCollection<Meme>();
                foreach (var item in DataManager.Memes)
                {
                    if (item.Name.StartsWith(SearchString)
                        || item.Category.StartsWith(SearchString)
                        || item.HashTag.StartsWith(SearchString))
                        Memes.Add(item);
                }
            }
        }


        /// <summary>
        /// Функция добавляет мем или загружает его по ссылке 
        /// </summary>
        private void AddMeme()
        {
            if (!string.IsNullOrEmpty(MemeName) && !string.IsNullOrEmpty(MemeCategory))
            {
                if (!string.IsNullOrEmpty(MemeLocation))
                {
                    Meme mem = new Meme(MemeName, MemeCategory, MemeLocation);
                    if (!MemeHashTag.Equals("#"))
                    {
                        mem.HashTag = MemeHashTag;
                    }
                    Memes.Add(mem);
                }
                else if (!string.IsNullOrEmpty(MemeUrl.ToString()))
                {
                    SelectPathDownload();
                    bool res = Meme.DownloadMem(MemeUrl, MemeName);
                    if (res)
                    {
                        MemeLocation = Meme.filePath + '\\'+ MemeName + ".png";
                        Meme mem = new Meme(MemeName, MemeCategory, MemeLocation);
                        if (!MemeHashTag.Equals("#"))
                        {
                            mem.HashTag = MemeHashTag;
                        }
                        Memes.Add(mem);
                    }
                   
                }
                MakeCurrentMemeEmpty();
                DataManager.SaveData();

            }
        }
        /// <summary>
        /// Функция удаляет выбранный мем
        /// </summary>
        /// <param name="meme">Выбранный мем</param>
        private void DeleteMeme(Meme meme)
        {
            Memes.Remove(meme);
            try
            {
                DataManager.Memes.Remove(meme);
            }
            catch { }
            DataManager.SaveData();
            File.Delete(meme.Source);

        }

        /// <summary>
        /// Очищает текстбоксы,в которых хранится информация о загружаемом меме
        /// </summary>
        private void MakeCurrentMemeEmpty()
        {
            MemeCategory = string.Empty;
            MemeName = string.Empty;
            MemeLocation = string.Empty;
        }
        /// <summary>
        /// Открытие диалогового окна для скачивания мема по ссылке
        /// </summary>
        private void SelectPathDownload()
        {
            var dlg = new CommonOpenFileDialog
            {
                Title = "My Title",
                IsFolderPicker = true,
                InitialDirectory = Meme.filePath,

                AddToMostRecentlyUsedList = false,
                AllowNonFileSystemItems = false,
                DefaultDirectory = Meme.filePath,
                EnsureFileExists = true,
                EnsurePathExists = true,
                EnsureReadOnly = false,
                EnsureValidNames = true,
                Multiselect = false,
                ShowPlacesList = true
            };

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var folder = dlg.FileName;
                Meme.filePath = folder;
            }
        }

    }
}
