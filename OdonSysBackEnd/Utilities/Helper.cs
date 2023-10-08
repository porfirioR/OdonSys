namespace Utilities
{
    public class Helper
    {
        public static string GetUsername(string name, string surname)
        {
            return !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(surname) ?
                $"{name[..1].ToUpper()}{surname[..1].ToUpper()}{surname[1..]}" :
                null;
        }
    }
}
