using Biblioteca.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Services
{
    public class EmailService : IEmailService
    {
        public void Enviar(string toEmail, string subject, string body)
        {
            var fromAddress = new MailAddress("nossa.biblioteca.rio2025@gmail.com", "Nossa Biblioteca");
            var toAddress = new MailAddress(toEmail);
            string fromPassword = "tnsj zdtw ofeg eyal"; // A senha de aplicativo gerada no Google

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }
    }
}
