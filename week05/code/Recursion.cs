using System.Collections;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it.  Remember to both express the solution 
    /// in terms of recursive call on a smaller problem and 
    /// to identify a base case (terminating case).  If the value of
    /// n <= 0, just return 0.   A loop should not be used.
    /// </summary> 
    public static int SumSquaresRecursive(int n)
    {
        // Base case: If n <= 0, return 0
        if (n <= 0)
            return 0;
 
        // Recursive case: Return n^2 + result of smaller problem
        return n * n + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length
    /// 'size' from a list of 'letters' into the results list.  This function
    /// should assume that each letter is unique (i.e. the 
    /// function does not need to find unique permutations).
    ///
    /// In mathematics, we can calculate the number of permutations
    /// using the formula: len(letters)! / (len(letters) - size)!
    ///
    /// For example, if letters was [A,B,C] and size was 2 then
    /// the following would the contents of the results array after the function ran: AB, AC, BA, BC, CA, CB (might be in 
    /// a different order).
    ///
    /// You can assume that the size specified is always valid (between 1 
    /// and the length of the letters list).
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // If the current permutation has reached the desired size, add it to the results list
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }
 
        // Loop through each letter and generate permutations
        for (int i = 0; i < letters.Length; i++)
        {
            // Remove the i-th letter and recurse with the rest
            string remaining = letters.Remove(i, 1);
            PermutationsChoose(results, remaining, size, word + letters[i]);
        }
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// Imagine that there was a staircase with 's' stairs.  
    /// We want to count how many ways there are to climb 
    /// the stairs.  If the person could only climb one 
    /// stair at a time, then the total would be just one.  
    /// However, if the person could choose to climb either 
    /// one, two, or three stairs at a time (in any order), 
    /// then the total possibilities become much more 
    /// complicated.  
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // Base Cases
        if (s == 0)
            return 1; // One way to do nothing
        if (s < 0)
            return 0; // No way to climb negative steps

        // Initialize memoization dictionary if null
        if (remember == null)
        {
            remember = new Dictionary<int, decimal>();
        }

        // Return previously computed result if available
        if (remember.ContainsKey(s))
        {
            return remember[s];
        }

        // Recursive case: Add ways from taking 1, 2, or 3 steps
        decimal ways = CountWaysToClimb(s - 1, remember) +
                       CountWaysToClimb(s - 2, remember) +
                       CountWaysToClimb(s - 3, remember);

        // Store result in dictionary
        remember[s] = ways;

        return ways;
    }

    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// A binary string is a string consisting of just 1's and 0's.  If we introduce a wildcard symbol *, it becomes a pattern for multiple binary strings.
    /// This function will generate all combinations by replacing * with 0 or 1 recursively.
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        // If no wildcard is found, add the pattern to results
        int index = pattern.IndexOf('*');
        if (index == -1)
        {
            results.Add(pattern);
            return;
        }

        // Replace the wildcard with '0' and recurse
        WildcardBinary(pattern.Substring(0, index) + "0" + pattern.Substring(index + 1), results);

        // Replace the wildcard with '1' and recurse
        WildcardBinary(pattern.Substring(0, index) + "1" + pattern.Substring(index + 1), results);
    }

    /// <summary>
    /// Use recursion to insert all paths that start at (0,0) and end at the
    /// 'end' square into the results list.
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // Initialize the path list on first run
        if (currPath == null)
        {
            currPath = new List<ValueTuple<int, int>>();
        }

        // Check if current position is valid
        if (x < 0 || x >= maze.Width || y < 0 || y >= maze.Height || !maze.IsValidMove(currPath, x, y))
        {
            return; // Out of bounds or invalid move
        }

        // Check if already visited
        if (currPath.Contains((x, y)))
            return;

        // Add current position to path
        currPath.Add((x, y));

        // If we've reached the end, store the path
        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());
            currPath.RemoveAt(currPath.Count - 1);
            return;
        }

        // Recurse in each direction
        SolveMaze(results, maze, x + 1, y, new List<(int, int)>(currPath)); // Right
        SolveMaze(results, maze, x - 1, y, new List<(int, int)>(currPath)); // Left
        SolveMaze(results, maze, x, y + 1, new List<(int, int)>(currPath)); // Down
        SolveMaze(results, maze, x, y - 1, new List<(int, int)>(currPath)); // Up

        // Backtrack step
        currPath.RemoveAt(currPath.Count - 1);
    }
}
