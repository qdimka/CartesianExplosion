using System;
using System.Linq;
using BenchmarkDotNet.Running;
using Microsoft.EntityFrameworkCore;

namespace CartesianExplosion
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Select benchmark 1 - {nameof(AsSplitBenchmark)}; 2 - {nameof(AsSplitVsCustom)}:");
            var userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    _ = BenchmarkRunner.Run<AsSplitBenchmark>();
                    break;
                case "2":
                    _ = BenchmarkRunner.Run<AsSplitVsCustom>();
                    break;
                default:
                    Console.WriteLine("u loose");
                    break;
            }
        }
    }
}