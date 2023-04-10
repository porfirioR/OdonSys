namespace Utilities
{
    public static class Claims
    {
        private static string userId = "userId";
        private static string userName = "userName";
        private static string userRoles = "userRoles";

        public static string UserId { get => userId; set => userId = value; }
        public static string UserName { get => userName; set => userName = value; }
        public static string UserRoles { get => userRoles; set => userRoles = value; }
    }
}
