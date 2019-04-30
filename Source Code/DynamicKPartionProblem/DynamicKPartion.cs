using System;
using System.Collections.Generic;
using System.Linq;

class DynamicKPartion
{
    // A simple struct that will work as an return object.
    public struct OptimalGroup
    {
        public List<List<int>> TheOptimalGroup;
        public List<int> SplitIndex;
        public int TotalCost;
        public int ProcessTime_Ticks;
        public int ProcessTime_Ms;

        public OptimalGroup(List<List<int>> _TheOptimalGroup, List<int> _SplitIndex, int _TotalCost, int _ProcessTime_Ticks, int _ProcessTime_Ms)
        {
            TheOptimalGroup = _TheOptimalGroup;
            SplitIndex = _SplitIndex;
            TotalCost = _TotalCost;
            ProcessTime_Ticks = _ProcessTime_Ticks;
            ProcessTime_Ms = _ProcessTime_Ms;
        }
    }

    // public call to get the optimal group
    public static OptimalGroup GetOptimalGrouping(int[] Values, int k)
    {
        // Saves the original title
        string PreTitle = Console.Title;

        // Just a stopwatch to check how long it takes.
        var Watch = new System.Diagnostics.Stopwatch();

        Watch.Start();

        List<List<int>> NewList = new List<List<int>>();

        // Gets the mask of the best group, eg the split indexes
        int[] Masked = Mask(Values, k);
        int Index = 0;

        // split that mask so its displayed propperly 
        for (int i = 0; i < k; i++)
        {
            List<int> InnerList = new List<int>();
            for (int j = 0; j < Masked[i]; j++)
            {
                InnerList.Add(Values[Index]);
                Index++;
            }
            NewList.Add(InnerList);
        }

        Watch.Stop();

        // Sets the title to the original title
        Console.Title = PreTitle;

        return new OptimalGroup(NewList, Masked.ToList<int>(), TotalGroupCost(NewList, Values.Length), (int)Watch.ElapsedTicks, (int)Watch.ElapsedMilliseconds);
    }

    private static int[] Mask(int[] Values, int k)
    {
        double min_cost = 99999999.0, cost = 0.0;

        // Sets the first group indexes, fx input [ 1,2,3,4 ] would be split into [1] [2] [3,4]
        int[] group = FirstGroup(k, Values.Length);
        int[] best_group = new int[k];

        // Make a caching array to make processing faster (a dynamic programing method)
        double[,] SaveVale = new double[Values.Length, Values.Length - 2];
        for (int i = 0; i < SaveVale.GetLength(0); i++)
            for (int j = 0; j < SaveVale.GetLength(1); j++)
                SaveVale[i,j] = int.MaxValue;

        // Then we run through all possible groups, until there are no more groups
        while (group != null)
        {
            // get the cost of this group setup
            cost = Cost(Values, group, SaveVale);
            if (cost < min_cost)
            {
                // if the cost is less than the previous one, save this one
                min_cost = cost;
                group.CopyTo(best_group, 0);
            }

            // Continue to next group
            group = NextGroup(group, Values.Length, group.Length - 2);
        }

        // return the best group
        return best_group;
    }

    public static int[] FirstGroup(int k, int n)
    {
        // Simply move the input out so the split indexes would end up being [1] [1] ... [n - k + 1]
        int[] group = new int[k];
        for (int i = 0; i < k - 1; i++)
        {
            group[i] = 1;
        }
        group[k - 1] = n - k + 1;
        return group;
    }

    public static int[] NextGroup(int[] group, int n, int cursor)
    {
        // Check if the group can be split even more:
        if (group[cursor] + InitalSum(group, cursor) + group.Length - cursor <= n)
        {
            int[] new_group = new int[group.Length];
            group.CopyTo(new_group,0);
            new_group[cursor]++;

            // Move indexes into the new_group from the current cursor position to the end of the group
            // fx. the split [1] [3] [1] would give the group [2] [1] [2]
            for (int i = cursor + 1; i < group.Length - 1; i++)
            {
                new_group[i] = 1;
            }
            new_group[group.Length - 1] = n - InitalSum(new_group, group.Length - 1);

            // Return the new_group:
            return new_group;
        }
        else if (cursor > 0)
        {
            // Here we dont just move one number from the current group back.
            // fx. the split [1] [1] [3] becomed [1] [2] [2]
            return NextGroup(group, n, cursor - 1);
        }
        else return null;
        // If non of that is possible, return null, meaning there is no more group option.
        // fx the last group of [1] [1] [3] would be [3] [1] [1]
    }

