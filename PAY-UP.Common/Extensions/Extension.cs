namespace PAY_UP.Common.Extensions
{
    public static class Extension
    {
        public static bool IsNull<T>(this T obj)
        {
            return obj == null;
        }
    }
}
