namespace Gostopolis.Services.Properties;

using Data.Interfaces;

public class PropertyService : IPropertyService
{
    public List<string> GetAccommodatingCombinations(IEnumerable<IIterable> objects, int people, int objCount)
    {
        if (!objects.Any())
        {
            return new List<string>();
        }

        var maxCapacity = objects.Max(o => o.Capacity);

        int[,] tuples = FindNTuplesWithSum(people, objCount, maxCapacity);

        return OrderCombinations(tuples, maxCapacity);
    }

    static int[,] FindNTuplesWithSum(int targetSum, int n, int maxValue)
    {
        int[,] tuples = new int[CountNTuplesWithSum(targetSum, n, maxValue), n];
        int index = 0;
        FindNTuplesWithSumHelper(targetSum, n, new int[n], 1, maxValue, tuples, ref index);
        return tuples;
    }

    static void FindNTuplesWithSumHelper(int targetSum, int n, int[] currentTuple, int currentIndex, int maxValue, int[,] tuples, ref int index)
    {
        if (n == 0)
        {
            if (currentTuple.Sum() == targetSum)
            {
                for (int i = 0; i < currentTuple.Length; i++)
                {
                    tuples[index, i] = currentTuple[i];
                }
                index++;
            }
            return;
        }

        for (int i = currentIndex; i <= maxValue; i++)
        {
            currentTuple[currentTuple.Length - n] = i;
            FindNTuplesWithSumHelper(targetSum, n - 1, currentTuple, i, maxValue, tuples, ref index);
        }
    }

    static int CountNTuplesWithSum(int targetSum, int n, int maxValue)
    {
        int count = 0;
        CountNTuplesWithSumHelper(targetSum, n, 1, maxValue, ref count);
        return count;
    }

    static void CountNTuplesWithSumHelper(int targetSum, int n, int currentIndex, int maxValue, ref int count)
    {
        if (n == 0)
        {
            if (targetSum == 0)
            {
                count++;
            }
            return;
        }

        for (int i = currentIndex; i <= maxValue; i++)
        {
            CountNTuplesWithSumHelper(targetSum - i, n - 1, i, maxValue, ref count);
        }
    }

    static List<string> OrderCombinations(int[,] matrix, int maxValue)
    {
        var combinations = new List<List<int>>();
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            var row = new List<int>();
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                row.Add(matrix[i, j]);
            }
            for (int k = 0; k < row.Count(); k++)
            {
                for (int l = row[k]; l <= maxValue; l++)
                {
                    row[k] = l;
                    combinations.Add(row.OrderBy(x => x).ToList());
                }
            }
        }
        var hashSet = new HashSet<string>();
        foreach (var combination in combinations)
        {
            hashSet.Add(string.Join(" ", combination));
        }

        return hashSet.OrderBy(combination => combination[0]).ToList();
    }
}
