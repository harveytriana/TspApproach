# Rust, GO, and C# in the same Stage

### Traveling Salesman Problem Exact algorithm

*Update: 08-09-21*

Rust is very interesting. I have explored its syntax, and I really liked it. It is elegant and powerful, yet it is clearly orthodox with traditional programming, which puts a steep learning curve on it. Personally, I think that despite arousing interest in the world of programming, it is not going to advance in popularity at the levels of a Python or GO, the trend is for languages with natural, simple, human syntax.

On the other hand, GO is impressive in several aspects. The syntax is very flexible and easy to learn, and at the same time it has an enviable performance. But not everything is a paradise in GO, some experts highlight some flaws of its design such as lack of function overload, lack of generics, lack of dependency injection (unforgivable for .NET programmers).

While C# is an extremely mature language, it covers all software development paradigms. It is my main language.

I was curious to write and run into a specific problem in all three languages. I wrote an approach to the classic [TSP](https://en.wikipedia.org/wiki/Travelling_salesman_problem), without taking code from others. My algorithm is simple, measuring all the permutations to arrive at an exact answer. Of course, due to the exponential nature of the problem, this is not the best solution. However, the goal is not to write a solution to the problem (which in fact does not exist), but to measure the performance of three languages. The code I wrote is very effective, it processes an average of 25 million calculations per second.

>It is worth clarifying that for a large number of nodes, perhaps more than 20, specialized algorithms have to be applied, for example, [Google-COR-Tools](https://developers.google.com/optimization/routing/tsp) or [Concorde TSP Solver](https://www.math.uwaterloo.ca/tsp/concorde.html) are suitable. The purpose of this post is not to resolve the TSP issue, per se, is just code.

## Results

In each code file, in the header I put a comment block with the results. I dealt with the problem with 13 nodes, taking the data sample that the [Google-COR-Tools](https://developers.google.com/optimization/routing/tsp) documentation shows.

## Conclusions

While C# is at a disadvantage in extreme performance, the margin is not great. It takes into account that it was programmed with objects, and that it is not optimized with unusual things in C# like pointers. Long live C#. On the other hand, Rust with vectors outperforms GO, but not by much. However, Rust, using a predefined matrix, outperforms the others by a significant margin. 

### Epilogue

In the algorithm that I programmed there is a flat in that the number of effective permutations is actually half. However, due to the flow of the permutation calculation, it is difficult to filter which ones are repeated. In other words, the distance the traveler takes to travel a route is the same back and forth, that is why one of the combinations should be discarded. There would be several ways to solve it, but the cost in code would involve high memory consumption and possibly the final time is longer. The programmed algorithm consumes a very small amount of memory, it does not remember anything, it only goes forward. I leave this concern, if anyone wants to improve it.

---
*By: Luis Harvey Triana Vega | https://www.blazorspread.net*

<small>P.S. Please add a star to this post if it is helpful to you.</small>

*Update: 05-09-21*
