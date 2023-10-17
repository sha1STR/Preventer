﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.IO;

namespace Preventer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TimerClass timerClass = new TimerClass();
        public static string userName = Environment.UserName;
        public static string appPath = $"C:/users/{userName}/Documents";
        public static string dictPath = System.IO.Path.Combine(appPath, "Preventer");
        public static string appFileName = "appBase.txt";
        public static string procFileName = "procBase.txt";
        public string saveAppPath = System.IO.Path.Combine(dictPath, appFileName);
        public string saveProcPath = System.IO.Path.Combine(dictPath, procFileName);



        public MainWindow()
        {
            System.IO.Directory.CreateDirectory(dictPath);
            InitializeComponent();
            fileEdit();
            SetTimerComponents();
            timerClass.TimerSettings();
            timerClass.Progress = Progress;
            timerClass.statusLabel = statusLabel;
            timerClass.startAppButton = startAppButton;
            timerClass.Program.saveProcPath = saveProcPath;
        }
        // Backend
        

        public void SetTimerComponents()
        {
            for (int i = 0; i < 60; i++)
            {
                SecondsList.Items.Add(i);
            }
            for (int i = 0; i < 60; i++)
            {
                MinutesList.Items.Add(i);
            }
            for (int i = 0; i <= 24; i++)
            {
                HoursList.Items.Add(i);
            }
            SecondsList.SelectedIndex = 0;
            MinutesList.SelectedIndex = 0;
            HoursList.SelectedIndex = 0;
        }
        public void fileEdit()
        {
            // AppName Path
            if (System.IO.File.Exists(saveAppPath))
            {
                timerClass.Program.selectedApp = System.IO.File.ReadAllLines(saveAppPath).ToList();
            }
            else
            {
                System.IO.File.Create(saveAppPath);
            }
            // ProcName Path
            if (System.IO.File.Exists(saveProcPath))
            {
                timerClass.Program.selectedProc = System.IO.File.ReadAllLines(saveProcPath).ToList();
            }
            else
            {
                System.IO.File.Create(timerClass.Program.saveProcPath);
            }
        }

        
        public void resetLists()
        {
            timerClass.Program.GetAppName();
            appAddListBox.Items.Clear();
            foreach (string name in timerClass.Program.appName)
            {
                appAddListBox.Items.Add(name);
            }
            appDelListBox.Items.Clear();
            foreach (string app in timerClass.Program.selectedApp)
            {
                appDelListBox.Items.Add(app);
            }
            try
            {
                System.IO.File.WriteAllLines(saveAppPath, timerClass.Program.selectedApp);
                System.IO.File.WriteAllLines(saveProcPath, timerClass.Program.selectedProc);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        //
        // Switching Pages        
        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            startGrid.Visibility = Visibility.Visible;
            selectGrid.Visibility = Visibility.Hidden;
        }

        private void selectButton_Click(object sender, RoutedEventArgs e)
        {
            resetLists();
            startGrid.Visibility = Visibility.Hidden;
            selectGrid.Visibility = Visibility.Visible;
        }
        //
        // Controls
        private void startAppButton_Click(object sender, RoutedEventArgs e)
        {
            timerClass.finish = new TimeSpan(DateTime.Now.Hour + HoursList.SelectedIndex, DateTime.Now.Minute + MinutesList.SelectedIndex, DateTime.Now.Second + SecondsList.SelectedIndex);

            timerClass.startPressed = !timerClass.startPressed;

            if (timerClass.startPressed)
            {
                statusLabel.Foreground = Brushes.Green;
                statusLabel.Content = "Active";
                startAppButton.Content = "Stop";
            }
            else
            {
                statusLabel.Foreground = Brushes.Gray;
                statusLabel.Content = "Disabled";
                startAppButton.Content = "Start";
            }
        }

        private void appAddListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (appAddListBox.SelectedItem != null)
            {
                timerClass.Program.selectedApp.Add(appAddListBox.SelectedItem.ToString());
                resetLists();
            }
        }

        private void appDelListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (appDelListBox.SelectedItem != null)
            {
                timerClass.Program.selectedApp.Remove(appDelListBox.SelectedItem.ToString());
                timerClass.Program.selectedProc.Remove(timerClass.Program.selectedProc[appDelListBox.SelectedIndex]);
                Console.WriteLine(appDelListBox.SelectedItem.ToString() + $" ({timerClass.Program.selectedProc[appDelListBox.SelectedIndex]}) was removed from the lists.");
                System.IO.File.WriteAllLines(saveProcPath, timerClass.Program.selectedProc);
                resetLists();
            }
        }

        private void tutorialButton_Click(object sender, RoutedEventArgs e)
        {
            TutorialGrid1.Visibility = Visibility.Visible;
        }

        private void startButton_GotFocus(object sender, RoutedEventArgs e)
        {
            startButton.BorderBrush = bg1.Background;
        }

        private void startButton_LostFocus(object sender, RoutedEventArgs e)
        {
            startButton.Background = bg2.Background;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            resetLists();
        }

        private void Ok1_Click(object sender, RoutedEventArgs e)
        {
            TutorialGrid1.Visibility = Visibility.Hidden;
            TutorialGrid2.Visibility = Visibility.Visible;
        }

        private void Ok2_Click(object sender, RoutedEventArgs e)
        {
            TutorialGrid2.Visibility = Visibility.Hidden;
        }

        private void Frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void ToggleTimerButton_Checked(object sender, RoutedEventArgs e)
        {
            TimerGrid.Visibility = Visibility.Visible;
            timerClass.WithTimer = true;
        }

        private void ToggleTimerButton_Unchecked(object sender, RoutedEventArgs e)
        {
            TimerGrid.Visibility = Visibility.Hidden;
            timerClass.WithTimer = false;
        }
    }
}