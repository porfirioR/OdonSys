namespace Utilities
{
    public static class Claims
    {
        private static string _userId = "userId";
        private static string _userName = "userName";
        private static string _userRoles = "userRoles";

        public static string UserId { get => _userId; set => _userId = value; }
        public static string UserName { get => _userName; set => _userName = value; }
        public static string UserRoles { get => _userRoles; set => _userRoles = value; }
    }
}
