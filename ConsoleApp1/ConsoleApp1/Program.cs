using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
            private static long _counter1 = 0;
            private static long _counter2 = 0;
            private static bool _keepRunning = true;

            static uint GenerateAbsoluteChaos()
            {
                _counter1 = 0;
                _counter2 = 0;
                _keepRunning = true;

                Thread t1 = new Thread(() => {
                    while (_keepRunning) { _counter1++; }
                });

                Thread t2 = new Thread(() => {
                    while (_keepRunning) { _counter2++; }
                });

                t1.Start();
                t2.Start();

                Thread.Sleep(5);

                _keepRunning = false; 
                t1.Join();
                t2.Join();
               
                long difference = Math.Abs(_counter1 - _counter2);
               
                ulong finalSeed = (ulong)difference ^ (ulong)Stopwatch.GetTimestamp();

                finalSeed ^= finalSeed << 13;
                finalSeed ^= finalSeed >> 7;
                finalSeed ^= finalSeed << 17;

                return (uint)(finalSeed % uint.MaxValue);
            }

            static void Main(string[] args)
            {
                Console.WriteLine("--- Thermal Jitter Race Algorithm ---");
                Console.WriteLine("Direct hardware-level entropy...\n");

                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"Chaos Number {i + 1}: {GenerateAbsoluteChaos()}");
                }

                Console.ReadLine();
            }
        }
    }








