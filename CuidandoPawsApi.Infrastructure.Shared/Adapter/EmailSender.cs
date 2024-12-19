using CuidandoPawsApi.Application.DTOs.Email;
using CuidandoPawsApi.Domain.Ports.Email;
using CuidandoPawsApi.Domain.Settings;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Infrastructure.Shared.Adapter
{
    public class EmailSender : IEmailService<EmailRequestDTos>
    {
        private MailSettings _mailSettings { get; } 

        public EmailSender(IOptions<MailSettings> options)
        {
            _mailSettings = options.Value;
        }

        public async Task Execute(EmailRequestDTos dto)
        {
            MimeMessage email = new ();

            email.Sender = MailboxAddress.Parse(_mailSettings.EmailFrom);
            email.To.Add(MailboxAddress.Parse(dto.To)); //This send email
            email.Subject = dto.Subject;
            
            BodyBuilder bodyBuilder = new ();
            bodyBuilder.HtmlBody = dto.Body;
            email.Body = bodyBuilder.ToMessageBody();

            //SMTP configuration
            using MailKit.Net.Smtp.SmtpClient smtp = new();
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
            smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort,SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.SmtpUser,_mailSettings.SmtpPass);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
