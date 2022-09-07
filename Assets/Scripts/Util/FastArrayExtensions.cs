namespace Util
{
    public static class FastArrayExtensions
    {
        public static int IndexOf<T>(this T[] arr, T t)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Equals(t))
                    return i;
            }
            return -1;
        }
    }
}