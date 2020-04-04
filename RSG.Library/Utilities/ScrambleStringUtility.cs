using System;

namespace RSG.Library.Utilities
{
    public static class ScrambleStringUtility
    {
        public static void KnuthShuffle<T>(T[] array, Random random)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int j = random.Next(i, array.Length); // Don't select from the entire array on subsequent loops
                T temp = array[i]; array[i] = array[j]; array[j] = temp;
            }
        }
    }
}
