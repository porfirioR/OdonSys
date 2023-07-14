namespace Utilities.Extensions
{
    public static class TimeExtensions
    {
        public static long ToUnixTimestamps(this DateTime dateTime)
        {
            return (long)(dateTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
