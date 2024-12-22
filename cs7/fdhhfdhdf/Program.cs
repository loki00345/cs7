using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace prog
{

    class Program
    {
        // Завдання 1:
        static void PrintNumbers()
        {
            for (int i = 0; i <= 50; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(50); // Затримка для наочності
            }
        }

        // Завдання 2:
        static void PrintNumbersInRange(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(50); // Затримка для наочності
            }
        }

        // Завдання 3:
        static void PrintNumbersInMultipleThreads(int start, int end, int numberOfThreads)
        {
            Thread[] threads = new Thread[numberOfThreads];
            int range = (end - start + 1) / numberOfThreads;

            for (int t = 0; t < numberOfThreads; t++)
            {
                int threadStart = start + t * range;
                int threadEnd = (t == numberOfThreads - 1) ? end : threadStart + range - 1;

                threads[t] = new Thread(() => PrintNumbersInRange(threadStart, threadEnd));
                threads[t].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
        }

        // Завдання 4:
        static void CalculateStats(int[] numbers)
        {
            int max = int.MinValue;
            int min = int.MaxValue;
            double average = 0;

            Thread maxThread = new Thread(() =>
            {
                foreach (int num in numbers)
                {
                    if (num > max)
                        max = num;
                }
            });

            Thread minThread = new Thread(() =>
            {
                foreach (int num in numbers)
                {
                    if (num < min)
                        min = num;
                }
            });

            Thread avgThread = new Thread(() =>
            {
                double sum = 0;
                foreach (int num in numbers)
                {
                    sum += num;
                }
                average = sum / numbers.Length;
            });

            maxThread.Start();
            minThread.Start();
            avgThread.Start();

            maxThread.Join();
            minThread.Join();
            avgThread.Join();

            Console.WriteLine($"Max: {max}, Min: {min}, Average: {average}");
        }

        // Завдання 5:
        static void PrintFibonacci(int count)
        {
            int a = 0, b = 1;
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(a);
                int temp = a;
                a = b;
                b = temp + b;
                Thread.Sleep(50);
            }
        }

        static void Main(string[] args)
        {
            // Завдання 1
            Console.WriteLine("Завдання 1: Числа від 0 до 50");
            Thread task1Thread = new Thread(PrintNumbers);
            task1Thread.Start();
            task1Thread.Join();

            // Завдання 2
            Console.WriteLine("Завдання 2: Введіть початок і кінець діапазону");
            int start = int.Parse(Console.ReadLine());
            int end = int.Parse(Console.ReadLine());
            Thread task2Thread = new Thread(() => PrintNumbersInRange(start, end));
            task2Thread.Start();
            task2Thread.Join();

            // Завдання 3
            Console.WriteLine("Завдання 3: Введіть початок, кінець діапазону і кількість потоків");
            start = int.Parse(Console.ReadLine());
            end = int.Parse(Console.ReadLine());
            int numberOfThreads = int.Parse(Console.ReadLine());
            PrintNumbersInMultipleThreads(start, end, numberOfThreads);

            // Завдання 4
            Console.WriteLine("Завдання 4: Статистика для набору з 10000 чисел");
            int[] numbers = new int[10000];
            Random random = new Random();
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(0, 10000);
            }
            CalculateStats(numbers);

            // Завдання 5
            Console.WriteLine("Завдання 5: Числа Фібоначчі");
            Console.WriteLine("Введіть кількість чисел Фібоначчі:");
            int count = int.Parse(Console.ReadLine());
            Thread task5Thread = new Thread(() => PrintFibonacci(count));
            task5Thread.Start();
            task5Thread.Join();
        }
    }
}
