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
            _ = BenchmarkRunner.Run<AsSplitBenchmark>();
        }
    }
}