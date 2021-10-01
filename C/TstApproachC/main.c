#include <stdio.h>
#include <limits.h>
#include <time.h>
#include <string.h>

#define NODES 13

// INTERFACE
void tspExact();
void routeString();
void swap(long* x, long* y);
long factorial(long number);
void getRoute(long start, long end);
void printRoute();

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
long _route[NODES - 1];

void main()
{
    printf("Traveling Salesman Problem Exact algorithm\n");

    tspExact();

    printf("End Program");
}

void tspExact()
{
    _depot = 0;
    _nodes = NODES;
    _nodulesCount = NODES - 1;
    _iterations = factorial(_nodulesCount);
    _percentSize = _iterations / 100;
    _permutation = 1;
    _minDistance = LONG_MAX;

    clock_t now = clock();

    // arrangement of getRoute
    int j = 0;
    for (int i = 0; i < _nodes; i++)
    {
        if (i != _depot)
        {
            _nodules[j] = i;
            _route[j] = i;
            j++;
        }
    }

    printf("Nodes         : %d\n", _nodes);
    printf("Iterations    : %d\n", _iterations);
    printf("NODULES\n");
    printRoute();

    // recursive calculation
    getRoute(0, _nodulesCount);

    printf("ROUTE\n");
    printRoute();
    printf("Distance      : %d\n", _minDistance);
    printf("Elapse time   : %f\n", (double)(clock() - now) / CLOCKS_PER_SEC);

}

void getRoute(long start, long end)
{
    if (start == end - 1)
    {
        // validate distance
        // 1. boundaries A..N, N..A
        long s = _data[_depot][_nodules[0]] +
            _data[_nodules[_nodulesCount - 1]][_depot];
        // 2. route
        for (int i = 0; i < _nodulesCount - 1; i++)
        {
            s += _data[_nodules[i]][_nodules[i + 1]];
        }
        _permutation++;
        if (_minDistance > s)
        { // update minimun
            _minDistance = s;
            for (int i = 0; i < _nodulesCount; i++)    _route[i] = _nodules[i];
        }
        if (_percentSize > 0 && _permutation % _percentSize == 0)
        {
            _percent += 1;
            printf("Permutations: %d\n", _percent);
        }
    }
    else
    {
        for (int i = start; i < end; i++)
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

long factorial(long number)
{
    if (number < 2)
        return 1;
    else
        return number * factorial(number - 1);
}

void swap(long* x, long* y)
{
    long t = *x;
    *x = *y;
    *y = t;
}

void printRoute()
{
    for (int i = 0; i < _nodulesCount; i++) {
        printf(" NODE %d\n", _route[i]);
    }
}
