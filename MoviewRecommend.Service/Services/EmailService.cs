using MoviewRecommend.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MoviewRecommend.Service.Services
{
    public class EmailService : IEmailService
    {
        public bool SendMail(string email = "")
        {
            try
            {
                SmtpClient client = new SmtpClient("mysmtpserver");
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("username", "password");

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("whoever@me.com");
                mailMessage.To.Add(email);
                mailMessage.Body = "Film tavsiyesi";
                mailMessage.Subject = "Film";
                client.Send(mailMessage);

                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
