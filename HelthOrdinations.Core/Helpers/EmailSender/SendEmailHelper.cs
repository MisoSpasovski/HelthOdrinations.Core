using System;
using System.Net.Mail;
using System.Text;

namespace HelthOrdinations.Core.Helpers.EmailSender
{
    public static class SendEmailHelper
    {
        public static void SendEmail(string emailTo, string body, string subject)
        {
            string from = "noreplay@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, emailTo);
            message.From = new MailAddress(from);
            message.Subject = subject;
            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("helthordinations@gmail.com", "ptrlddebafhoysaz");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;

            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

