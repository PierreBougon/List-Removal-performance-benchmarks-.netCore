using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace PerfBench
{
    static class ListHelper
    {
        public static void RemoveAtUnordered<T>(this List<T> list, int index)
        {
            list[index] = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
        }
    }

    class MyObject
    {
        public int Value { get; set; }
    }

    class Program
    {

        static int inverseRemoveFrequency = 2;

        private static List<MyObject> GetList(int size, Random rand)
        {
            List<MyObject> myList = new List<MyObject>();
            for (int i = 0; i < size; i++)
                myList.Add(new MyObject { Value = rand.Next() });
            return myList;
        }



        static void RemoveOnCompare(List<MyObject> list)
        {
            for (int i = list.Count - 1; i >= 0; --i)
            {
                var item = list[i];
                if (item.Value % inverseRemoveFrequency == 0)
                    list.RemoveAtUnordered(i);
            }
        }

        static void RemoveAllOnCompare(List<MyObject> list)
        {
            list.RemoveAll(i => i.Value % inverseRemoveFrequency == 0);
        }


        static void RemoveOnCompareWithRemoveAt(List<MyObject> list)
        {
            for (int i = list.Count - 1; i >= 0; --i)
            {
                var item = list[i];
                if (item.Value % inverseRemoveFrequency == 0)
                    list.RemoveAt(i);
            }
        }

        static List<MyObject> ReturnFilteredList(List<MyObject> list)
        {
            return list.Where(i => i.Value % inverseRemoveFrequency == 0).ToList();
        }

        static void RunBench(int size, Stopwatch watch, Random rand)
        {
            var it = 1000;

            var list = GetList(size, rand);
            var list1 = new List<MyObject>(list);
            var list2 = new List<MyObject>(list);
            var list3 = new List<MyObject>(list);

            Console.WriteLine("Recreate correct list");

            watch.Reset();
            watch.Start();
            for (int i = 0; i < it; ++i)
            {
                _ = ReturnFilteredList(list);
            }
            watch.Stop();

            Console.WriteLine((watch.Elapsed.TotalMilliseconds / it).ToString("##.################"));




            Console.WriteLine("Remove unordered");

            watch.Reset();
        
            for (int i = 0; i < it; ++i)
            {
                watch.Stop();
                var listx = new List<MyObject>(list1);
                watch.Start();
                RemoveOnCompare(listx);
            }
            watch.Stop();

            Console.WriteLine((watch.Elapsed.TotalMilliseconds / it).ToString("##.################"));


            Console.WriteLine("Remove All native");

            watch.Reset();
            watch.Start();

            for (int i = 0; i < it; ++i)
            {
                RemoveAllOnCompare(list3);
            }
            watch.Stop();

            Console.WriteLine((watch.Elapsed.TotalMilliseconds / it).ToString("##.################"));



            if (size < 50000)
            {
                Console.WriteLine("Remove At native");
            
                watch.Reset();
                watch.Start();
                for (int i = 0; i < it; ++i)
                {
                    watch.Stop();
                    var listx = new List<MyObject>(list2);
                    watch.Start();
                    RemoveOnCompareWithRemoveAt(listx);
                }
                watch.Stop();
            
                Console.WriteLine((watch.Elapsed.TotalMilliseconds / it).ToString("##.################"));
            }
        }

        static void Main(string[] args)
        {
            var rand = new Random((int)(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() % int.MaxValue));
            Stopwatch watch = new Stopwatch();

            Thread.Sleep(500);

            // JIT pre warmup
            Console.WriteLine("");
            Console.WriteLine("Jit Pre WarmUp");
            RunBench(5, watch, rand);



            
            Console.WriteLine("");
            Console.WriteLine("Medium size+");
            RunBench(100000, watch, rand);

            Console.WriteLine("");
            Console.WriteLine("Small size");
            RunBench(100, watch, rand);

            Console.WriteLine("");
            Console.WriteLine("Small size +");
            RunBench(1000, watch, rand);

            Console.WriteLine("");
            Console.WriteLine("Medium size");
            RunBench(10000, watch, rand);

            Console.WriteLine("");
            Console.WriteLine("Large size");
            RunBench(1000000, watch, rand);

            //Console.WriteLine("");
            //Console.WriteLine("Large size+");
            //RunBench(10000000, watch, rand);
        }
    }
}
