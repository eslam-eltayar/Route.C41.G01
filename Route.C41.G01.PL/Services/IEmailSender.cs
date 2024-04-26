using System.Threading.Tasks;

namespace Route.C41.G01.PL.Services
{
    public interface IEmailSender
    {
        Task SendAsync(string from, string to, string Subject, string body);
    }
}
