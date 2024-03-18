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
            //string MailBody = "Token Generated : " + Token;
            string MailBody = $"FundooNotes Reset Password : <a href=http://localhost:4200/reset/{Token}> Click Me </a>" ;
            Message.Subject = "FundoNotes Rest Password Link";
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
            string mailBody = $@"
                <html>
                <body>
                    <p style='font-size: 16px; font-family: Arial, sans-serif;'>
                        Hi {ToEmail.Split('@')[0]},<br/><br/>
                        {name.ToUpper()} ({email}) shared a note with you.<br/><br/>
                        <strong>{Title.ToUpper()}</strong><br/><br/>
                        Click the link below to view the shared note:<br/>
                        <a href='#'>View Shared Note</a><br/><br/>
                        Best regards,<br/>
                    </p>
                </body>
                </html>
            ";

            Message.Subject = $"Note shared with you: '{Title}'";
            Message.Body = mailBody;
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
