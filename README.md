# Comparing performance of different languages solving the same problem

***Traveling Salesman Problem Exact algorithm as Logic Tester***

*Update: 15-10-21*

## Resume

It is oriented to write the equivalent of the same algorithm, as far as possible, to solve a classic extreme calculation problem, and to measure the performance trend in several programming languages. To be more precise, they run on the same physical infrastructure. This is a classic approach [TSP](https://en.wikipedia.org/wiki/Travelling_salesman_problem). My algorithm is simple, it measures all permutations to arrive at an exact answer. Of course, due to the exponential nature of the problem, this is not a general solution. However, the goal is not to write a point solution to this problem, but to measure performance. The code I wrote is very effective, processing an average of 30 million calculations per second in C#. Gradually translations into other languages and optimizations have been made, including contributions.

## Comments

Rust is very interesting. I have explored its syntax, and I really liked it. It is elegant and powerful, yet it is clearly «heterodox» with traditional programming, which puts a steep learning curve on it. Personally, I think that despite arousing interest in the world of programming, it is not going to advance in popularity at the levels of a Python or GO, the trend is for languages with natural, simple, human syntax.

On the other hand, GO is impressive in several aspects. The syntax is very flexible and easy to learn, and at the same time it has an enviable performance. But not everything is a paradise in GO, some experts highlight some flaws of its design such as lack of function overload, lack of generics, lack of dependency injection (unforgivable for .NET programmers).

In any case, C turns out to be the language that solves the same problem in less time, followed closely by C++ and Fortran. 

> It is worth clarifying that for a large number of nodes, perhaps more than 20, specialized algorithms have to be applied, for example, [Google-COR-Tools](https://developers.google.com/optimization/routing/tsp) or [Concorde TSP Solver](https://www.math.uwaterloo.ca/tsp/concorde.html) are suitable. The purpose of this post is not to resolve the classic TSP, per se, is just code.

In each code file, in the header I put a comment block with the results. I dealt with the problem with 13 nodes, taking the data sample that the [Google-COR-Tools](https://developers.google.com/optimization/routing/tsp) documentation shows.

There are contributions; FORTRAN, Dart, and Python. It is mentioned that there are several variables that could change the results, for example the compilation parameters. The results vary according to the machine where it is executed, but not the order. In summary, the same problem, running in the same machine, with 13 nodes, the results is as follows:

About Fortran, initially I compiled with *gfortran MinGW-W64 8.1.0*, with uncertain results, it took 29.7 seconds. Tried applying the available options for optimization with the same poor result to be Fortran.

> MinGW gfortran is an easy compiler with which you can run Fortran programs from a terminal, but the result is disappointing.

Then I work with Intel's developer tools, Intel oneApi©, certainly advanced, with the final result shown in the table, now fortran is at the same level as C++.

> With Intel oneApi© and Visual Studio (for now 2019) we can develop Fortran programs with a professional IDE.

## Results

Of course, the calculated times can vary slightly if you run on another machine, however, the ratio should be practically constant.

| Language     | Elapsed time, s     |
| ------------ | ------------------- |
| C            | 6.60 x86 | 7.68 x64 |
| C++          | 6.68 x86 | 7.54 x64 |
| Fortran      | 7.39 x86 | 7.42 x64 |
| Rust         | 10.0                |
| C#           | 11.20 (1)           |
| GO           | 11.95               |
| Java         | 13.20               |
| C#           | 16.50 (2)           |
| NodeJS       | 25.25               |
| Dart         | 35.0                |
| Python       | 157.0 (3)           |

As theoretically expected, C and C++ are the languages that solves the problem in less time. On the other hand, Rust proves to be the powerful language that it is. While C# is at a little disadvantage in extreme performance, the margin is not great.  

(1) By virtue of excellent collaboration, applying advanced optimization techniques in C#, the result of this can be improved. However, the application of these techniques is heterodox to normal coding in C#. The result in the table corresponds to long* (for int* the time is around 12.2 s). Read the README document in the corresponding folder for more information. Thanks Tedd; https://github.com/tedd. 

(2) For standard C# code, elapse time is ~17 s. I mean by "standard" one to which extreme optimizations were not applied.

(3) About Python, an executable was created from the Script using Pyinstaller, however the performance is still far from the others. I would like a Python expert to review the code or other compilation tool, it may be possible to write better code and improve the results.

The study can continue.This is only a hint of the matter, and is possibly not determinative.

### Epilogue

In the algorithm that I programmed there is a flat in that the number of effective permutations is actually half. However, due to the flow of the permutation calculation, it is difficult to filter which ones are repeated. In other words, the distance the traveler takes to travel a route is the same back and forth, that is why one of the combinations should be discarded. There would be several ways to solve it, but the cost in code would involve high memory consumption and possibly the final time is longer. The programmed algorithm consumes a very small amount of memory, it does not remember anything, it only goes forward. I leave this concern, if anyone wants to improve it.

---

By: Luis Harvey Triana Vega

<small>P.S. Please add a star to this post if it is helpful to you.</small>

**Collaborations**

You can publish a version in another language or start a discussion about something published. Email me to add you as Collaborator. 
