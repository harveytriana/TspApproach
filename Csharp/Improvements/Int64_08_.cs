
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Int64_08_;

unsafe class TspExact
{
    const int DataWidth = 13;
    const int DataLength = 13;
    long* _data;
    long _depot;
    long _nodes;
    long _nodulesCount;
    long _iterations;
    long _percentSize;
    long _percent;
    long _permutation;
    long _distance;
    long* _nodules;
    long[] _route;

    public void GetOptimusRoute(long[] data, long depot = 0)
    {
        var dataPinned = GCHandle.Alloc(data, GCHandleType.Pinned);
        _data = (long*)dataPinned.AddrOfPinnedObject();
        _depot = depot;
        _nodes = DataLength;
        _nodulesCount = _nodes - 1;
        _iterations = Factorial(_nodulesCount);
        _percentSize = _iterations / 100;
        _permutation = 1;
        _distance = long.MaxValue;
        _route = new long[_nodulesCount];

        // arrangement of permutations
        var nodules = new long[_nodulesCount];
        var nodulesPinned = GCHandle.Alloc(nodules, GCHandleType.Pinned);
        _nodules = (long*)nodulesPinned.AddrOfPinnedObject();
        long j = 0;
        for (long i = 0; i < _nodes; i++)
        {
            if (i != _depot)
            {
                _nodules[j++] = i;
            }
        }
        Console.WriteLine("Nodes         : {0}", _nodes.ToString());
        Console.WriteLine("Iterations    : {0:N0}", _iterations.ToString());
        Console.WriteLine("Nodules       : {0}", string.Join(" ", nodules));

        var now = DateTime.Now;

        // recursive calculation 
        GetRoute(0, _nodulesCount);

        Console.WriteLine("RESULT");
        Console.WriteLine("Optimus route : {0} {1}{2}", _depot.ToString(), string.Join(" ", _route), _depot.ToString());
        Console.WriteLine("Distance      : {0}", _distance.ToString());
        Console.WriteLine("Elapse Time   : {0} s", (DateTime.Now - now).TotalSeconds.ToString("N4"));

        _data = null;
        dataPinned.Free();
        _nodules = null;
        nodulesPinned.Free();
    }

    void GetRoute(long start, long end)
    {
        if (start == end - 1)
        {
            // validate distance
            // 1. boundaries A..N, N..A
            var sum = _data[_depot * DataWidth + _nodules[0]] +
                   _data[_nodules[_nodulesCount - 1] * DataWidth + _depot];
            // 2. route
            for (long i = 0; i < _nodulesCount - 1; i++)
            {
                sum += _data[_nodules[i] * DataWidth + _nodules[i + 1]];
            }
            _permutation++;
            if (_distance > sum)
            {// update minimun
                _distance = sum;
                for (long i = 0; i < _nodulesCount; i++)
                {
                    _route[i] = _nodules[i];
                }
            }
            if (_percentSize > 0 && _permutation % _percentSize == 0)
            {
                _percent += 1;
                Console.WriteLine("Permutations: {0} %", _percent.ToString());
            }
        }
        else
        {
            for (var i = start; i < end; i++)
            {
                // swap
                (_nodules[start], _nodules[i]) = (_nodules[i], _nodules[start]);
                // permute
                GetRoute(start + 1, end);
                // swap
                (_nodules[start], _nodules[i]) = (_nodules[i], _nodules[start]);
            }
        }
    }

    long Factorial(long number)
    {
        if (number < 2)
            return 1;
        else
            return number * Factorial(number - 1);
    }
}
