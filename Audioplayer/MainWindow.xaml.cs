using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;

namespace Audioplayer
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        public List<string> history = new List<string>();
        public List<FileInfo> fileInfos = new List<FileInfo>();
        private void displayFiles() {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog { IsFolderPicker = true };
            var result = dialog.ShowDialog();

            if (result == CommonFileDialogResult.Ok)
            {
                string[] files = Directory.GetFiles(dialog.FileName);
              

                foreach (string file in files)
                {
                    string fileExtension = System.IO.Path.GetExtension(file);

                    if (fileExtension == ".mp3" || fileExtension == ".wav" || fileExtension == ".m4a")
                    {

                        FileInfo fileInfo = new FileInfo(file);
                        fileInfos.Add(fileInfo);


                    }

                }
                Listview.ItemsSource = fileInfos;
                Media.Source = new Uri(fileInfos[0].FullName);
                Media.Play();


            }
        }
        
        private void fileplay()
        {
            FileInfo selectedFile = (FileInfo)Listview.SelectedItem;

            if (selectedFile != null)
            {
                history.Add(selectedFile.Name);
                Media.Source = new Uri(selectedFile.FullName);
            }

            Media.Play();
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenFolder_Click(object sender, RoutedEventArgs e)
        {

            displayFiles();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HistoryWindow window = new HistoryWindow(history);
            window.Show();

        }

        bool click1 = false;
        bool click2 = false;

        private void Play_Click(object sender, RoutedEventArgs e)
        {

            if (click1 == true) {

                fileplay();
                click1 = false;
            }

            else if (click1 == false) 
            {
                Media.Stop();
                click1 = true;
            }



        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int selectedIndex = Listview.SelectedIndex;
            if (selectedIndex > 0)
            {
                Media.Source = new Uri(fileInfos[selectedIndex - 1].FullName);
            }
        }

        private void OpenFolder_ContextMenuClosing(object sender, ContextMenuEventArgs e)

            {
                Media.Source = new Uri(fileInfos[Listview.SelectedIndex + 1].FullName); 
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
           // Media.Source = new Uri();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (click2 == false)
            {
                if (Media.Position.TotalMilliseconds >= Media.NaturalDuration.TimeSpan.TotalMilliseconds)
                {
                    Media.Position = new TimeSpan(Convert.ToInt64(0));
                    click2 = true;
                }


            }

            else
            {
                click2 = false;
            }
        }
    }
}
