﻿namespace LoyaltySystem.Api.Services
{
	public class EmailService : IEmailSender
	{
		private readonly MailSettings _mailSettings;
		private readonly ApplicationDbContext _context;
		public EmailService(IOptions<MailSettings> mailSettings, ApplicationDbContext context)
		{
			_context = context;
			_mailSettings = mailSettings.Value;
		}

		public async Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			var message = new MimeMessage
			{
				Sender = MailboxAddress.Parse(_mailSettings.Mail),
				Subject = subject
			};
			message.To.Add(MailboxAddress.Parse(email));
			var builder = new BodyBuilder
			{
				HtmlBody = htmlMessage
			};
			message.Body = builder.ToMessageBody();
			using var smtp = new MailKit.Net.Smtp.SmtpClient();
			smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
			smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
			await smtp.SendAsync(message);
			smtp.Disconnect(true);
		}
	}
}
