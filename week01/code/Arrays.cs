public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // Plan:
        // 1. Create a new array of doubles with the specified length.
        // 2. Loop from index 0 up to length -1.
        //    a. For each index i, compute the i-th multiple as (i + 1) * number.
        //    b. Store the computed multiple in the array at index i.
        // 3. After filling the array, return it.

        // Step 1: initialize the result array.
        double[] result = new double[length];

        // Step 2: fill each position with the appropriate multiple.
        for (int i = 0; i < length; i++)
        {
            // {i + 1} gives the 1-based multiple count.
            result[i] = number * (i + 1);
        }

        // Step 3: Return the populated array.
        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // Plan:
        // 1. Determine the effective rotation by taking amount modulo data.Count.
        // 2. If effective rotation is 0, no change needed.
        // 3. Otherwise, perform rotation:
        //    a. Copy the last 'amount' elements into a temporary List.
        //    b. Shift the remaining elements right by amount positions.
        //    c. Copy the temporary elements into the start of the List.

        // Step 1: Determine the effective rotation.
        int count = data.Count;
        int k = amount % count; // handles cases where amount == count

        // Step 2: if k is 0, no rotation needed.
        if (k == 0) return;

        // Step 3a: Copy the original list.
        List<int> original = new List<int>(data);

        // Step 3b: place each element at its new rotated index.
        for (int i = 0; i < count; i++)
        {
            // (i + k) modulo count gives the new position.
            data[(i + k) % count] = original[i];
        }

    }
}
