namespace Utilities
{
    public static class Claims
    {
        private static string userId = "userId";
        private static string userName = "userName";
        private static string roles = "roles";

        public static string UserId { get => userId; set => userId = value; }
        public static string UserName { get => userName; set => userName = value; }
        public static string Roles { get => roles; set => roles = value; }
    }
}
