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
        public string manifestAdm = 
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<assembly manifestVersion=\"1.0\" xmlns=\"urn:schemas-microsoft-com:asm.v1\">\r\n  <assemblyIdentity version=\"1.0.0.0\" name=\"MyApplication.app\" />\r\n  <trustInfo xmlns=\"urn:schemas-microsoft-com:asm.v2\">\r\n    <security>\r\n      <requestedPrivileges xmlns=\"urn:schemas-microsoft-com:asm.v3\">\r\n        <!-- UAC Manifest Options\r\n             If you want to change the Windows User Account Control level replace the \r\n             requestedExecutionLevel node with one of the following.\r\n\r\n        <requestedExecutionLevel  level=\"asInvoker\" uiAccess=\"false\" />\r\n        <requestedExecutionLevel  level=\"requireAdministrator\" uiAccess=\"false\" />\r\n        <requestedExecutionLevel  level=\"highestAvailable\" uiAccess=\"false\" />\r\n\r\n            Specifying requestedExecutionLevel element will disable file and registry virtualization. \r\n            Remove this element if your application requires this virtualization for backwards\r\n            compatibility.\r\n        -->\r\n        <requestedExecutionLevel level=\"highestAvailable\" uiAccess=\"false\" />\r\n      </requestedPrivileges>\r\n      <applicationRequestMinimum>\r\n        <defaultAssemblyRequest permissionSetReference=\"Custom\" />\r\n        <PermissionSet class=\"System.Security.PermissionSet\" version=\"1\" ID=\"Custom\" SameSite=\"site\" />\r\n      </applicationRequestMinimum>\r\n    </security>\r\n  </trustInfo>\r\n  <compatibility xmlns=\"urn:schemas-microsoft-com:compatibility.v1\">\r\n    <application>\r\n      <!-- A list of the Windows versions that this application has been tested on\r\n           and is designed to work with. Uncomment the appropriate elements\r\n           and Windows will automatically select the most compatible environment. -->\r\n      <!-- Windows Vista -->\r\n      <!--<supportedOS Id=\"{e2011457-1546-43c5-a5fe-008deee3d3f0}\" />-->\r\n      <!-- Windows 7 -->\r\n      <!--<supportedOS Id=\"{35138b9a-5d96-4fbd-8e2d-a2440225f93a}\" />-->\r\n      <!-- Windows 8 -->\r\n      <!--<supportedOS Id=\"{4a2f28e3-53b9-4441-ba9c-d69d4a4a6e38}\" />-->\r\n      <!-- Windows 8.1 -->\r\n      <!--<supportedOS Id=\"{1f676c76-80e1-4239-95bb-83d0f6d0da78}\" />-->\r\n      <!-- Windows 10 -->\r\n      <!--<supportedOS Id=\"{8e0f7a12-bfb3-4fe8-b9a5-48fd50a15a9a}\" />-->\r\n    </application>\r\n  </compatibility>\r\n  <!-- Indicates that the application is DPI-aware and will not be automatically scaled by Windows at higher\r\n       DPIs. Windows Presentation Foundation (WPF) applications are automatically DPI-aware and do not need \r\n       to opt in. Windows Forms applications targeting .NET Framework 4.6 that opt into this setting, should \r\n       also set the 'EnableWindowsFormsHighDpiAutoResizing' setting to 'true' in their app.config. \r\n       \r\n       Makes the application long-path aware. See https://docs.microsoft.com/windows/win32/fileio/maximum-file-path-limitation -->\r\n  <!--\r\n  <application xmlns=\"urn:schemas-microsoft-com:asm.v3\">\r\n    <windowsSettings>\r\n      <dpiAware xmlns=\"http://schemas.microsoft.com/SMI/2005/WindowsSettings\">true</dpiAware>\r\n      <longPathAware xmlns=\"http://schemas.microsoft.com/SMI/2016/WindowsSettings\">true</longPathAware>\r\n    </windowsSettings>\r\n  </application>\r\n  -->\r\n  <!-- Enable themes for Windows common controls and dialogs (Windows XP and later) -->\r\n  <!--\r\n  <dependency>\r\n    <dependentAssembly>\r\n      <assemblyIdentity\r\n          type=\"win32\"\r\n          name=\"Microsoft.Windows.Common-Controls\"\r\n          version=\"6.0.0.0\"\r\n          processorArchitecture=\"*\"\r\n          publicKeyToken=\"6595b64144ccf1df\"\r\n          language=\"*\"\r\n        />\r\n    </dependentAssembly>\r\n  </dependency>\r\n  -->\r\n</assembly>";
        public string manifest =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<assembly manifestVersion=\"1.0\" xmlns=\"urn:schemas-microsoft-com:asm.v1\">\r\n  <assemblyIdentity version=\"1.0.0.0\" name=\"MyApplication.app\" />\r\n  <trustInfo xmlns=\"urn:schemas-microsoft-com:asm.v2\">\r\n    <security>\r\n      <requestedPrivileges xmlns=\"urn:schemas-microsoft-com:asm.v3\">\r\n        <!-- UAC Manifest Options\r\n             If you want to change the Windows User Account Control level replace the \r\n             requestedExecutionLevel node with one of the following.\r\n\r\n        <requestedExecutionLevel  level=\"asInvoker\" uiAccess=\"false\" />\r\n        <requestedExecutionLevel  level=\"requireAdministrator\" uiAccess=\"false\" />\r\n        <requestedExecutionLevel  level=\"highestAvailable\" uiAccess=\"false\" />\r\n\r\n            Specifying requestedExecutionLevel element will disable file and registry virtualization. \r\n            Remove this element if your application requires this virtualization for backwards\r\n            compatibility.\r\n        -->\r\n        <requestedExecutionLevel level=\"asInvoker\" uiAccess=\"false\" />\r\n      </requestedPrivileges>\r\n      <applicationRequestMinimum>\r\n        <defaultAssemblyRequest permissionSetReference=\"Custom\" />\r\n        <PermissionSet class=\"System.Security.PermissionSet\" version=\"1\" ID=\"Custom\" SameSite=\"site\" />\r\n      </applicationRequestMinimum>\r\n    </security>\r\n  </trustInfo>\r\n  <compatibility xmlns=\"urn:schemas-microsoft-com:compatibility.v1\">\r\n    <application>\r\n      <!-- A list of the Windows versions that this application has been tested on\r\n           and is designed to work with. Uncomment the appropriate elements\r\n           and Windows will automatically select the most compatible environment. -->\r\n      <!-- Windows Vista -->\r\n      <!--<supportedOS Id=\"{e2011457-1546-43c5-a5fe-008deee3d3f0}\" />-->\r\n      <!-- Windows 7 -->\r\n      <!--<supportedOS Id=\"{35138b9a-5d96-4fbd-8e2d-a2440225f93a}\" />-->\r\n      <!-- Windows 8 -->\r\n      <!--<supportedOS Id=\"{4a2f28e3-53b9-4441-ba9c-d69d4a4a6e38}\" />-->\r\n      <!-- Windows 8.1 -->\r\n      <!--<supportedOS Id=\"{1f676c76-80e1-4239-95bb-83d0f6d0da78}\" />-->\r\n      <!-- Windows 10 -->\r\n      <!--<supportedOS Id=\"{8e0f7a12-bfb3-4fe8-b9a5-48fd50a15a9a}\" />-->\r\n    </application>\r\n  </compatibility>\r\n  <!-- Indicates that the application is DPI-aware and will not be automatically scaled by Windows at higher\r\n       DPIs. Windows Presentation Foundation (WPF) applications are automatically DPI-aware and do not need \r\n       to opt in. Windows Forms applications targeting .NET Framework 4.6 that opt into this setting, should \r\n       also set the 'EnableWindowsFormsHighDpiAutoResizing' setting to 'true' in their app.config. \r\n       \r\n       Makes the application long-path aware. See https://docs.microsoft.com/windows/win32/fileio/maximum-file-path-limitation -->\r\n  <!--\r\n  <application xmlns=\"urn:schemas-microsoft-com:asm.v3\">\r\n    <windowsSettings>\r\n      <dpiAware xmlns=\"http://schemas.microsoft.com/SMI/2005/WindowsSettings\">true</dpiAware>\r\n      <longPathAware xmlns=\"http://schemas.microsoft.com/SMI/2016/WindowsSettings\">true</longPathAware>\r\n    </windowsSettings>\r\n  </application>\r\n  -->\r\n  <!-- Enable themes for Windows common controls and dialogs (Windows XP and later) -->\r\n  <!--\r\n  <dependency>\r\n    <dependentAssembly>\r\n      <assemblyIdentity\r\n          type=\"win32\"\r\n          name=\"Microsoft.Windows.Common-Controls\"\r\n          version=\"6.0.0.0\"\r\n          processorArchitecture=\"*\"\r\n          publicKeyToken=\"6595b64144ccf1df\"\r\n          language=\"*\"\r\n        />\r\n    </dependentAssembly>\r\n  </dependency>\r\n  -->\r\n</assembly>";

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
            if (HoursList.SelectedIndex > 0 && MinutesList.SelectedIndex > 0 && SecondsList.SelectedIndex > 0)
            {
                finish = new TimeSpan(DateTime.Now.Hour + HoursList.SelectedIndex, DateTime.Now.Minute + MinutesList.SelectedIndex, DateTime.Now.Second + SecondsList.SelectedIndex);
                WithTimer = true;
            }
            else if (HoursList.SelectedIndex <= 0 && MinutesList.SelectedIndex > 0 && SecondsList.SelectedIndex > 0)
            {
                finish = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute + MinutesList.SelectedIndex, DateTime.Now.Second + SecondsList.SelectedIndex);
                WithTimer = true;
            }
            else if (HoursList.SelectedIndex > 0 && MinutesList.SelectedIndex <= 0 && SecondsList.SelectedIndex > 0)
            {
                finish = new TimeSpan(DateTime.Now.Hour + HoursList.SelectedIndex, DateTime.Now.Minute, DateTime.Now.Second + SecondsList.SelectedIndex);
                WithTimer = true;
            }
            else if (HoursList.SelectedIndex > 0 && MinutesList.SelectedIndex > 0 && SecondsList.SelectedIndex <= 0)
            {
                finish = new TimeSpan(DateTime.Now.Hour + HoursList.SelectedIndex, DateTime.Now.Minute + MinutesList.SelectedIndex, DateTime.Now.Second);
                WithTimer = true;
            }
            else if (HoursList.SelectedIndex <= 0 && MinutesList.SelectedIndex <= 0 && SecondsList.SelectedIndex > 0)
            {
                finish = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second + SecondsList.SelectedIndex);
                WithTimer = true;
            }
            else if (HoursList.SelectedIndex <= 0 && MinutesList.SelectedIndex > 0 && SecondsList.SelectedIndex <= 0)
            {
                finish = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute + MinutesList.SelectedIndex, DateTime.Now.Second);
                WithTimer = true;
            }
            else if (HoursList.SelectedIndex > 0 && MinutesList.SelectedIndex <= 0 && SecondsList.SelectedIndex <= 0)
            {
                finish = new TimeSpan(DateTime.Now.Hour + HoursList.SelectedIndex, DateTime.Now.Minute, DateTime.Now.Second);
                WithTimer = true;
            }
            else WithTimer = false;

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

        
    }
}