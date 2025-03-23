namespace Banking_system.Services.AuthService_d
{
    public interface IMailService
    {
        Task<bool> SendMailAsync(string To, string Subject, string Body);
    }
}
