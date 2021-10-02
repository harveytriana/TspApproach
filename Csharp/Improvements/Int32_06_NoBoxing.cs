
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Int32_06_NoBoxing;

unsafe class TspExact
{
    const int DataWidth = 13;
    const int DataLength = 13;
    int* _data;
    int _depot;
    int _nodes;
    int _nodulesCount;
    int _iterations;
    int _percentSize;
    int _percent;
    int _permutation;
    int _distance;
    int* _nodules;
    StringBuilder _route;
    DateTime _now;

    public void GetOptimusRoute(int[] data, int depot = 0)
    {
        if ((_nodes = DataLength) > 13)
        {
            Console.WriteLine("The maximum number of nodes for Int32 is 13.");
            return;
        }

        var dataPinned = GCHandle.Alloc(data, GCHandleType.Pinned);
        _data = (int*)dataPinned.AddrOfPinnedObject(); _depot = depot;
        _nodulesCount = _nodes - 1;
        _iterations = Factorial(_nodulesCount);
        _percentSize = _iterations / 100;
        _permutation = 1;
        _distance = int.MaxValue;
        _route = new StringBuilder();

        // arrangement of permutations
        var nodules = new int[_nodulesCount];
        var nodulesPinned = GCHandle.Alloc(nodules, GCHandleType.Pinned);
        _nodules = (int*)nodulesPinned.AddrOfPinnedObject();
        int j = 0;
        for (int i = 0; i < _nodes; i++)
        {
            if (i != _depot)
            {
                _nodules[j++] = i;
            }
        }
        Console.WriteLine("Nodes         : {0}", _nodes.ToString());
        Console.WriteLine("Iterations    : {0:N0}", _iterations.ToString());
        Console.WriteLine("Nodules       : {0}", string.Join(" ", nodules));

        _now = DateTime.Now;

        // recursive calculation 
        GetRoute(0, _nodulesCount);

        Console.WriteLine("RESULT");
        Console.WriteLine("Optimus route : {0} {1}{2}", _depot.ToString(), _route.ToString(), _depot.ToString());
        Console.WriteLine("Distance      : {0}", _distance.ToString());
        Console.WriteLine("Elapse Time   : {0} ", ElapseTime());
    }

    void GetRoute(int start, int end)
    {
        if (start == end - 1)
        {
            // validate distance
            // 1. boundaries A..N, N..A
            var sum = _data[_depot * DataWidth + _nodules[0]] +
                   _data[_nodules[_nodulesCount - 1] * DataWidth + _depot];
            // 2. route
            for (int i = 0; i < _nodulesCount - 1; i++)
            {
                sum += _data[_nodules[i] * DataWidth + _nodules[i + 1]];
            }
            _permutation++;
            if (_distance > sum)
            {// update minimun
                _distance = sum;
                _route.Clear();
                for (int i = 0; i < _nodulesCount; i++)
                {
                    _route.Append(_nodules[i]);
                    _route.Append(" ");
                }
            }
            if (_percentSize > 0 && _permutation % _percentSize == 0)
            {
                _percent += 1;
                Console.WriteLine("Permutations: {0} % {1}", _percent.ToString(), ElapseTime());
            }
        }
        else
        {
            for (var i = start; i < end; i++)
            {
                // swap
                // Swap(start, i);
                (_nodules[start], _nodules[i]) = (_nodules[i], _nodules[start]);
                // permute
                GetRoute(start + 1, end);
                // swap
                // Swap(start, i);
                (_nodules[start], _nodules[i]) = (_nodules[i], _nodules[start]);
            }
        }
    }

    int Factorial(int number)
    {
        if (number < 2)
            return 1;
        else
            return number * Factorial(number - 1);
    }

    string ElapseTime() => (DateTime.Now - _now).TotalSeconds.ToString("N2") + " s";
}