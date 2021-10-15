/*
Traveling Salesman Problem 

RUN
$ dart main.dart

OUTPUT
Nodes         : 13
Iterations    : 479,001,600
Nodules       : 1 2 3 4 5 6 7 8 9 10 11 12
...
Optimum route : 0 7 2 3 4 12 6 8 1 11 10 5 9 0
Distance      : 7293
Elapse Time   : 35
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
var _fragment;

main(List<String> args) {
  print("Traveling Salesman Problem Exact algorithm");
  // 13 nodes
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
  //   [00, 10, 35, 30],
  //   [10, 00, 30, 15],
  //   [35, 30, 00, 30],
  //   [30, 15, 30, 00],
  // ];

  _depot = 0;
  _nodes = _data.length;
  _nodulesCount = _nodes - 1;
  _iterations = factorial(_nodulesCount);
  _percentSize = (_iterations / 100).toInt();
  _fragment = _percentSize;
  _permutation = 1;
  _minDistance = 999999;
  _percent = 0;

  List<int> a = [];
  for (int i = 0; i < _nodes; i++) {
    if (i != _depot) {
      a.add(i);
    }
  }
  _nodules = a;

  print("Nodes         : $_nodes");
  print("Iterations    : $_iterations");
  print("Nodules       : $_nodules");

  Stopwatch stopwatch = new Stopwatch()..start();

  // recursive calculation
  getRoute(0, _nodulesCount);

  print("RESULT");
  print("Optimum route : $_depot $_route$_depot");
  print("Distance      : $_minDistance");
  print("Elapse Time   : ${stopwatch.elapsed.inSeconds}");
}

getRoute(int start, int end) {
  if (start == end - 1) {
    // calculate distance
    // 1. boundaries A..N, N..A
    var s =
        _data[_depot][_nodules[0]] + _data[_nodules[_nodulesCount - 1]][_depot];
    // 2. route
    for (var i = 0; i < _nodulesCount - 1; i++) {
      s += _data[_nodules[i]][_nodules[i + 1]];
    }
    _permutation++;
    if (_minDistance > s) {
      // update minimun
      _minDistance = s;
      // let route
      NodulesString();
    }
    if (_permutation > _fragment) {
      _percent += 1;
      _fragment += _percentSize;
      print("Permutations: $_percent %");
    }
  } else {
    for (var i = start; i < end; i++) {
      // swap
      swap(start, i);
      // permute
      getRoute(start + 1, end);
      // swap
      swap(start, i);
    }
  }
}

int factorial(int n) {
  int result;
  if (n > 0) {
    result = n * factorial(n - 1);
    return result;
  }
  return 1;
}

swap(int i, int j) {
  _swap = _nodules[i];
  _nodules[i] = _nodules[j];
  _nodules[j] = _swap;
}

NodulesString() {
  _route = "";
  for (var i = 0; i < _nodulesCount; i++) {
    _route = _route + _nodules[i].toString() + " ";
  }
}
