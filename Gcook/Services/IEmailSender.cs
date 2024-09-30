namespace GCook.Services;

public interface IEmailSender 
{
    Task SendEmailASync(string email, string subject, string htmlMessage);
}