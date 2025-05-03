using System.Net.Mail;
using System.Net;

namespace InventorySystem.Service
{
    public interface IEmailService
    {

        void SendEmail(string to, string subject, string body);
    }
}
