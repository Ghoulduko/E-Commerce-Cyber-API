namespace Cyber.Core.Interfaces;

public interface IEmailService
{
    public void SendEmail(string email, string code);
}