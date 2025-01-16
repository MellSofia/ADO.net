using System;
using System.Threading;
using System.Timers;

namespace SysProg01_Timers
{
    internal class Program
    {
        private static int[] percents = { 0, 15, 36, 51, 65, 73, 85, 99, 100 };
        private static int counter = 0;
        private static System.Timers.Timer updateTimer;
        private static System.Timers.Timer displayTimer;

        static void UpdateProgress(object source, ElapsedEventArgs e)
        {
            if (counter < percents.Length)
            {
                counter++;
            }
            else
            {
                updateTimer.Stop();
                updateTimer.Dispose();
            }
        }

        static void DisplayProgress(object source, ElapsedEventArgs e)
        {
            Console.Clear();
            if (counter < percents.Length)
            {
                Console.WriteLine($"Загрузка: {percents[counter]}% ...");
                Console.WriteLine($"Текущее время: {e.SignalTime}.");
            }
            else
            {
                Console.WriteLine($"Загрузка завершена!");
                Console.WriteLine($"Текущее время: {e.SignalTime}.");
                Console.WriteLine("Нажмите любую клавишу, чтобы выйти из программы.");
                displayTimer.Stop();
                displayTimer.Dispose();
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.Clear();

            updateTimer = new System.Timers.Timer(1500);
            updateTimer.Elapsed += UpdateProgress;
            updateTimer.AutoReset = true;

            displayTimer = new System.Timers.Timer(2000);
            displayTimer.Elapsed += DisplayProgress;
            displayTimer.AutoReset = true;

            Console.WriteLine("Ожидайте начала загрузки.");
            updateTimer.Start();
            displayTimer.Start();

            Console.ReadLine();
        }
    }
}