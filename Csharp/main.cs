/*
Traveling Salesman Problem 
By: harveytriana@gmail.com

RUN
$ dotnet run -c Release

OUTPUT
Nodes         : 13
Iterations    : 479,001,600
Nodules       : 1 2 3 4 5 6 7 8 9 10 11 12
...
Optimus route : 0 7 2 3 4 12 6 8 1 11 10 5  0
Distance      : 7293
Elapse Time   : 20.0720705 s
*/
using System;

class Program
{
    public static void Main() {
        long[,] a = {
                { 0, 2451, 713, 1018, 1631, 1374, 2408, 213, 2571, 875, 1420, 2145, 1972 },
                { 2451, 0, 1745, 1524, 831, 1240, 959, 2596, 403, 1589, 1374, 357, 579 },
                { 713, 1745, 0, 355, 920, 803, 1737, 851, 1858, 262, 940, 1453, 1260 },
                { 1018, 1524, 355, 0, 700, 862, 1395, 1123, 1584, 466, 1056, 1280, 987 },
                { 1631, 831, 920, 700, 0, 663, 1021, 1769, 949, 796, 879, 586, 371 },
                { 1374, 1240, 803, 862, 663, 0, 1681, 1551, 1765, 547, 225, 887, 999 },
                { 2408, 959, 1737, 1395, 1021, 1681, 0, 2493, 678, 1724, 1891, 1114, 701 },
                { 213, 2596, 851, 1123, 1769, 1551, 2493, 0, 2699, 1038, 1605, 2300, 2099 },
                { 2571, 403, 1858, 1584, 949, 1765, 678, 2699, 0, 1744, 1645, 653, 600 },
                { 875, 1589, 262, 466, 796, 547, 1724, 1038, 1744, 0, 679, 1272, 1162 },
                { 1420, 1374, 940, 1056, 879, 225, 1891, 1605, 1645, 679, 0, 1017, 1200 },
                { 2145, 357, 1453, 1280, 586, 887, 1114, 2300, 653, 1272, 1017, 0, 504 },
                { 1972, 579, 1260, 987, 371, 999, 701, 2099, 600, 1162, 1200, 504, 0 },
            };

        Console.WriteLine("Traveling Salesman Problem Exact algorithm");
        new TspExact().GetOptimumRoute(a);
    }
}

class TspExact
{
    long[,] _data;
    long _depot;
    long _nodes;
    long _nodulesCount;
    long _iterations;
    long _percentSize;
    long _percent;
    long _permutation;
    long _minDistance;
    string _route;
    long[] _nodules;

    public void GetOptimumRoute(long[,] data, long depot = 0) {
        _data = data;
        _depot = depot;
        _nodes = _data.GetLength(0);
        _nodulesCount = _nodes - 1;
        _iterations = Factorial(_nodulesCount);
        _percentSize = _iterations / 100;
        _permutation = 1;
        _minDistance = long.MaxValue;

        var now = DateTime.Now;

        // arrangement of permutations
        _nodules = new long[_nodulesCount];
        long j = 0;
        for (long i = 0; i < _nodes; i++) {
            if (i != _depot) {
                _nodules[j++] = i;
            }
        }
        NodulesString();

        Console.WriteLine("Nodes         : {0}", _nodes);
        Console.WriteLine("Iterations    : {0:N0}", _iterations);
        Console.WriteLine("Nodules       : {0}", _route);

        // recursive calculation 
        GetRoute(0, _nodulesCount);

        Console.WriteLine("RESULT");
        Console.WriteLine("Optimus route : {0} {1} {2}", _depot, _route, _depot);
        Console.WriteLine("Distance      : {0}", _minDistance);
        Console.WriteLine("Elapse Time   : {0} s", (DateTime.Now - now).TotalSeconds);
    }

    void GetRoute(long start, long end) {
        if (start == end - 1) {
            // validate distance
            // 1. boundaries A..N, N..A
            long s = _data[_depot, _nodules[0]] +
                     _data[_nodules[_nodulesCount - 1], _depot];
            // 2. route
            for (long i = 0; i < _nodulesCount - 1; i++) {
                s += _data[_nodules[i], _nodules[i + 1]];
            }
            _permutation++;
            if (_minDistance > s) {// update minimun
                _minDistance = s;
                NodulesString();
            }
            if (_percentSize > 0 && _permutation % _percentSize == 0) {
                _percent += 1;
                Console.WriteLine("Permutations: {0} %", _percent);
            }
        } else {
            for (var i = start; i < end; i++) {
                // swap
                (_nodules[start], _nodules[i]) = (_nodules[i], _nodules[start]);
                // permute
                GetRoute(start + 1, end);
                // swap
                (_nodules[start], _nodules[i]) = (_nodules[i], _nodules[start]);
            }
        }
    }

    long Factorial(long number) {
        if (number < 2)
            return 1;
        else
            return number * Factorial(number - 1);
    }

    void NodulesString() {
        _route = string.Empty;
        for (long i = 0; i < _nodulesCount - 1; i++) {
            _route = _route + _nodules[i].ToString() + " ";
        }
    }
}

