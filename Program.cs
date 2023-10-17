using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preventer
{
    internal class Program
    {
        internal List<string> appName;
        internal List<string> procName;
        internal List<string> selectedApp;
        internal List<string> selectedProc;
        internal string saveProcPath;
        public Program()
        {
            appName = new List<string>();
            procName = new List<string>();
            selectedApp = new List<string>();
            selectedProc = new List<string>();
        }

        internal void GetAppName()
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
                        Console.WriteLine($"{process.MainWindowTitle} ({process.ProcessName}) was added to the selectedProc list.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
        internal void ProcessKilling()
        {
            Process[] processes = Process.GetProcesses(); // creates a massive of all the processes

            foreach (Process process in processes) // this line speaks for itself
            {
                try
                {

                    if (selectedProc.Contains(process.ProcessName)) // checks whether the process is not allowed
                    {
                        process.Kill(); // prevents the app from being opened during the session
                        Console.WriteLine($"{process.MainWindowTitle} ({process.ProcessName}) was killed.");
                    }
                }
                catch
                {

                }

            }
        }
    }
}
