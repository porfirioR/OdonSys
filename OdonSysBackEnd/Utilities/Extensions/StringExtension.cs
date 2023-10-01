namespace Utilities.Extensions
{
    public static class StringExtension
    {
        public static string RemoveDash(this string value)
        {
            return value.Replace("-", "", StringComparison.OrdinalIgnoreCase);
        }
    }
}
