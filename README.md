# Rust, GO, C#, Others in the same Stage

## Traveling Salesman Problem Exact algorithm as Logic Tester

*Update: 22-09-21*

I was curious to write a specific problem in multiple languages to check its performance. I wrote an approach to the classic [TSP](https://en.wikipedia.org/wiki/Travelling_salesman_problem). My algorithm is simple, it measures all permutations to arrive at an exact answer. Of course, due to the exponential nature of the problem, this is not the best solution. However, the goal is not to write a solution to this problem, that in fact a totally perfect one does not exist. The code I wrote is very effective, processing an average of 25 million calculations per second in C#. Initially write in C#, and I did the corresponding translation to Rust and GO. Then, through collaborations, I have added C++, Dart, Python, and the scary Fortran.

Rust is very interesting. I have explored its syntax, and I really liked it. It is elegant and powerful, yet it is clearly orthodox with traditional programming, which puts a steep learning curve on it. Personally, I think that despite arousing interest in the world of programming, it is not going to advance in popularity at the levels of a Python or GO, the trend is for languages with natural, simple, human syntax.

On the other hand, GO is impressive in several aspects. The syntax is very flexible and easy to learn, and at the same time it has an enviable performance. But not everything is a paradise in GO, some experts highlight some flaws of its design such as lack of function overload, lack of generics, lack of dependency injection (unforgivable for .NET programmers).

While C# is an extremely mature language, it covers all software development paradigms. It is my main language.

In any case, its majesty C++ turns out to be the language that solves the same problem in less time. Followed closely by Rust, as expected. The module in C++ was written and compiled with Visual Studio 2022, x64, and running in Release mode, of course.

As a note, I bring up that tests were made with C# integer of 32 (*int* isntead of *long*), and it improves performance by ~15%. GO also increases when using int32. However, with an integer of 32 the solution limit is reduced to 13 nodes or less. 

> It is worth clarifying that for a large number of nodes, perhaps more than 20, specialized algorithms have to be applied, for example, [Google-COR-Tools](https://developers.google.com/optimization/routing/tsp) or [Concorde TSP Solver](https://www.math.uwaterloo.ca/tsp/concorde.html) are suitable. The purpose of this post is not to resolve the TSP issue, per se, is just code.

About Fortran, it is still an experiment, compiled with *gfortran (MinGW.org GCC-6.3.0-1)*, it is possible that there is a better compiler option.

## Results

In each code file, in the header I put a comment block with the results. I dealt with the problem with 13 nodes, taking the data sample that the [Google-COR-Tools](https://developers.google.com/optimization/routing/tsp) documentation shows.

There are three contributions; FORTRAN, Dart, and Python. It is mentioned that there are several variables that could change the results, for example the compilation parameters. The results vary according to the machine where it is executed, but not the order. In summary of results is as follows:

| Language     | Time 13 nodes, s |
| ------------ | ---------------- |
| C++          | 8.4              |
| Rust         | 10.0             |
| GO           | 16.6             |
| C#           | 20.0             |
| Fortran      | 29.7           ° |
| Dart         | 35.0             |
| Python       | 157.7          ° |
|              |                  |

> ° *Collaboration with an expert in this language would be desirable*

## Conclusions

While C# is at a little disadvantage in extreme performance, the margin is not great. It takes into account that it was programmed with objects, and that it is not optimized with unusual things in C# like pointers. Long live C#. On the other hand, Rust with vectors outperforms GO, but not by much. However, Rust, using a predefined matrix, outperforms the others by a significant margin. 

The study continues.

### Epilogue

In the algorithm that I programmed there is a flat in that the number of effective permutations is actually half. However, due to the flow of the permutation calculation, it is difficult to filter which ones are repeated. In other words, the distance the traveler takes to travel a route is the same back and forth, that is why one of the combinations should be discarded. There would be several ways to solve it, but the cost in code would involve high memory consumption and possibly the final time is longer. The programmed algorithm consumes a very small amount of memory, it does not remember anything, it only goes forward. I leave this concern, if anyone wants to improve it.

---

By: Luis Harvey Triana Vega

<small>P.S. Please add a star to this post if it is helpful to you.</small>

**Collaborations**

You can publish a version in another language or start a discussion about something published. Email me to add you as Collaborator. 
