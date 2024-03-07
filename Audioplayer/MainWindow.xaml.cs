using Microsoft.WindowsAPICodePack.Dialogs;
using MS.WindowsAPICodePack.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

        private void timespass()
        {

            this.Dispatcher.Invoke(async () =>
            {
                while (true)
                {   
                    Left_time.Text = Convert.ToString(Media.Position.TotalSeconds);
                    Duration.Value = Media.Position.TotalSeconds;


                    await System.Threading.Tasks.Task.Delay(100);


                }
            });
        }

        private void repeat()
        {

            this.Dispatcher.Invoke(async () =>
            {
                while (true)
                {

                    if (Media.Position.TotalMilliseconds >= Media.NaturalDuration.TimeSpan.TotalMilliseconds)
                    {
                        Media.Position = new TimeSpan(Convert.ToInt64(0));
                        
                    }
                    await System.Threading.Tasks.Task.Delay(100);


                }
            });
        }

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
                history.Add(selectedFile.FullName);
                Media.Source = new Uri(selectedFile.FullName);
            }

            Media.Play();
        }


        private void openhistory() {
            HistoryWindow window = new HistoryWindow(history);
            window.ShowDialog();

            if(window.DialogResult == true)
            {
               //MessageBox.Show (window.Result);

               Media.Source = new Uri(window.Result);
               timespass();
            }

        }


        private void back()
        {
            int selectedIndex = Listview.SelectedIndex;
            if (selectedIndex > 0)
            {
                Media.Source = new Uri(fileInfos[selectedIndex - 1].FullName);
                timespass();
            }


        }

        private void forward()
        {
            Media.Source = new Uri(fileInfos[Listview.SelectedIndex + 1].FullName);
            timespass();
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

            openhistory();
        }

        bool click1 = false;
        bool click2 = false;

        private void Play_Click(object sender, RoutedEventArgs e)
        {

            Thread timepass = new Thread(timespass);
            

            if (click1 == true) {

                fileplay();
                timepass.Start();
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
            back();
        }

        private void OpenFolder_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
                forward();
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
           // Media.Source = new Uri();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Thread repeats = new Thread(repeat);

            if (click2 == false)
            {
                repeats.Start();
                click1 = true;



            }

            else
            {
                click2 = false;
            }
        }

        private void Duration_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
 
               

                Dispatcher.Invoke(() =>
                {
                    Media.Position = TimeSpan.FromSeconds(Duration.Value);
                });
               
               
            
        }

        private void Media_MediaOpened(object sender, RoutedEventArgs e)
        {
            Duration.Maximum = Media.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void Sound_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Media.Volume = Sound.Value;
        }
    }
    }
