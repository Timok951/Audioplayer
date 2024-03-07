﻿using MS.WindowsAPICodePack.Internal;
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
using System.Windows.Shapes;

namespace Audioplayer
{
    /// <summary>
    /// Логика взаимодействия для HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        public string Result { get; set; }

        public HistoryWindow(List<string> history)
        {
            InitializeComponent();
            History.ItemsSource = history;
        }

        private void History_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string selectedFilePath = History.SelectedItem as string;
            Result = selectedFilePath;
             this.DialogResult = true;  

        }
    }
}
