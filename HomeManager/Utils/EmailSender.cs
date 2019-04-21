using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace HomeManager.Utils
{
    public class EmailSender
    {
        public bool SendEmailAsync(string receiver, string verificationUrl)
        {
            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential basicCredential = new NetworkCredential("registration@homemanager.hostingasp.pl", "Dupa1!");

            using (MailMessage message = new MailMessage())
            {
                MailAddress fromAddress = new MailAddress("registration@homemanager.hostingasp.pl");

                smtpClient.Host = "smtp.webio.pl";
                smtpClient.Port = 587;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = basicCredential;

                message.From = fromAddress;
                message.Subject = "Confirm your email.";
                message.IsBodyHtml = true;
                message.Body = verificationUrl;
                message.To.Add(receiver);

                try
                {
                    smtpClient.Send(message);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
