﻿//using MailKit.Security;
//using Microsoft.AspNetCore.Identity.UI.Services;
//using Microsoft.Extensions.Options;
//using MimeKit;
//using System.Net.Mail;

//namespace LoyaltySystem.Api.Services
//{
//	public class EmailService(IOprions<MailSettings> mailSettings) : IEmailSender
//	{
//		private readonly MailSettings _mailSettings = mailSettings.Value;

//		public async Task SendEmailAsync(string email, string subject, string htmlMessage)
//		{
//			var message = new MimeMessage
//			{
//				Sender=MailboxAddress.Parse(_mailSettings),
//				Subject=subject,
//			};
//			message.To.Add(MailboxAddress.Parse(email));
//			var builder = new BodyBuilder()
//			{
//				HtmlBody = htmlMessage
//			};
//			message.Body=builder.ToMessageBody();
//			using var smtp = new SmtpClient();
//			smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
//			smtp.Authentication(_mailSettings.Mail, _mailSettings.Password);
//			await smtp.SendAsync(message);
//			smtp.Disconnect(true);
//		}
//	}
//}
