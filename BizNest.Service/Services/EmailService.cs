using BizNest.Core.Logic.Definations;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace BizNest.Service.Services
{
    public class EmailService : IEmailService
    {
        private IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> SendEmailAsync(string to, string from, string subject, string body)
        {
            var smtpEmail = _configuration["SmtpEmail"];
            var smtpPassword = _configuration["SmtpPassword"];
            var smtpServerAddress = _configuration["SmtpServerAddress"];

            try
            {
                MailMessage mailMsg = new MailMessage();
                mailMsg.To.Add(new MailAddress(to));
                mailMsg.From = new MailAddress(from, "BizNest Service");

                // Subject
                mailMsg.Subject = subject;
                // Body content
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Plain));
             
                //DirectMail SMTP address and port
                SmtpClient smtpClient = new SmtpClient(smtpServerAddress, 25);
                // Verify SMTP user name and password
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpEmail, smtpPassword);
                smtpClient.Credentials = credentials;
                await smtpClient.SendMailAsync(mailMsg);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }
    }
}
