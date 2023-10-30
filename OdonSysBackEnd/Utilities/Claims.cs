namespace Utilities
{
    public static class Claims
    {
        private static string _userId = "userId";
        private static string _userName = "userName";
        private static string _userRoles = "userRoles";
        private static string _userIdAadB2C = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        private static string _nameAadB2C = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";
        private static string _surnameAadB2C = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";

        public static string UserId { get => _userId; set => _userId = value; }
        public static string UserName { get => _userName; set => _userName = value; }
        public static string NameAadB2C { get => _nameAadB2C; set => _nameAadB2C = value; }
        public static string SurnameAadB2C { get => _surnameAadB2C; set => _surnameAadB2C = value; }
        public static string UserRoles { get => _userRoles; set => _userRoles = value; }
        public static string UserIdAadB2C { get => _userIdAadB2C; set => _userIdAadB2C = value; }
    }
}
