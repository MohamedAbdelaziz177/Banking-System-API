namespace Banking_system.DTO_s.AuthDto
{
    public class TokenResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public bool Successed { get; set; } = true;
    }
}
