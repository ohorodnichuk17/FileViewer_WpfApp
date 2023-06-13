using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace FileViewer_WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel viewModel = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult res = dialog.ShowDialog();

            if(res == CommonFileDialogResult.Ok)
            {
                //MessageBox.Show(dialog.FileName);
                viewModel.LoadFiles(dialog.FileName);
            }
        }
    }

    class ViewModel : INotifyPropertyChanged
    {
        private string directoryPath;

        public string DirectoryPath
        {
            get =>  directoryPath; 
            set 
            { 
                directoryPath = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DirectoryName));
            }
        }

        private ObservableCollection<FileInfo> files = new ObservableCollection<FileInfo>();

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IEnumerable<FileInfo> Files => files;

        public string DirectoryName => DirectoryPath;
        private FileInfo selectedfile
        {
            get => selectedfile;
            set
            {
                selectedfile = value;
                OnPropertyChanged();
            }
        }
        public FileInfo SelectedFile { get; set; }

        public ViewModel()
        {
            LoadFiles(@"D:\Downloads\windows");
        }

        public void LoadFiles(string dirPath)
        {
            this.DirectoryPath = dirPath;
            DirectoryInfo directory = new DirectoryInfo(dirPath);
            var data = directory.GetFiles();

            files.Clear();
            foreach (var item in data)
            {
                files.Add(item);
            }

        }
    }
}
