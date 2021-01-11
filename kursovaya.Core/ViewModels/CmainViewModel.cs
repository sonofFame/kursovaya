using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Input;

namespace kursovaya.Core.ViewModels
{
    public class CmainViewModel
    {
        public ObservableCollection<Cmem> Memes { get; set; }

        public string MemeName { get; set; }
        public string MemeCategory { get; set; }
        public string MemeLocation { get; set; }

        public string MemeHashTag { get; set; } = "#";
        public Uri MemeUrl { get; set; }
        public string SearchString { get; set; }

        public ICommand DeleteMemeCommand { get; set; }
        public ICommand AddNewMemeCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public CmainViewModel()
        {

        }

            
        
    }
}
