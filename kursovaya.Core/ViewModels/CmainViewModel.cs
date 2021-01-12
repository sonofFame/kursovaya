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

        }
        
    }
}
