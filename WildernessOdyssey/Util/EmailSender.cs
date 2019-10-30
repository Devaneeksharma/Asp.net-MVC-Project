using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.IO;

namespace WildernessOdyssey.Util
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "";

        public void Send(String toEmail, String subject, String contents,string filePath,string fileName)
        {
            
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("noreply@localhost.com", "Wilderness Odyssey");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(filePath))
            {
                var bytes = File.ReadAllBytes(filePath);
                var file = Convert.ToBase64String(bytes);
                msg.AddAttachment(fileName, file);
            }
            var response = client.SendEmailAsync(msg);

        }
    }
    
}