using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preventer
{
    internal class Program : MainWindow
    {
        internal static void GetAppName()
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
                    Console.WriteLine(e.Message);
                }

            }
        }
        internal static void ProcessKilling()
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
    }
}
