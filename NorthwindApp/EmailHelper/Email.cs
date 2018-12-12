using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace EmailHelper
{
    public class Email
    {
        public static void Send(string to, string subject, string body)
        {
            MailMessage message = new MailMessage();
            message.To.Add(to);
            message.From = new MailAddress("joe@gmail.com", "Jay Fischer");
            message.Subject = subject;
            message.Body = body;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("jaydavidfischer@gmail.com", "fischerj12");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;

            client.Send(message);
        }
    }
}