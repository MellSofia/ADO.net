using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NumberGenerator
{

    public partial class MainWindow : Window
    {
        private Thread primeThread;
        private Thread fibonacciThread;
        private bool isRunning;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            int lowerBound = string.IsNullOrEmpty(LowerBoundTextBox.Text) ? 2 : int.Parse(LowerBoundTextBox.Text);
            int upperBound = string.IsNullOrEmpty(UpperBoundTextBox.Text) ? int.MaxValue : int.Parse(UpperBoundTextBox.Text);

            isRunning = true;
            primeThread = new Thread(() => GeneratePrimes(lowerBound, upperBound));
            primeThread.Start();
        }

        private void GeneratePrimes(int lowerBound, int upperBound)
        {
            for (int number = lowerBound; isRunning; number++)
            {
                if (IsPrime(number))
                {
                    Dispatcher.Invoke(() => PrimeNumbersListBox.Items.Add(number));
                    if (number >= upperBound) break;
                }
            }
        }

        private bool IsPrime(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        private void StopPrimesButton_Click(object sender, RoutedEventArgs e)
        {
            isRunning = false;
        }

        private void StartFibonacciButton_Click(object sender, RoutedEventArgs e)
        {
            isRunning = true;
            fibonacciThread = new Thread(GenerateFibonacci);
            fibonacciThread.Start();
        }

        private void GenerateFibonacci()
        {
            int a = 0, b = 1;
            while (isRunning)
            {
                Dispatcher.Invoke(() => FibonacciNumbersListBox.Items.Add(a));
                int next = a + b;
                a = b;
                b = next;
                Thread.Sleep(100);
            }
        }

        private void StopFibonacciButton_Click(object sender, RoutedEventArgs e)
        {
            isRunning = false;
        }

        protected override void OnClosed(EventArgs e)
        {
            isRunning = false;
            primeThread?.Join();
            fibonacciThread?.Join();
            base.OnClosed(e);
        }
    }
}
