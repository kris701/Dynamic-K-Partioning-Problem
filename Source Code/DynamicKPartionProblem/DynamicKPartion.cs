using System;
using System.Collections.Generic;

class DynamicKPartion
{
    // Public Section

    public static int GetBestValueOf(int[] Values, int k)
    {
        return TotalCost(ReconstructPartition(Values, Partition(Values, k), k));
    }
    public static int GetBestValueOf(List<List<int>> List)
    {
        return TotalCost(List);
    }

    public static List<List<int>> GetOptimalGrouping(int[] Values, int k)
    {
        return ReconstructPartition(Values, Partition(Values, k), k);
    }

    public static void PrintOptimalGrouping(List<List<int>> Group)
    {
        foreach (List<int> InnerList in Group)
        {
            Console.Write(" [ ");
            foreach (int InnerInnerList in InnerList)
            {
                Console.Write(InnerInnerList + ", ");
            }
            Console.Write(" ] ");
        }
        Console.WriteLine(" Cost: " + TotalCost(Group));
    }

    // Private Section

    private static int[,] Partition(int[] Values, int k)
    {
        // Initializes the initial map arrays:
        int n = Values.Length;
        int[,] M = new int[n, k];
        int[,] D = new int[n - 1, k - 1];

        // Puts in the base values in the map array
        M[0, 0] = Values[0];
        for (int i = 1; i < n; i++)
        {
            M[i, 0] = Values[i] + M[i - 1, 0];
        }
        for (int i = 1; i < k; i++)
        {
            M[0, i] = Values[0];
        }

        // Run throught every number compared to a group:
        for (int i = 1; i < n; i++)
        {
            for (int j = 1; j < k; j++)
            {
                /// Resets currentmin and minimumx
                int CurrentMin = -1;
                int MinimumX = int.MaxValue;

                // Run through every number that are under the current i index
                for (int x = 0; x < i; x++)
                {
                    // Get the maximum value of this set:
                    int s = Math.Max(M[x, j - 1], M[i, 0] - M[x, 0]);

                    // if s is less than the last currentmin, then save the s and x as the new currentmin and minimumx
                    if (CurrentMin < 0 || s < CurrentMin)
                    {
                        CurrentMin = s;
                        MinimumX = x;
                    }
                }

                // Save the current min and minimumx in the M and D arrays
                M[i, j] = CurrentMin;
                D[i - 1, j - 1] = MinimumX;
            }
        }

        // Returns the new partion mask
        return D;
    }

    private static List<List<int>> ReconstructPartition(int[] Values, int[,] D, int k)
    {
        List<List<int>> Result = new List<List<int>>();
        int n = D.GetLength(0);
        k = k - 2;

        // Runs through the input values, from top to bottom, until k >= 0.
        while (k >= 0)
        {
            List<int> Inner1 = new List<int>();
            for (int i = D[n - 1, k] + 1; i < n + 1; i++)
            {
                Inner1.Add(Values[i]);
            }
            Result.Add(Inner1);
            n = D[n - 1, k];
            k--;
        }

        // When k < 0 then it must be the remaining group thats lest
        List<int> Inner2 = new List<int>();
        for (int i = 0; i < n + 1; i++)
        {
            Inner2.Add(Values[i]);
        }
        Result.Add(Inner2);

        // Revert the list, since its currently showing the largest one as the first
        Result.Reverse();
        return Result;
    }

    private static int TotalCost(List<List<int>> InputList)
    {
        // Quickly calculate the cost of a list

        int TotalCost = 0;

        double IndividualCost = 0;
        double IndividualAvr = 0;
        for (int i = 0; i < InputList.Count; i++)
        {
            IndividualAvr = 0;
            IndividualCost = 0;
            for (int j = 0; j < InputList[i].Count; j++)
            {
                IndividualAvr += InputList[i][j];
            }
            IndividualAvr = IndividualAvr / InputList[i].Count;

            for (int j = 0; j < InputList[i].Count; j++)
            {
                IndividualCost += Math.Pow(InputList[i][j] - IndividualAvr, 2);
            }

            TotalCost += (int)IndividualCost;
        }

        return TotalCost;
    }
}
