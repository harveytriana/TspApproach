
/*
----------------------------------------------------
TSP execise
RUN
Writing in Visual Studio 2022, Release Mode, x64

OUTPUT
Nodes         : 13
Iterations    : 479,001,600
Nodules       : 1 2 3 4 5 6 7 8 9 10 11 12
...
Optimus route : 7 2 3 4 12 6 8 1 11 10 5 9
Distance      : 7293
Elapse time   : 8.213
----------------------------------------------------
*/

#include <iostream>
#include <time.h>
#include <string>

using namespace std;

const long NODES = 13;

// INTERFACE
void getRoute(long start, long end);
void getRouteString();
long factorial(long number);
void swap(long* x, long* y);

long _data[NODES][NODES] = {
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

long _depot;
long _nodes;
long _nodulesCount;
long _iterations;
long _percentSize;
long _percent;
long _permutation;
long _minDistance;
long _nodules[NODES - 1];

std::string _routeString;

int main()
{
    cout << "Traveling Salesman Problem Exact algorithm" << endl;

    _depot = 0;
    _nodes = NODES;
    _nodulesCount = NODES - 1;
    _iterations = factorial(_nodulesCount);
    _percentSize = _iterations / 100;
    _permutation = 1;
    _minDistance = 999999;

    // arrangement of permutations
    long j = 0;
    for (long i = 0; i < _nodes; i++)
    {
        if (i != _depot)
        {
            _nodules[j++] = i;
        }
    }
    getRouteString();

    cout << "Nodes         : " << _nodes << endl;
    cout << "Iterations    : " << _iterations << endl;
    cout << "Nodules       : " << _routeString << endl;

    clock_t now = clock();

    // recursive calculation
    getRoute(0, _nodulesCount);

    clock_t stop = clock();

    cout << "RESULT" << endl;
    cout << "Optimus route : " << _routeString << endl;
    cout << "Distance      : " << _minDistance << endl;
    cout << "Elapse time   : " << (double)(clock() - now) / CLOCKS_PER_SEC << endl;

    system("pause>0");
}

long factorial(long number)
{
    if (number < 2)
        return(1);
    return(number * factorial(number - 1));
}

void getRoute(long start, long end)
{
    if (start == end - 1)
    {
        // validate distance
        // 1. boundaries A..N, N..A
        long sum = _data[_depot][_nodules[0]] +
            _data[_nodules[_nodulesCount - 1]][_depot];
        // 2. route
        for (long i = 0; i < _nodulesCount - 1; i++)
        {
            sum += _data[_nodules[i]][_nodules[i + 1]];
        }
        _permutation++;
        if (_minDistance > sum)
        { // update minimun
            _minDistance = sum;
            getRouteString();
        }
        if (_percentSize > 0 && _permutation % _percentSize == 0)
        {
            _percent += 1;
            cout << "Permutations : " << _percent << " %" << sum << endl;
        }
    }
    else
    {
        for (long i = start; i < end; i++)
        {
            // swap
            swap(&_nodules[start], &_nodules[i]);
            // permute
            getRoute(start + 1, end);
            // swap
            swap(&_nodules[start], &_nodules[i]);
        }
    }
}

void swap(long* x, long* y)
{
    long t = *x;
    *x = *y;
    *y = t;
}

void getRouteString()
{
    _routeString.clear();
    for (long i : _nodules) {
        _routeString += std::to_string(i);
        _routeString += " ";
    }
}
