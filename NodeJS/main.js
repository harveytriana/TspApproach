/*
Traveling Salesman Problem 

RUN
$ node main.js

Nodes         :  13
Iterations    :  479001600
Nodules       :  1,2,3,4,5,6,7,8,9,10,11,12
...
RESULT
Optimum route :  7,2,3,4,12,6,8,1,11,10,5,9
Distance      :  7293
Elapse time   :  27.122  s
*/

var _data;
var _depot;
var _nodes;
var _nodulesCount;
var _iterations;
var _percentSize;
var _percent;
var _permutation;
var _minDistance;
var _route;
var _nodules;
var _swap;

main();

function main() {
    console.log("Traveling Salesman Problem Exact algorithm");

    _data = [
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
    ];
    // 4 nodes sample
    // _data = [
    //     [00, 10, 35, 30],
    //     [10, 00, 30, 15],
    //     [35, 30, 00, 30],
    //     [30, 15, 30, 00],
    // ];

    _depot = 0;
    _nodes = _data.length;
    _nodulesCount = _nodes - 1;
    _iterations = factorial(_nodulesCount);
    _percentSize = (_iterations / 100);
    _permutation = 1;
    _minDistance = 999999;
    _percent = 0;

    _nodules = [];
    for (let i = 0; i < _nodes; i++) {
        if (i != _depot) {
            _nodules.push(i);
        }
    }

    console.log("Nodes         : ", _nodes);
    console.log("Iterations    : ", _iterations);
    console.log("Nodules       : ", _nodules.toString());

    let now = new Date().getTime();

    // recursive calculation
    getRoute(0, _nodulesCount);

    e = (new Date().getTime() - now) / 1000.0;

    console.log("RESULT")
    console.log("Optimum route : ", _route);
    console.log("Distance      : ", _minDistance);
    console.log("Elapse time   : ", e, " s");
}

function getRoute(start, end) {
    if (start == end - 1) {
        // calculate distance
        // 1. boundaries A..N, N..A
        let s = _data[_depot][_nodules[0]] + _data[_nodules[_nodulesCount - 1]][_depot];
        // 2. route
        for (let i = 0; i < _nodulesCount - 1; i++) {
            s += _data[_nodules[i]][_nodules[i + 1]];
        }
        _permutation++;
        if (_minDistance > s) {
            // update minimun
            _minDistance = s;
            // let route
            _route = _nodules.toString();
        }
        if (_percentSize > 0) {
            if (_permutation % _percentSize == 0) {
                _percent += 1;
                console.log("Permutations: ", _percent, " %");
            }
        }
    } else {
        for (let i = start; i < end; i++) {
            // swap
            swap(start, i);
            // permute
            getRoute(start + 1, end);
            // swap
            swap(start, i);
        }
    }
}

function swap(i, j) {
    _swap = _nodules[i];
    _nodules[i] = _nodules[j];
    _nodules[j] = _swap;
}

function factorial(n) {
    var f = 1;
    for (let i = 1; i <= n; i++) {
        f = f * i;
    }
    return f;
}