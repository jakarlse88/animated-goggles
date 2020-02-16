using System;
using System.IO;
using System.Threading.Tasks;
using Demelain.Server.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Demelain.Server.Services
{
    public class MessageService : IMessageService
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MessageService(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public Task<bool> SendMessage(ContactFormDto dto)
        {
            if (dto == null ||
                string.IsNullOrWhiteSpace(dto.Name) ||
                string.IsNullOrWhiteSpace(dto.Subject) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Message))
            {
                throw new ArgumentException("MessageService: the argument or a property thereof was null.");
            }

            var mimeMessage = new MimeMessage();

            mimeMessage.From.Add(new MailboxAddress(
                "Contact Form",
                "noreply@nexus.com"));

            mimeMessage.To.Add(new MailboxAddress(
                "Webmaster",
                _configuration["ContactEmail"]));

            mimeMessage.Subject = dto.Subject;

            mimeMessage.Body = new TextPart("html")
            {
                Text = FormatMessageBody(dto.Name, dto.Email, dto.Subject, dto.Message)
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);

                client.Authenticate(
                    _configuration["ContactEmail"],
                    _configuration["ContactEmailPassword"]);

                client.Send(mimeMessage);

                client.Disconnect(true);
            }

            return Task.FromResult(true);
        }

        private string FormatMessageBody(
            string senderName,
            string senderEmail,
            string subject,
            string message)
        {
            var templatePath =
                _webHostEnvironment.ContentRootPath +
                Path.DirectorySeparatorChar +
                "Assets" +
                Path.DirectorySeparatorChar +
                "Templates" +
                Path.DirectorySeparatorChar +
                "ContactFormEmailTemplate.html";

            var builder = new BodyBuilder();

            using (var sourceReader = System.IO.File.OpenText(templatePath))
            {
                builder.HtmlBody = sourceReader.ReadToEnd();
            }

            // 1: sender name
            // 2: sender email
            // 3: sent date
            // 4: subject
            // 5: body
            var messageBody = string.Format(builder.HtmlBody,
                senderName,
                senderEmail,
                $"{DateTime.Now:dddd, d MMM yyyy}",
                subject,
                message);

            return messageBody;
        }
    }
}