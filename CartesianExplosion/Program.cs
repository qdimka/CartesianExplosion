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
            Console.WriteLine($"Select benchmark 1 - {nameof(AsSplitBenchmark)}; 2 - {nameof(AsSplitVsCustom)}; 3 - {nameof(AsSplitVsAsNoTrackingWithIdentityResolution)}:");
            var userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    _ = BenchmarkRunner.Run<AsSplitBenchmark>();
                    break;
                case "2":
                    _ = BenchmarkRunner.Run<AsSplitVsCustom>();
                    break;
                case "3":
                    _ = BenchmarkRunner.Run<AsSplitVsAsNoTrackingWithIdentityResolution>();
                    break;
                default:
                    Console.WriteLine("u loose");
                    break;
            }
        }
    }
}