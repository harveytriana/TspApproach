using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace tsp.Improvements
{
    [SimpleJob(launchCount: 1, warmupCount: 1, targetCount: 5)]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory), CategoriesColumn]
    [MarkdownExporterAttribute.GitHub]
    [MemoryDiagnoser]
    public class Benchmark
    {
        static readonly int[] data32SD = {
             0, 2451, 713, 1018, 1631, 1374, 2408, 213, 2571, 875, 1420, 2145, 1972,
             2451, 0, 1745, 1524, 831, 1240, 959, 2596, 403, 1589, 1374, 357, 579,
             713, 1745, 0, 355, 920, 803, 1737, 851, 1858, 262, 940, 1453, 1260,
             1018, 1524, 355, 0, 700, 862, 1395, 1123, 1584, 466, 1056, 1280, 987,
             1631, 831, 920, 700, 0, 663, 1021, 1769, 949, 796, 879, 586, 371,
             1374, 1240, 803, 862, 663, 0, 1681, 1551, 1765, 547, 225, 887, 999,
             2408, 959, 1737, 1395, 1021, 1681, 0, 2493, 678, 1724, 1891, 1114, 701,
             213, 2596, 851, 1123, 1769, 1551, 2493, 0, 2699, 1038, 1605, 2300, 2099,
             2571, 403, 1858, 1584, 949, 1765, 678, 2699, 0, 1744, 1645, 653, 600,
             875, 1589, 262, 466, 796, 547, 1724, 1038, 1744, 0, 679, 1272, 1162,
             1420, 1374, 940, 1056, 879, 225, 1891, 1605, 1645, 679, 0, 1017, 1200,
             2145, 357, 1453, 1280, 586, 887, 1114, 2300, 653, 1272, 1017, 0, 504,
             1972, 579, 1260, 987, 371, 999, 701, 2099, 600, 1162, 1200, 504, 0,
        };
        static readonly int[,] data32MD = {
            { 0, 2451, 713, 1018, 1631, 1374, 2408, 213, 2571, 875, 1420, 2145, 1972 },
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
            { 1972, 579, 1260, 987, 371, 999, 701, 2099, 600, 1162, 1200, 504, 0 },
        };        
        static readonly long[] data64SD = {
             0, 2451, 713, 1018, 1631, 1374, 2408, 213, 2571, 875, 1420, 2145, 1972,
             2451, 0, 1745, 1524, 831, 1240, 959, 2596, 403, 1589, 1374, 357, 579,
             713, 1745, 0, 355, 920, 803, 1737, 851, 1858, 262, 940, 1453, 1260,
             1018, 1524, 355, 0, 700, 862, 1395, 1123, 1584, 466, 1056, 1280, 987,
             1631, 831, 920, 700, 0, 663, 1021, 1769, 949, 796, 879, 586, 371,
             1374, 1240, 803, 862, 663, 0, 1681, 1551, 1765, 547, 225, 887, 999,
             2408, 959, 1737, 1395, 1021, 1681, 0, 2493, 678, 1724, 1891, 1114, 701,
             213, 2596, 851, 1123, 1769, 1551, 2493, 0, 2699, 1038, 1605, 2300, 2099,
             2571, 403, 1858, 1584, 949, 1765, 678, 2699, 0, 1744, 1645, 653, 600,
             875, 1589, 262, 466, 796, 547, 1724, 1038, 1744, 0, 679, 1272, 1162,
             1420, 1374, 940, 1056, 879, 225, 1891, 1605, 1645, 679, 0, 1017, 1200,
             2145, 357, 1453, 1280, 586, 887, 1114, 2300, 653, 1272, 1017, 0, 504,
             1972, 579, 1260, 987, 371, 999, 701, 2099, 600, 1162, 1200, 504, 0,
        };
        static readonly long[,] data64MD = {
            { 0, 2451, 713, 1018, 1631, 1374, 2408, 213, 2571, 875, 1420, 2145, 1972 },
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
            { 1972, 579, 1260, 987, 371, 999, 701, 2099, 600, 1162, 1200, 504, 0 },
        };

        [Benchmark(Baseline = true), BenchmarkCategory("Int32")]
        public void Int32_01_Original() => new Int32_01_Original.TspExact().GetOptimusRoute(data32MD);
        //[Benchmark, BenchmarkCategory("Int32")]
        //public void Int32_02_TimingBug() => new Int32_02_TimingBug.TspExact().GetOptimusRoute(data32MD);
        [Benchmark, BenchmarkCategory("Int32")]
        public void Int32_03_MD2SD_Array() => new Int32_03_MD2SD_Array.TspExact().GetOptimusRoute(data32SD);
        [Benchmark, BenchmarkCategory("Int32")]
        public void Int32_04_LocalCopyArray() => new Int32_04_LocalCopyArray.TspExact().GetOptimusRoute(data32SD);
        [Benchmark, BenchmarkCategory("Int32")]
        public void Int32_05_UnsafeArrays() => new Int32_05_UnsafeArrays.TspExact().GetOptimusRoute(data32SD);
        [Benchmark, BenchmarkCategory("Int32")]
        public void Int32_06_NoBoxing() => new Int32_06_NoBoxing.TspExact().GetOptimusRoute(data32SD);
        [Benchmark, BenchmarkCategory("Int32")]
        public void Int32_07_RouteArray() => new Int32_07_RouteArray.TspExact().GetOptimusRoute(data32SD);
        //[Benchmark, BenchmarkCategory("Int32")]
        //public void Int32_08_() => new Int32_08_.TspExact().GetOptimusRoute(data32SD);

        [Benchmark(Baseline = true), BenchmarkCategory("Int64")]
        public void Int64_01_Original() => new Int64_01_Original.TspExact().GetOptimusRoute(data64MD);
        [Benchmark, BenchmarkCategory("Int64")]
        public void Int64_02_TimingBug() => new Int64_02_TimingBug.TspExact().GetOptimusRoute(data64MD);
        [Benchmark, BenchmarkCategory("Int64")]
        public void Int64_03_MD2SD_Array() => new Int64_03_MD2SD_Array.TspExact().GetOptimusRoute(data64SD);
        [Benchmark, BenchmarkCategory("Int64")]
        public void Int64_04_LocalCopyArray() => new Int64_04_LocalCopyArray.TspExact().GetOptimusRoute(data64SD);
        [Benchmark, BenchmarkCategory("Int64")]
        public void Int64_05_UnsafeArrays() => new Int64_05_UnsafeArrays.TspExact().GetOptimusRoute(data64SD);
        [Benchmark, BenchmarkCategory("Int64")]
        public void Int64_06_NoBoxing() => new Int64_06_NoBoxing.TspExact().GetOptimusRoute(data64SD);
        [Benchmark, BenchmarkCategory("Int64")]
        public void Int64_07_RouteArray() => new Int64_07_RouteArray.TspExact().GetOptimusRoute(data64SD);
        /////[Benchmark, BenchmarkCategory("Int64")]
        //public void Int64_08_() => new Int64_08_.TspExact().GetOptimusRoute(data64SD);

    }
}
