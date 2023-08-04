using System;
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
        public bool startPressed = false;
        public List<string> selectedApp = new List<string>();
        public List<string> selectedProc = new List<string>();
        public List<string> procName = new List<string>();
        public static string userName = Environment.UserName;
        public static string appPath = $"C:/users/{userName}/Documents";
        public static string dictPath = System.IO.Path.Combine(appPath, "Preventer");
        public static string appFileName = "appBase.txt";
        public static string procFileName = "procBase.txt";
        public string saveAppPath = System.IO.Path.Combine(dictPath, appFileName);
        public string saveProcPath = System.IO.Path.Combine(dictPath, procFileName);
        public List<string> appName = new List<string>();
        

        public DispatcherTimer Timer = new DispatcherTimer();

        public TimeSpan finish;
        public TimeSpan currentTime;
        public bool WithTimer = false;

        public MainWindow()
        {
            System.IO.Directory.CreateDirectory(dictPath);
            InitializeComponent();
            fileEdit();
            SetTimerComponents();
            TimerSettings();
            
        }
        // Backend
        public void TimerSettings()
        {
            Timer.Tick += TimerTick;
            Timer.Interval = TimeSpan.FromMilliseconds(500);
            Timer.Start();
        }
        public void TimerTick(object sender, EventArgs e)
        {            
            if (WithTimer)
            {
                if (startPressed && new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second) >= finish && finish != new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second))
                {
                    startPressed = false;
                }
                else if (startPressed)
                {
                    Program();
                    Progress.Content = finish - new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    statusLabel.Foreground = Brushes.Green;
                    statusLabel.Content = "Active";
                    startAppButton.Content = "Stop";
                }
                else
                {
                    Progress.Content = "00:00:00";
                    statusLabel.Foreground = Brushes.Gray;
                    statusLabel.Content = "Disabled";
                    startAppButton.Content = "Start";
                }
            }
            else
            {
                if (startPressed)
                {
                    Program();
                    statusLabel.Foreground = Brushes.Green;
                    statusLabel.Content = "Active";
                    startAppButton.Content = "Stop";
                }
                else
                {
                    Progress.Content = "00:00:00";
                    statusLabel.Foreground = Brushes.Gray;
                    statusLabel.Content = "Disabled";
                    startAppButton.Content = "Start";
                }
            }
        }

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
                selectedApp = System.IO.File.ReadAllLines(saveAppPath).ToList();
            }
            else
            {
                System.IO.File.Create(saveAppPath);
            }
            // ProcName Path
            if (System.IO.File.Exists(saveProcPath))
            {
                selectedProc = System.IO.File.ReadAllLines(saveProcPath).ToList();
            }
            else
            {
                System.IO.File.Create(saveProcPath);
            }
        }

        public void GetAppName()
        {
            appName.Clear();
            procName.Clear();
            selectedProc.Clear();
            selectedProc = System.IO.File.ReadAllLines(saveProcPath).ToList();

            Process[] processes = Process.GetProcesses().OrderBy(process => process.ProcessName).ToArray();
            foreach (Process process in processes)
            {
                try
                {
                    if (!selectedApp.Contains(process.MainWindowTitle)
                        && process.MainWindowTitle != "Preventer"
                        && !process.MainWindowTitle.Contains("Microsoft")
                        && !appName.Contains(process.MainWindowTitle)
                        && process.MainWindowTitle.Length > 0
                        //&& !selectedProc.Contains(process.ProcessName)
                        && process.MainModule.FileName != "")
                    {
                        appName.Add(process.MainWindowTitle);
                    }
                    
                    if (selectedApp.Contains(process.MainWindowTitle)
                        && !selectedProc.Contains(process.ProcessName))
                    {
                        selectedProc.Add(process.ProcessName);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            }
        }
        public void Program()
        {
            Process[] processes = Process.GetProcesses(); // creates a massive of all the processes

            foreach (Process process in processes) // this line speaks for itself
            {
                try
                {
                    
                    if (selectedProc.Contains(process.ProcessName)) // checks whether the process is not allowed
                    {
                        process.Kill(); // prevents the app from being opened during the session
                    }
                }
                catch
                {
                    
                }
                
            }
        }
        public void resetLists()
        {
            GetAppName();
            appAddListBox.Items.Clear();
            foreach (string name in appName)
            {
                appAddListBox.Items.Add(name);
            }
            appDelListBox.Items.Clear();
            foreach (string app in selectedApp)
            {
                appDelListBox.Items.Add(app);
            }
            try
            {
                System.IO.File.WriteAllLines(saveAppPath, selectedApp);
                System.IO.File.WriteAllLines(saveProcPath, selectedProc);
            }
            catch
            {
                
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
            finish = new TimeSpan(DateTime.Now.Hour + HoursList.SelectedIndex, DateTime.Now.Minute + MinutesList.SelectedIndex, DateTime.Now.Second + SecondsList.SelectedIndex);
                        
            startPressed = !startPressed;

            if (startPressed)
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
                selectedApp.Add(appAddListBox.SelectedItem.ToString());
                resetLists();
            }
        }

        private void appDelListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (appDelListBox.SelectedItem != null)
            {
                selectedApp.Remove(appDelListBox.SelectedItem.ToString());
                selectedProc.Remove(selectedProc[appDelListBox.SelectedIndex]);
                //MessageBox.Show(selectedProc.ToString());
                System.IO.File.WriteAllLines(saveProcPath, selectedProc);
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
            WithTimer = true;
        }

        private void ToggleTimerButton_Unchecked(object sender, RoutedEventArgs e)
        {
            TimerGrid.Visibility = Visibility.Hidden;
            WithTimer = false;
        }
    }
}
