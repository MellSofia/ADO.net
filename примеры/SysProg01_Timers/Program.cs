using System.Diagnostics.Metrics;

namespace SysProg01_Timers
{
    internal class Program
    {
        private static int[] percents = { 0, 15, 36, 51, 65, 73, 85, 99, 100 };
        private static int counter = 0;
        private static System.Timers.Timer timer;
        static void WriteSymbols(object source, System.Timers.ElapsedEventArgs e)
        {
            Console.Clear();
            if (counter < percents.Length)
            {
                Console.WriteLine($"Загрузка: {percents[counter]}% ...");
                Console.WriteLine($"Текущее время: {e.SignalTime}.");
                counter++;
                Thread.Sleep(2000);
                //await Task.Delay(2000);
            }
            else
            {
                Console.WriteLine($"Загрузка завершена!");
                Console.WriteLine($"Текущее время: {e.SignalTime}.");
                Console.WriteLine("Нажмите любую клавишу, чтобы выйти из программы.");
                timer.Stop();
                timer.Dispose();
            }
            
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.Clear();
            timer = new System.Timers.Timer(1500);
            timer.Elapsed += WriteSymbols;
            timer.AutoReset = true;
            Console.WriteLine("Ожидайте начала загрузки.");
            timer.Start();
            Console.ReadLine();
        }
    }
}
