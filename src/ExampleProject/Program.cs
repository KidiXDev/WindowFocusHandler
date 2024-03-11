using System;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using wfh;

namespace ExampleProject
{
    public class Program
    {
        private static Timer timer;
        private static Process targetProcess;

        static void Main(string[] args)
        {
            // Change desired process name here. Alternatively, use Process.GetProcessById to get a specific process by PID.
            // Try running Notepad first.
            targetProcess = Process.GetProcessesByName("notepad").FirstOrDefault();

            if (targetProcess != null)
            {
                /*
                 * You can use any approach to achieve this.
                 * For example, by using Timer.Elapsed from a Timer
                 */
                using (timer = new Timer())
                {
                    timer.Interval = 1000;
                    timer.Elapsed += TimerElapsed;
                    timer.Start();

                    Console.WriteLine("Monitoring Notepad...");
                    Console.WriteLine("Press Enter to stop.");
                    Console.WriteLine("====================================");
                    Console.ReadLine();

                    timer.Stop();
                }
            }
            else
            {
                Console.WriteLine("Notepad is not running.");
                Console.ReadLine();
            }
        }


        /*
         * TimerElapsed method that handles the Elapsed event of a timer.
         * Checks if the target process is still running and if it is currently in focus.
         */
        private static void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            // Check if the target process is null or has exited
            if (targetProcess == null || targetProcess.HasExited)
            {
                Console.WriteLine("Notepad is no longer running.");
                timer.Stop(); // Stop the timer
                return;
            }

            // Check if the target process window is focused
            bool isFocused = FocusHandler.IsProcessWindowFocused(targetProcess);
            Console.WriteLine(isFocused ? "> Notepad currently in focus." : "> Notepad no longer in focus.");
        }


    }
}
