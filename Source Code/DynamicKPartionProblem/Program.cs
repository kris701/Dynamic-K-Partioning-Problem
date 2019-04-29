using System;
using System.Collections.Generic;

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

            DynamicKPartion.PrintOptimalGrouping(DynamicKPartion.GetOptimalGrouping(new int[] { 2, 2, 3, 5, 6, 6, 7, 9, 9, 10, 11, 24, 24, 24, 25, 28, 31, 33, 40, 45, 46 }, 7));

            DynamicKPartion.PrintOptimalGrouping(DynamicKPartion.GetOptimalGrouping(new int[] { 1,1,1,2,2,5,8,8,9,9,11,21,22 }, 3));

            DynamicKPartion.PrintOptimalGrouping(DynamicKPartion.GetOptimalGrouping(new int[] { 10, 11, 12, 13, 100, 120, 130, 145, 178, 189, 200, 201, 225, 229, 300, 1000, 1001, 1008 }, 7));

            DynamicKPartion.PrintOptimalGrouping(DynamicKPartion.GetOptimalGrouping(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1,1, 8, 8, 8 ,8 , 8, 8, 9, 20, 20, 20, 20, 20,21,21,21,21,23,70,70,70,71,71,71,71,71,71,100,200,1000,2000 }, 10));
            
            DynamicKPartion.PrintOptimalGrouping(DynamicKPartion.GetOptimalGrouping(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 201, 202, 203, 204, 205, 206, 207, 208, 209, 210 }, 12));

            Console.ReadLine();
        }
    }
}