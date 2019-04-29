using System;
using System.Collections.Generic;
using System.Linq;

class DynamicKPartion
{
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

    public static OptimalGroup GetOptimalGrouping(int[] Values, int k)
    {
        var Watch = new System.Diagnostics.Stopwatch();

        Watch.Start();

        List<List<int>> NewList = new List<List<int>>();
        int[] Masked = Mask(Values, k);
        int Index = 0;
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

        return new OptimalGroup(NewList, Masked.ToList<int>(), TotalGroupCost(NewList, Values.Length), (int)Watch.ElapsedTicks, (int)Watch.ElapsedMilliseconds);
    }

    private static int[] Mask(int[] Values, int k)
    {
        double min_cost = 99999999.0, cost = 0.0;
        int[] group = FirstGroup(k, Values.Length);
        int[] best_group = new int[k];
        double[,] SaveVale = new double[Values.Length, Values.Length];
        for (int i = 0; i < SaveVale.GetLength(0); i++)
            for (int j = 0; j < SaveVale.GetLength(1); j++)
                SaveVale[i,j] = int.MaxValue;

        while (group != null)
        {
            cost = Cost(Values, group, SaveVale);
            if (cost < min_cost)
            {
                min_cost = cost;
                group.CopyTo(best_group, 0);
            }
            group = NextGroup(group, Values.Length, group.Length - 2);
        }

        return best_group;
    }

    public static int[] FirstGroup(int k, int n)
    {
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
        if (group[cursor] + InitalSum(group, cursor) + group.Length - cursor <= n)
        {
            int[] new_group = new int[group.Length];
            group.CopyTo(new_group,0);
            new_group[cursor]++;
            for (int i = cursor + 1; i < group.Length - 1; i++)
            {
                new_group[i] = 1;
            }
            new_group[group.Length - 1] = n - InitalSum(new_group, group.Length - 1);
            return new_group;
        }
        else if (cursor > 0)
        {
            return NextGroup(group, n, cursor - 1);
        }
        else return null;
    }

    public static int InitalSum(int[] group, int cursor)
    {
        int sum = 0;
        for (int j = 0; j < cursor; j++)
        {
            sum += group[j];
        }
        return sum;

    }

    public static double GroupCost(int[] A, int firstelement, int nbelements, double[,] SaveVal)
    {
        if (SaveVal[firstelement, nbelements] == int.MaxValue)
        {
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
            SaveVal[firstelement, nbelements] = cost;
        }

        return SaveVal[firstelement, nbelements];
    }

    public static double Cost(int[] A, int[] group, double[,] SaveVal)
    {
        double cost = 0.0;
        int firstelement = 0;
        for (int i = 0; i < group.Length; i++)
        {
            cost += GroupCost(A, firstelement, group[i], SaveVal);
            firstelement += group[i];
        }
        return cost;
    }

    public static void PrintOptimalGrouping(OptimalGroup Group)
    {
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
