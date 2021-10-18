## REQUIREMENTS

- Visual Studio 2022

To obtain the maximum performance, the program must be run in Release mode.

Edit main.cs to modify which test to run.

Switch from .Net 5.0 to .Net 6.0 rc1 made test go 4.6% faster.

## Array access

In C# memory safety is opt-out. So when comparing with languages without memory safety such as purely array-driven access (bounds check) will always be slower. There are however some simple tricks for speeding up array access in certain cases, as described here: [https://blog.tedd.no/2020/06/01/faster-c-array-access/](https://blog.tedd.no/2020/06/01/faster-c-array-access/)). C# also has a problem with multidimensional arrays, where both jagged arrays or one-dimensional arrays are significantly faster. Using unsafe array access is any way faster, and removed 24-28% of the time for this test.

## Results of improvements

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1237 (21H1/May2021Update)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
Frequency=14318180 Hz, Resolution=69.8413 ns, Timer=HPET
.NET SDK=6.0.100-rc.1.21463.6
  [Host]     : .NET 6.0.0 (6.0.21.45113), X64 RyuJIT
  Job-SKRSDE : .NET 6.0.0 (6.0.21.45113), X64 RyuJIT

IterationCount=5  LaunchCount=1  WarmupCount=1  

```
| Method                  | Categories |    Mean |    Error |   StdDev | Ratio | RatioSD | Allocated |
| ----------------------- | ---------- | ------: | -------: | -------: | ----: | ------: | --------: |
| Int32_01_Original       | Int32      | 8.565 s | 0.0532 s | 0.0082 s |  1.00 |    0.00 |     11 KB |
| Int32_03_MD2SD_Array    | Int32      | 7.395 s | 0.1710 s | 0.0444 s |  0.86 |    0.01 |     11 KB |
| Int32_04_LocalCopyArray | Int32      | 6.189 s | 0.0355 s | 0.0092 s |  0.72 |    0.00 |     11 KB |
| Int32_05_UnsafeArrays   | Int32      | 5.771 s | 0.0135 s | 0.0035 s |  0.67 |    0.00 |     11 KB |
| Int32_06_NoBoxing       | Int32      | 5.672 s | 0.0026 s | 0.0007 s |  0.66 |    0.00 |     11 KB |
| Int32_07_RouteArray     | Int32      | 5.739 s | 0.3022 s | 0.0785 s |  0.67 |    0.01 |     12 KB |
|                         |            |         |          |          |       |         |           |
| Int64_01_Original       | Int64      | 8.348 s | 0.4143 s | 0.1076 s |  1.00 |    0.00 |      4 KB |
| Int64_02_TimingBug      | Int64      | 8.357 s | 0.6782 s | 0.1049 s |  1.00 |    0.01 |      4 KB |
| Int64_03_MD2SD_Array    | Int64      | 6.520 s | 0.0045 s | 0.0007 s |  0.78 |    0.01 |      4 KB |
| Int64_04_LocalCopyArray | Int64      | 6.315 s | 0.3383 s | 0.0879 s |  0.76 |    0.02 |      4 KB |
| Int64_05_UnsafeArrays   | Int64      | 4.802 s | 0.1625 s | 0.0422 s |  0.58 |    0.01 |      4 KB |
| Int64_06_NoBoxing       | Int64      | 4.808 s | 0.1858 s | 0.0483 s |  0.58 |    0.01 |      4 KB |
| Int64_07_RouteArray     | Int64      | 4.760 s | 0.0251 s | 0.0065 s |  0.57 |    0.01 |      4 KB |
