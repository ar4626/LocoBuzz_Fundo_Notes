using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Common_Layer.Utility
{
    public class Mail
    {

        public string SendMail(string ToEmail, string Token)
        {
            string FromEmail = "ar2646@srmist.edu.in";
            MailMessage Message = new MailMessage(FromEmail, ToEmail);
            string MailBody = "Token Generated : " + Token;
            Message.Subject = "Token Generated For Resetting Password";
            Message.Body = MailBody.ToString();
            Message.BodyEncoding = Encoding.UTF8;
            Message.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential credential
                = new NetworkCredential(FromEmail, "ckvc cppd gulr nfyd");

            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = credential;

            smtp.Send(Message);
            return ToEmail;
        }

        public string SendCollabMail(string ToEmail,string Title, string email, string name)
        {
            string FromEmail = "ar2646@srmist.edu.in";
            MailMessage Message = new MailMessage(FromEmail,ToEmail);
            string mailBody = $"{name.ToUpper()} ({email}) shared a note with you. \n {Title.ToUpper()}";
            Message.Subject = $"Note shared with you : '{Title}'";
            Message.Body = mailBody.ToString();
            Message.BodyEncoding = Encoding.UTF8;
            Message.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential credential
                = new NetworkCredential(FromEmail, "ckvc cppd gulr nfyd");
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = credential;

            smtp.Send(Message);
            return ToEmail;

        }
    }
}
