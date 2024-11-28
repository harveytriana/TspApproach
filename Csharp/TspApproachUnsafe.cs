/*
Traveling Salesman Problem 
Collaboration Geekosaurus Tedd

Update: 18-10-21

OUTPUT
Nodes         : 13
Iterations    : 479,001,600
Nodules       : 1 2 3 4 5 6 7 8 9 10 11 12
...
Optimum route : 0 7 2 3 4 12 6 8 1 11 10 5 0
Distance      : 7293
Elapse Time   : ~10.55 s
*/
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

class TspApproachUnsafeSample
{
    public static void Run()
    {
        int[] data = [
             0, 2451, 713, 1018, 1631, 1374, 2408, 213, 2571, 875, 1420, 2145, 1972,
             2451, 0, 1745, 1524, 831, 1240, 959, 2596, 403, 1589, 1374, 357, 579,
             713, 1745, 0, 355, 920, 803, 1737, 851, 1858, 262, 940, 1453, 1260,
             1018, 1524, 355, 0, 700, 862, 1395, 1123, 1584, 466, 1056, 1280, 987,
             1631, 831, 920, 700, 0, 663, 1021, 1769, 949, 796, 879, 586, 371,
             1374, 1240, 803, 862, 663, 0, 1681, 1551, 1765, 547, 225, 887, 999,
             2408, 959, 1737, 1395, 1021, 1681, 0, 2493, 678, 1724, 1891, 1114, 701,
             213, 2596, 851, 1123, 1769, 1551, 2493, 0, 2699, 1038, 1605, 2300, 2099,
             2571, 403, 1858, 1584, 949, 1765, 678, 2699, 0, 1744, 1645, 653, 600,
             875, 1589, 262, 466, 796, 547, 1724, 1038, 1744, 0, 679, 1272, 1162,
             1420, 1374, 940, 1056, 879, 225, 1891, 1605, 1645, 679, 0, 1017, 1200,
             2145, 357, 1453, 1280, 586, 887, 1114, 2300, 653, 1272, 1017, 0, 504,
             1972, 579, 1260, 987, 371, 999, 701, 2099, 600, 1162, 1200, 504, 0,
        ];

        new TspApproachUnsafe().GetOptimusRoute(data, 13, 0);
    }
}

unsafe class TspApproachUnsafe
{
    int* _data;
    int _depot;
    int _nodes;
    int _nodulesCount;
    int _percent;
    int _distance;
    int* _nodules;
    // iterations   
    long _iterations;
    long _percentSize;
    long _permutation;
    long _fragment;
    StringBuilder _route;

    readonly Stopwatch _now = new();

    // aux
    int _depotIndex;
    int _nodulesBackOne;

    public void GetOptimusRoute(int[] data, int nodesCount, int depot = 0)
    {
        var dataPinned = GCHandle.Alloc(data, GCHandleType.Pinned);
        _data = (int*)dataPinned.AddrOfPinnedObject();
        _depot = depot;
        _nodes = nodesCount;
        _nodulesCount = _nodes - 1;
        _iterations = Factorial(_nodulesCount);
        _percentSize = _iterations / 100;
        _fragment = _percentSize;
        _permutation = 1;
        _distance = int.MaxValue;
        _route = new StringBuilder(256);
        // aux
        _depotIndex = _depot * _nodes;
        _nodulesBackOne = _nodulesCount - 1;

        // arrangement of permutations
        var nodules = new int[_nodulesCount];
        var nodulesPinned = GCHandle.Alloc(nodules, GCHandleType.Pinned);
        _nodules = (int*)nodulesPinned.AddrOfPinnedObject();
        int j = 0;
        for(int i = 0; i < _nodes; i++) {
            if(i != _depot) {
                _nodules[j++] = i;
            }
        }
        Console.WriteLine("Nodes         : {0}", _nodes.ToString());
        Console.WriteLine("Iterations    : {0}", _iterations.ToString());
        Console.WriteLine("Nodules       : {0}", string.Join(" ", nodules));
        _now.Restart();

        // recursive calculation 
        GetRoute(0, _nodulesCount);

        Console.WriteLine("RESULT");
        Console.WriteLine("Optimus route : {0} {1}{2}", _depot.ToString(), _route.ToString(), _depot.ToString());
        Console.WriteLine("Distance      : {0}", _distance.ToString());
        Console.WriteLine("Elapse Time   : {0} ", ElapseTime());
        Console.WriteLine("\nPause");
        Console.ReadKey();
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    void GetRoute(int start, int end)
    {
        if(start == end - 1) {
            // validate distance
            // 1. boundaries A..N, N..A
            var sum = _data[_depotIndex + _nodules[0]] +
                      _data[_nodules[_nodulesBackOne] * _nodes + _depot];
            // 2. route
            for(int i = 0; i < _nodulesBackOne; i++) {
                sum += _data[_nodules[i] * _nodes + _nodules[i + 1]];
            }
            _permutation++;
            if(_distance > sum) {// update minimun
                _distance = sum;
                _route.Clear();
                for(int i = 0; i < _nodulesCount; i++) {
                    _route.Append(_nodules[i]);
                    _route.Append(' ');
                }
            }
            if(_permutation > _fragment) {
                _percent += 1;
                _fragment += _percentSize;
                Console.WriteLine("Permutations: {0} % ", _percent);
            }
        }
        else {
            for(int i = start; i < end; i++) {
                // swap
                Swap(start, i);
                // permute
                GetRoute(start + 1, end);
                // swap
                Swap(start, i);
            }
        }
    }

    long Factorial(int number)
    {
        if(number < 2)
            return 1;
        else
            return number * Factorial(number - 1);
    }

    void Swap(int i, int j)
    {
        int t = _nodules[i];
        _nodules[i] = _nodules[j];
        _nodules[j] = t;
    }

    string ElapseTime() => _now.Elapsed.TotalSeconds.ToString("N4") + " s";
}

