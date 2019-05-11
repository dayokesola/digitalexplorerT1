using System;
using System.Threading.Tasks;
namespace BizNest.Core.Logic.Definations
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string to,string from, string subject, string body);
    }
}
