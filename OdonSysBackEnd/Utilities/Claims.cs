namespace Utilities
{
    public static class Claims
    {
        private static string _userId = "userId";
        private static string _userName = "userName";
        private static string _userRoles = "userRoles";
        private static string _userIdAadB2C = "oid";

        public static string UserId { get => _userId; set => _userId = value; }
        public static string UserName { get => _userName; set => _userName = value; }
        public static string UserRoles { get => _userRoles; set => _userRoles = value; }
        public static string UserIdAadB2C { get => _userIdAadB2C; set => _userIdAadB2C = value; }
    }
}
