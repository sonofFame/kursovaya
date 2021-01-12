using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Input;

namespace kursovaya.Core
{
    public class CmainViewModel
    {
        public ObservableCollection<Cmem> Mems { get; set; }

        public string MemName { get; set; }
        public string MemCategory { get; set; }
        public string MemLocation { get; set; }

        public string MemHashTag { get; set; } = "#";
        public Uri MemUrl { get; set; }
        public string SearchString { get; set; }

        public ICommand DeleteMemCommand { get; set; }
        public ICommand AddNewMemCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public CmainViewModel()
        {
            CDataManager.LoadData();
            Mems = CDataManager.Memes;
            AddNewMemCommand = new CRelayCommands(AddMeme);
            SearchCommand = new CRelayCommands(Search);
            DeleteMemCommand = new CRelayParametrizedCommands((mem) => DeleteMeme((Cmem)mem));
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
                InitialDirectory = Cmem.filePath,

                AddToMostRecentlyUsedList = false,
                AllowNonFileSystemItems = false,
                DefaultDirectory = Cmem.filePath,
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
                Cmem.filePath = folder;
            }
        }

        /// <summary>
        /// Функция добавляет мем или загружает его по ссылке
        /// </summary>
        private void AddMeme()
        {
            if (!string.IsNullOrEmpty(MemName) && !string.IsNullOrEmpty(MemCategory))
            {
                if (!string.IsNullOrEmpty(MemLocation))
                {
                    Cmem mem = new Cmem(MemName, MemCategory, MemLocation);
                    if (!MemHashTag.Equals("#"))
                    {
                        mem.HashTag = MemHashTag;
                    }
                    Mems.Add(mem);
                }
                else if (!string.IsNullOrEmpty(MemUrl.ToString()))
                {
                    SelectPathDownload();
                    bool res = Cmem.DownloadMem(MemUrl, MemName);
                    if (res)
                    {
                        MemLocation = Cmem.filePath + '\\' + MemName + ".png";
                        Cmem mem = new Cmem(MemName, MemCategory, MemLocation);
                        if (!MemHashTag.Equals("#"))
                        {
                            mem.HashTag = MemHashTag;
                        }
                        Mems.Add(mem);
                    }

                }
                MakeCurrentMemeEmpty();
                CDataManager.SaveData();

            }
        }

        /// <summary>
        /// Поиск по названию мема,категории и хештега
        /// </summary>
        private void Search()
        {
            if (string.IsNullOrEmpty(SearchString))
            {
                Mems = null;
                Mems = CDataManager.Memes;
            }
            else
            {
                Mems = null;
                Mems = new ObservableCollection<Cmem>();
                foreach (var item in CDataManager.Memes)
                {
                    if (item.Name.StartsWith(SearchString)
                        || item.Category.StartsWith(SearchString)
                        || item.HashTag.StartsWith(SearchString))
                        Mems.Add(item);
                }
            }
        }

        /// <summary>
        /// Функция удаляет выбранный мем
        /// </summary>
        /// <param name="meme">Выбранный мем</param>
        private void DeleteMeme(Cmem mem)
        {
            Mems.Remove(mem);
            try
            {
                CDataManager.Memes.Remove(mem);
            }
            catch { }
            CDataManager.SaveData();
            File.Delete(mem.Source);

        }

        /// <summary>
        /// Очищает текстбоксы,в которых хранится информация о загружаемом меме
        /// </summary>
        private void MakeCurrentMemeEmpty()
        {
            MemCategory = string.Empty;
            MemName = string.Empty;
            MemLocation = string.Empty;
        }
    }
}
