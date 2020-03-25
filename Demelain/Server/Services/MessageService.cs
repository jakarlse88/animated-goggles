using System;
using System.IO;
using Demelain.Server.Models.InputModels;
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

        public MessageService(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public void SendMessage(ContactFormInputModel model)
        {
            if (model == null ||
                string.IsNullOrWhiteSpace(model.Name) ||
                string.IsNullOrWhiteSpace(model.Subject) ||
                string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Message))
            {
                throw new ArgumentException("MessageService: the argument or a property thereof was null.");
            }

            var mimeMessage = new MimeMessage();

            mimeMessage.From.Add(new MailboxAddress(
                "Contact Form",
                "noreply@demelain.com"));

            mimeMessage.To.Add(new MailboxAddress(
                "Webmaster",
                _configuration["ContactEmail"]));

            mimeMessage.Subject = model.Subject;

            mimeMessage.Body = new TextPart("html")
            {
                Text = FormatMessageBody(model.Name, model.Email, model.Subject, model.Message)
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

            using (var sourceReader = File.OpenText(templatePath))
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