    public static int InitalSum(int[] group, int cursor)
    {
        // Gets the sum of a index group 
        int sum = 0;
        for (int j = 0; j < cursor; j++)
        {
            sum += group[j];
        }
        return sum;

    }

    public static double Cost(int[] A, int[] group, double[,] SaveVal)
    {
        // Gets the cost of a group setup

        double cost = 0.0;
        int firstelement = 0;

        // We run through this for every group
        for (int i = 0; i < group.Length; i++)
        {
            // then add the cost, as well as increase the first element, this makes so that the group
            // [4, 5, 7] [11] [21]
            // after the first rundown, then only add the cost from
            // [11] [21]

            // This is a dynamic programing method:
            // Check if the current cost have been processed before, eg if we have calculated the subgroup [4, 5, 7]
            // then there are no reason to recalculate it for every time a new group is made.
            // Eg the diference between the configuration [4, 5] [7, 11] [21]
            // and [4, 5] [7] [11, 21]
            // the first group only need to be calculated once, but if it have not been calculated before, then we should calculate it:
            if (SaveVal[firstelement, group[i] - 1] == int.MaxValue)
                cost += GroupCost(A, firstelement, group[i], SaveVal);
            else
                cost += SaveVal[firstelement, group[i] - 1];
            firstelement += group[i];
        }
        return cost;
    }

    public static double GroupCost(int[] A, int firstelement, int nbelements, double[,] SaveVal)
    {
        // Quickly calculate the cost of this group
        double cost = int.MaxValue;
        double avg = 0, sum = 0.0;
        for (int i = 0; i < nbelements; i++)
        {
            if (firstelement + i < A.Length)
            {
                sum += A[firstelement + i];
            }
        }
        avg = sum / nbelements;

        cost = 0;
        for (int i = 0; i < nbelements; i++)
        {
            if (firstelement + i < A.Length)
            {
                cost += Math.Pow(A[i + firstelement] - avg, 2);
            }
        }

        // Save that cost 
        SaveVal[firstelement, nbelements - 1] = cost;

        return cost;
    }

    public static void PrintOptimalGrouping(OptimalGroup Group)
    {
        //Just a structured way of displaying the best group:

        Console.Write("Best Group: ");
        foreach (List<int> InnerList in Group.TheOptimalGroup)
        {
            Console.Write(" [ ");
            foreach (int InnerInnerList in InnerList)
            {
                Console.Write(InnerInnerList + ", ");
            }
            Console.Write(" ] ");
        }

        Console.WriteLine("");

        Console.Write("Index:      ");
        foreach (int SplitIndex in Group.SplitIndex)
        {
            Console.Write(" [ " + SplitIndex + " ]");
        }
        Console.WriteLine("");

        Console.WriteLine("Cost: " + Group.TotalCost + " Elapsed time (ms): " + Group.ProcessTime_Ms + " Elapsed time (ticks): " + Group.ProcessTime_Ticks);

        Console.WriteLine("");
    }

    private static int TotalGroupCost(List<List<int>> Group, int n)
    {
        // Quickly calculates the total cost of a already made list:
        double Result = 0;
        foreach (List<int> InnerList in Group)
        {
            double[,] SaveVale = new double[n, n];
            for (int i = 0; i < SaveVale.GetLength(0); i++)
                for (int j = 0; j < SaveVale.GetLength(1); j++)
                    SaveVale[i, j] = int.MaxValue;

            Result += GroupCost(InnerList.ToArray(), 0, InnerList.Count, SaveVale);
        }
        return (int)Result;
    }
}
