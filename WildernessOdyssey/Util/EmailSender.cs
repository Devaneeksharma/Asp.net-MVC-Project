using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.IO;

namespace WildernessOdyssey.Util
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents,string filePath,string fileName)
        {
            
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("noreply@localhost.com", "FIT5032 Example Email User");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var bytes = File.ReadAllBytes(filePath);
            var file = Convert.ToBase64String(bytes);
            msg.AddAttachment(fileName, file);
            var response = client.SendEmailAsync(msg);
        }
    }
    
}