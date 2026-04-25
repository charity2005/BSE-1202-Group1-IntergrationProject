namespace SACCO_RMS
{
    public static class Session
    {
        public static string Username { get; set; } = string.Empty;
        public static string Role     { get; set; } = string.Empty;
        public static bool IsAdmin    => Role == "Admin";
        public static void Clear()    { Username = string.Empty; Role = string.Empty; }
    }
}
