/*
Traveling Salesman Problem 
Updte 15-09-21

RUN
$ dotnet run -c Release

OUTPUT
Nodes         : 13
Iterations    : 479,001,600
Nodules       : 1 2 3 4 5 6 7 8 9 10 11 12
...
Optimum route : 0 7 2 3 4 12 6 8 1 11 10 5 0
Distance      : 7293
Elapse Time   : (Optimized) 10.55 s
Elapse Time   : (Standard)  16.20 s
*/
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

using BenchmarkDotNet.Running;

using static System.Console;

class Program
{
    public static void Main()
    {
        while (true) {
            Clear();
            WriteLine("Traveling Salesman Problem Exact algorithm Approach");
            WriteLine(" [1] Optimized code");
            WriteLine(" [2] Standard code");
            _ = int.TryParse(ReadLine(), out int option);
            switch (option) {
                case 1:
                    TspApproach.Run();
                    break;
                case 2:
                    new Standard.TspApproach().Run();
                    break;
                default: return;
            }
        }

        // Try to get a stable reading on every pass by maxing priority
        //System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;
        //BenchmarkRunner.Run<tsp.Improvements.Benchmark>();
    }
}

