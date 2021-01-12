using System.Windows;
using MemeFinder.Core;
using Microsoft.Win32;
namespace MemeFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new MainViewModel();
            InitializeComponent();
        }
        /// <summary>
        /// Функция предоставляет доступ к выбору файла,который будет мы потом сохраним
        /// </summary>
        private void OpenFileDilaog_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "png files (*.png)|(*.jpeg)|(*.gif)|*.png|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if ((bool)fd.ShowDialog())
            {
                //Get the path of specified file
                (this.DataContext as MainViewModel).MemeLocation = fd.FileName;
            }
        }

        
    }
}
