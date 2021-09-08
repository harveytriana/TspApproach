/*
----------------------------------------------------
TSP execise

RUN
cargo run --release

OUTPUT
Nodes         : 13
Iterations    : 479,001,600
Nodules       : 1 2 3 4 5 6 7 8 9 10 11 12
...
Optimus route : 0 [7, 2, 3, 4, 12, 6, 8, 1, 11, 10, 5, 9] 0
Distance      : 7293
Elapse Time   : 10.0019832s
----------------------------------------------------
*/
#![allow(non_snake_case)]

use std::time::Instant;

const NODES: usize = 13;

fn get_optimum_route(data: [[usize; NODES]; NODES], depot: usize) {
    let nodes = data.len();
    let mut min_distance = usize::MAX;
    let nodules_count = nodes - 1;
    let iterations = factorial(nodules_count);
    let percent_size = iterations / 100;
    let mut permutation = 1;
    let mut percent = 0;
    let mut route = String::new();

    // time measure
    let now = Instant::now();

    // create permutation array
    let mut nodules = Vec::new();
    for i in 0..nodes {
        if i != depot {
            nodules.push(i);
        }
    }

    println!("Nodes         : {}", nodes);
    println!("Iterations    : {}", iterations);
    println!("Nodules       : {:?}", nodules);

    // recursive calculation
    get_route(
        &data,
        depot,
        0,
        nodules_count,
        nodules_count,
        percent_size,
        &mut nodules,
        &mut permutation,
        &mut min_distance,
        &mut route,
        &mut percent,
    );

    println!("RESULT");
    println!("Optimus route : {} {} {}", depot, route, depot);
    println!("Distance      : {}", min_distance);
    println!("Elapse Time   : {:?}", now.elapsed());
}

fn get_route(
    data: &[[usize; NODES]; NODES],
    depot: usize,
    start: usize,
    end: usize,
    nodules_count: usize,
    percent_size: usize,
    nodules: &mut [usize],
    permutation: &mut usize,
    min_distance: &mut usize,
    route: &mut String,
    percent: &mut usize,
) {
    if start == end - 1 {
        // validate distance
        let mut s = data[depot][nodules[0]] + data[nodules[nodules_count - 1]][depot];
        for i in 0..nodules_count - 1 {
            s = s + data[nodules[i]][nodules[i + 1]];
        }
        // validate route
        *permutation += 1;
        if *min_distance > s {
            // update minimun
            *min_distance = s;
            route.clear();
            route.push_str(format!("{0} {1:?} {2}", depot, nodules, depot).as_str());
        }
        // show progress
        if percent_size > 0 && *permutation % percent_size == 0 {
            *percent += 1;
            println!("get_route: {0} %", *percent);
        }
    } else {
        for i in start..end {
            nodules.swap(start, i);
            // recursive
            get_route(
                data,
                depot,
                start + 1,
                end,
                nodules_count,
                percent_size,
                nodules,
                permutation,
                min_distance,
                route,
                percent,
            );
            nodules.swap(start, i);
        }
    }
}

fn factorial(num: usize) -> usize {
    match num {
        0 => 1,
        1 => 1,
        _ => factorial(num - 1) * num,
    }
}

fn main() {
    let data: [[usize; NODES]; NODES]  = vec![
        [0, 2451, 713, 1018, 1631, 1374, 2408, 213, 2571, 875, 1420, 2145, 1972,],
        [2451, 0, 1745, 1524, 831, 1240, 959, 2596, 403, 1589, 1374, 357, 579,],
        [713, 1745, 0, 355, 920, 803, 1737, 851, 1858, 262, 940, 1453, 1260,],
        [1018, 1524, 355, 0, 700, 862, 1395, 1123, 1584, 466, 1056, 1280, 987,],
        [1631, 831, 920, 700, 0, 663, 1021, 1769, 949, 796, 879, 586, 371,],
        [1374, 1240, 803, 862, 663, 0, 1681, 1551, 1765, 547, 225, 887, 999,],
        [2408, 959, 1737, 1395, 1021, 1681, 0, 2493, 678, 1724, 1891, 1114, 701,],
        [213, 2596, 851, 1123, 1769, 1551, 2493, 0, 2699, 1038, 1605, 2300, 2099,],
        [2571, 403, 1858, 1584, 949, 1765, 678, 2699, 0, 1744, 1645, 653, 600,],
        [875, 1589, 262, 466, 796, 547, 1724, 1038, 1744, 0, 679, 1272, 1162,],
        [1420, 1374, 940, 1056, 879, 225, 1891, 1605, 1645, 679, 0, 1017, 1200,],
        [2145, 357, 1453, 1280, 586, 887, 1114, 2300, 653, 1272, 1017, 0, 504,],
        [1972, 579, 1260, 987, 371, 999, 701, 2099, 600, 1162, 1200, 504, 0,],
    ];

    println!("Traveling Salesman Problem Exact algorithm");
    get_optimum_route(data, 0);
}
