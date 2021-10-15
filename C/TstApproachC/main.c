/*
----------------------------------------------------
TSP execise
RUN
Open Solution, then Start withoput Debugging

OUTPUT
Nodes         : 13
Iterations    : 479,001,600
Nodules       : 1 2 3 4 5 6 7 8 9 10 11 12
...
Optimus route : 7 2 3 4 12 6 8 1 11 10 5 9
Distance      : 7293
Elapse time   : 7.68 x64 | 6.60 x86
----------------------------------------------------
*/
#include <stdio.h>
#include <limits.h>
#include <time.h>
#include <string.h>

#define NODES 13

// INTERFACE
void tspExact();
void swap(int* x, int* y);
int factorial(int number);
void getRoute(int start, int end);
void printRoute();

int _data[NODES][NODES] = {
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

int _depot;
int _nodes;
int _nodulesCount;
int _percent;
int _minDistance;
int _nodules[NODES - 1];
int _route[NODES - 1];
//
long _iterations;
long _percentSize;
long _permutation;
long _fragment;

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
    _fragment = _percentSize;
    _permutation = 1;
    _minDistance = 999999;

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

void getRoute(int start, int end)
{
    if (start == end - 1)
    {
        // validate distance
        // 1. boundaries A..N, N..A
        int s = _data[_depot][_nodules[0]] +
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
        if (_permutation > _fragment)
        {
            _percent += 1;
            _fragment += _percentSize;
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

long factorial(int number)
{
    if (number < 2)
        return 1;
    else
        return number * factorial(number - 1);
}

void swap(int* x, int* y)
{
    int t = *x;
    *x = *y;
    *y = t;
}

void printRoute()
{
    for (int i = 0; i < _nodulesCount; i++) {
        printf(" NODE %d\n", _route[i]);
    }
}
