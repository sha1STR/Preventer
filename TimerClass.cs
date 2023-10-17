using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace Preventer
{
    internal class TimerClass
    {
        internal Program Program;
        public DispatcherTimer Timer;
        public TimeSpan finish;
        public TimeSpan currentTime;
        internal bool WithTimer;
        internal bool startPressed = false;
        internal System.Windows.Controls.Label Progress;
        internal System.Windows.Controls.Label statusLabel;
        internal System.Windows.Controls.Button startAppButton;
        internal TimerClass()
        {
            Program = new Program();
            Timer = new DispatcherTimer();
        }


        public void TimerSettings()
        {
            Timer.Tick += TimerTick;
            Timer.Interval = TimeSpan.FromMilliseconds(500);
            Timer.Start();
        }
        public void TimerTick(object sender, EventArgs e)
        {
            //Console.WriteLine(WithTimer);
            if (WithTimer)
            {
                 if (startPressed)
                {
                    TimeSpan timeNow = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    Program.ProcessKilling();
                    Progress.Content = finish - timeNow;
                    statusLabel.Foreground = Brushes.Green;
                    statusLabel.Content = "Active";
                    startAppButton.Content = "Stop";
                    if (finish <= timeNow)
                    {
                        startPressed = false;
                        Console.WriteLine("The time is past finish.");
                    }
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
                    Program.ProcessKilling();
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
    }
}
