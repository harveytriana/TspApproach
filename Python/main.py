"""
TSP execise

RUN
$ py main.py

OUTPUT
Nodes         : 13
Iterations    : 479,001,600
Nodules       : 1 2 3 4 5 6 7 8 9 10 11 12
...
RESULT
Optimus route  : 0 7 10 5 11 1 8 6 12 4 3 2 9 0
Distance       : 7632
Elapse time    : 290.47128772735596s (!)

NOTE. A Python expert would write it better, and optimized run
"""
import math
import time
from array import *

_data = [[]]
_depot = 0
_nodes = 0
_nodules_count = 0
_iterations = 0
_percentSize = 0
_percent = 0
_permutation = 0
_minDistance = 0
_route = ""
_nodules = []


def main():
    print("Traveling Salesman Problem Exact algorithm")
    data = [
        [0, 2451, 713, 1018, 1631, 1374, 2408, 213, 2571, 875, 1420, 2145, 1972],
        [2451, 0, 1745, 1524, 831, 1240, 959, 2596, 403, 1589, 1374, 357, 579],
        [713, 1745, 0, 355, 920, 803, 1737, 851, 1858, 262, 940, 1453, 1260],
        [1018, 1524, 355, 0, 700, 862, 1395, 1123, 1584, 466, 1056, 1280, 987],
        [1631, 831, 920, 700, 0, 663, 1021, 1769, 949, 796, 879, 586, 371],
        [1374, 1240, 803, 862, 663, 0, 1681, 1551, 1765, 547, 225, 887, 999],
        [2408, 959, 1737, 1395, 1021, 1681, 0, 2493, 678, 1724, 1891, 1114, 701],
        [213, 2596, 851, 1123, 1769, 1551, 2493, 0, 2699, 1038, 1605, 2300, 2099],
        [2571, 403, 1858, 1584, 949, 1765, 678, 2699, 0, 1744, 1645, 653, 600],
        [875, 1589, 262, 466, 796, 547, 1724, 1038, 1744, 0, 679, 1272, 1162],
        [1420, 1374, 940, 1056, 879, 225, 1891, 1605, 1645, 679, 0, 1017, 1200],
        [2145, 357, 1453, 1280, 586, 887, 1114, 2300, 653, 1272, 1017, 0, 504],
        [1972, 579, 1260, 987, 371, 999, 701, 2099, 600, 1162, 1200, 504, 0],
    ]
    # data = [
    #     [00, 10, 35, 30],
    #     [10, 00, 30, 15],
    #     [35, 30, 00, 30],
    #     [30, 15, 30, 00],
    # ]
    get_optimus_route(data, 0)


def get_optimus_route(data, depot):
    global _data
    global _depot
    global _nodules_count
    global _iterations
    global _percentSize
    global _permutation
    global _minDistance
    global _nodules
    global _route
    # ...
    _data = data
    _depot = depot
    _nodes = len(_data)
    _nodules_count = _nodes - 1
    _iterations = math.factorial(_nodules_count)
    _percentSize = _iterations / 100
    _permutation = 1
    _minDistance = 999999
    now = time.time()

    # create permutation array
    for i in range(_nodes):
        if i != _depot:
            _nodules.append(i)

    print(f"Nodes          : {_nodes}")
    print(f"Iterations     : {_iterations}")
    print(f"Nodules        : {_nodules}")

    # recursive calculation
    get_route(0, _nodules_count)

    print("RESULT")
    print(f"Optimus route  : {_depot} {_route}{_depot}")
    print(f"Distance       : {_minDistance}")
    print(f"Elapse time    : {time.time() - now}s")


def get_route(start, end):
    global _data
    global _depot
    global _nodules_count
    global _iterations
    global _percentSize
    global _permutation
    global _minDistance
    global _nodules
    global _route
    global _percent
    # ...
    if start == (end - 1):
        s = _data[_depot][_nodules[0]] + _data[_nodules[_nodules_count-1]][_depot]
        for i in range(_nodules_count-1):
            s = s + _data[_nodules[i]][_nodules[i+1]]
        # print("first len: ", s)
        _permutation += 1
        if _minDistance > s:
            _minDistance = s
            _route = array_to_string(_nodules)
        if _permutation % _percentSize == 0:
            _percent += 1
            print(f"get_route: {_percent} %")
    else:
        for i in range(start+1, end):
            # swap
            _nodules[start], _nodules[i] = _nodules[i], _nodules[start]
            # permute
            get_route(start+1, end)
            # swap
            _nodules[start], _nodules[i] = _nodules[i], _nodules[start]


def array_to_string(s):
    result = ""
    for i in s:
        result += str(i)
        result += " "
    return (result)


if __name__ == "__main__":
    main()
