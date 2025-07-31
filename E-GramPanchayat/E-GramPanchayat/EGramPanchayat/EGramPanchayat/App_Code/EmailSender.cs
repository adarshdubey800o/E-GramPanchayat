using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace EGramPanchayat.App_Code
{
    public class EmailSender
    {
        string MyEmailId, MyMailPassCode;
        public EmailSender()
        {
            MyEmailId = "atulpandey12071999@gmail.com";
            MyMailPassCode = "zneo wzvj oshk rofn";
        }
        internal bool SendEmail(string SendTo, string Subject, string MailBody)
        {
            try
            {
                //setting my email message
                MailMessage msg = new MailMessage();
                MailAddress maSender = new MailAddress(MyEmailId);
                //msg.Sender=new MailAddress(MyEmailId);
                msg.Sender = maSender;
                msg.To.Add(SendTo);
                msg.Subject = Subject;
                msg.Body = MailBody;
                msg.From = maSender;
                //Setting g authenticatuion of protocol
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                NetworkCredential nc = new NetworkCredential(MyEmailId, MyMailPassCode);
                client.Credentials = nc;
                client.Send(msg);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
