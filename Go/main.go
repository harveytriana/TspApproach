/*
TSP execise

RUN
$ go run main.go

OUTPUT
Nodes         : 13
Iterations    : 479,001,600
Nodules       : 1 2 3 4 5 6 7 8 9 10 11 12
...
Optimus route : 0 7,2,3,4,12,6,8,1,11,10,5,9 0
Distance      :  7293
Elapse Time   :  16.5590105s
*/
package main

import (
	"fmt"
	"math"
	"strings"
	"time"
)

var _data [][]int64
var _depot int64
var _nodes int64
var _nodules_count int64
var _iterations int64
var _percentSize int64
var _percent int64
var _permutation int64
var _minDistance int64
var _route string
var nodules []int64
var now time.Time

func GetOptimusRoute(data [][]int64, depot int64) {
	_data = data
	_depot = depot
	_nodes = int64(len(_data))
	_nodules_count = _nodes - 1
	_iterations = factorial(_nodules_count)
	_percentSize = _iterations / 100
	_permutation = 1
	_minDistance = math.MaxInt64
	now = time.Now()

	// create permutation array
	nodules = make([]int64, _nodules_count)
	var j int64
	var i int64
	for i = 0; i < _nodes; i++ {
		if i != _depot {
			nodules[j] = i
			j += 1
		}
	}

	fmt.Printf("Nodes          : %d\n", _nodes)
	fmt.Printf("Iterations     : %d\n", _iterations)
	fmt.Printf("Nodules        : %s\n", arrayToString(nodules))

	// recursive calculation
	permutations(0, _nodules_count)

	fmt.Println("RESULT")
	fmt.Printf("Optimus route : %d,%s,%d\n", _depot, _route, _depot)
	fmt.Printf("Distance      : %d\n", _minDistance)
	fmt.Printf("Elapse Time   : %v\n", time.Now().Sub(now))
}

func permutations(start int64, end int64) {
	if start == (end - 1) {
		// validate distance
		// 1. boundaries A..N, N..A
		s := _data[_depot][nodules[0]] + _data[nodules[_nodules_count-1]][_depot]
		// 2. route
		for i := 0; i < int(_nodules_count-1); i++ {
			s = s + _data[nodules[i]][nodules[i+1]]
		}
		_permutation += 1
		if _minDistance > s {
			_minDistance = s
			_route = arrayToString(nodules)
		}
		if _permutation%_percentSize == 0 {
			_percent += 1
			fmt.Println("Permutations:", _percent, " %")
		}
	} else {
		for i := start; i < end; i++ {
			// swap
			nodules[start], nodules[i] = nodules[i], nodules[start]
			// permute
			permutations(start+1, end)
			// swap
			nodules[start], nodules[i] = nodules[i], nodules[start]
		}
	}
}

func factorial(n int64) (result int64) {
	if n > 0 {
		result = n * factorial(n-1)
		return result
	}
	return 1
}

func arrayToString(a []int64) string {
	return strings.Trim(strings.Replace(fmt.Sprint(a), " ", ",", -1), "[]")
}

func main() {
	data := [][]int64{
		{0, 2451, 713, 1018, 1631, 1374, 2408, 213, 2571, 875, 1420, 2145, 1972},
		{2451, 0, 1745, 1524, 831, 1240, 959, 2596, 403, 1589, 1374, 357, 579},
		{713, 1745, 0, 355, 920, 803, 1737, 851, 1858, 262, 940, 1453, 1260},
		{1018, 1524, 355, 0, 700, 862, 1395, 1123, 1584, 466, 1056, 1280, 987},
		{1631, 831, 920, 700, 0, 663, 1021, 1769, 949, 796, 879, 586, 371},
		{1374, 1240, 803, 862, 663, 0, 1681, 1551, 1765, 547, 225, 887, 999},
		{2408, 959, 1737, 1395, 1021, 1681, 0, 2493, 678, 1724, 1891, 1114, 701},
		{213, 2596, 851, 1123, 1769, 1551, 2493, 0, 2699, 1038, 1605, 2300, 2099},
		{2571, 403, 1858, 1584, 949, 1765, 678, 2699, 0, 1744, 1645, 653, 600},
		{875, 1589, 262, 466, 796, 547, 1724, 1038, 1744, 0, 679, 1272, 1162},
		{1420, 1374, 940, 1056, 879, 225, 1891, 1605, 1645, 679, 0, 1017, 1200},
		{2145, 357, 1453, 1280, 586, 887, 1114, 2300, 653, 1272, 1017, 0, 504},
		{1972, 579, 1260, 987, 371, 999, 701, 2099, 600, 1162, 1200, 504, 0},
	}

	fmt.Println("Traveling Salesman Problem Exact algorithm")
	GetOptimusRoute(data, 0)
}
