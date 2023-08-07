using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Web.Helpers;

namespace VideoSite.Helpers
{
    public static class SendEmail
    {
        public static void Send(string message,string destinationEmail, HttpContext httpContext)
        {
            MailMessage mm = new MailMessage();
            mm.SubjectEncoding = Encoding.Default;
            mm.Subject = "Email Onayla";
            mm.IsBodyHtml = true;
            mm.BodyEncoding = Encoding.Default;
            mm.Body = message;
            mm.From = new MailAddress("emailverify@sarowa36.com.tr");
            mm.To.Add(destinationEmail);

            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.Host = "sarowa36.com.tr";
            smtp.EnableSsl = false;
            smtp.Credentials = new NetworkCredential("emailverify@sarowa36.com.tr", "Jtpc57^21");
            smtp.Send(mm);
        }
    }
}
