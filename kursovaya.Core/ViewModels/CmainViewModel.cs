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
                CDataManager.SaveData();

            }
        }

    }
}
