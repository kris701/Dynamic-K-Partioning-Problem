using System;
using System.Collections.Generic;

// Main source
// http://www8.cs.umu.se/kurser/TDBA77/VT06/algorithms/BOOK/BOOK2/NODE45.HTM

namespace DynamicKPartionProblem
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // Sample call to get the best group value:

            DynamicKPartion.PrintOptimalGrouping(DynamicKPartion.GetOptimalGrouping(new int[] { 4, 5, 7, 11, 21 }, 3));
            DynamicKPartion.PrintOptimalGrouping(DynamicKPartion.GetOptimalGrouping(new int[] { 1, 2, 3, 4, 5, 6, 7, 9, 9, 9, 9, 9, 9, 22, 45, 66 }, 5));
            DynamicKPartion.PrintOptimalGrouping(DynamicKPartion.GetOptimalGrouping(new int[] { 1, 2, 2, 3, 6, 8, 8, 9, 10 }, 4));
            DynamicKPartion.PrintOptimalGrouping(DynamicKPartion.GetOptimalGrouping(new int[] {1,1, 2, 2, 3, 3, 8, 9, 11, 12 }, 2));

            Console.ReadLine();
        }
    }
}