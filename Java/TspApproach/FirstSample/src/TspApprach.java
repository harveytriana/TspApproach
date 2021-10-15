/*
----------------------------------------------------
TSP execise
OpenJDK 11 (LTS) Windows X64

RUN: Ctrl+F5 (vscode)

OUTPUT
Nodes         : 13
Iterations    : 479,001,600
Nodules       : 1 2 3 4 5 6 7 8 9 10 11 12
...
Optimus route : 7 2 3 4 12 6 8 1 11 10 5 9
Distance      : 7293
Elapse time   : 13.204
----------------------------------------------------
*/
public class TspApprach {
    static int[][] _data = { { 0, 2451, 713, 1018, 1631, 1374, 2408, 213, 2571, 875, 1420, 2145, 1972 },
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
            { 1972, 579, 1260, 987, 371, 999, 701, 2099, 600, 1162, 1200, 504, 0 } };

    static int _nodes = _data[0].length;
    static int _depot;
    static int _nodulesCount;
    static int _minDistance;
    static int _percent;
    static long _percentSize;
    static long _permutations;
    static long _permutation;
    static long _fragment;
    static String _route;
    static int _nodules[] = new int[_nodes - 1];

    public static void main(String[] args) throws Exception {
        System.out.println("Traveling Salesman Problem Exact algorithm");
        _depot = 0;
        _nodulesCount = _nodes - 1;
        _permutations = factorial(_nodulesCount);
        _percentSize = _permutations / 100;
        _fragment= _percentSize;
        _percent = 0;
        _permutation = 1;
        _minDistance = 999999;

        // arrangement of permutations
        int j = 0;
        for (int i = 0; i < _nodes; i++) {
            if (i != _depot) {
                _nodules[j++] = i;
            }
        }
        getRouteString();

        System.out.println("Nodes        : " + _nodes);
        System.out.println("Permutations : " + _permutations);
        System.out.println("Nodules      : " + _route);

        long now = System.currentTimeMillis();

        // recursive calculation
        getRoute(0, _nodulesCount);

        long end = System.currentTimeMillis();

        System.out.println("RESULT");
        System.out.println("Optimus route : " + _route);
        System.out.println("Distance      : " + _minDistance);
        System.out.println("Elapse time   : " + (end - now) / 1000.0);
    }

    static void getRoute(int start, int end) {
        if (start == end - 1) {
            // validate distance
            // 1. boundaries A..N, N..A
            int sum = _data[_depot][_nodules[0]] + _data[_nodules[_nodulesCount - 1]][_depot];
            // 2. route
            for (int i = 0; i < _nodulesCount - 1; i++) {
                sum += _data[_nodules[i]][_nodules[i + 1]];
            }
            _permutation++;
            if (_minDistance > sum) { // update minimun
                _minDistance = sum;
                getRouteString();
            }
            if (_permutation >_fragment) {
                _percent += 1;
                _fragment += _percentSize;
                System.out.println("Permutations : " + _percent + " % | " + _permutation);
            }
        } else {
            for (int i = start; i < end; i++) {
                // swap
                swap(start, i);
                // permute
                getRoute(start + 1, end);
                // swap
                swap(start, i);
            }
        }
    }

    static void swap(int a, int b) {
        int t = _nodules[a];
        _nodules[a] = _nodules[b];
        _nodules[b] = t;
    }

    static long factorial(int n) {
        if (n == 0)
            return 1;

        return n * factorial(n - 1);
    }

    static void getRouteString() {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < _nodules.length; i++) {
            stringBuilder.append(_nodules[i]);
            stringBuilder.append(' ');
        }
        _route = stringBuilder.toString();
    }
}
