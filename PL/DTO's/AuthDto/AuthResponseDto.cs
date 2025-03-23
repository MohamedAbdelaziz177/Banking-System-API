namespace Banking_system.DTO_s.AuthDto
{
    public class AuthResponseDto
    {
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }

        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }

        public string RefreshToken { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
