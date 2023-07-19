using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace VideoSite.Hubs
{
    public class VerifyEmailHub:Hub
    {
        public async Task RecieveEmail(string email)
        {
            await Clients.All.SendAsync("allowed",email);
        }
        public async Task SendVerifyEmailRequest(string email)
        {
            var httpcontext = Context.GetHttpContext();
            MailMessage mm = new MailMessage();
            mm.SubjectEncoding = Encoding.Default;
            mm.Subject = "Email Onayla";
            mm.IsBodyHtml = true;
            mm.BodyEncoding = Encoding.Default;
            mm.Body = $"<a href='{httpcontext.Request.Scheme+"://"+ httpcontext.Request.Host+"/user/verifyemail/"+Context.ConnectionId}'>Emailinizi Onaylayın</a>";
            mm.From = new MailAddress("emailverify@sarowa36.com.tr");
            mm.To.Add(email);

            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.Host = "sarowa36.com.tr";
            smtp.EnableSsl = false;
            smtp.Credentials = new NetworkCredential("emailverify@sarowa36.com.tr", "Jtpc57^21");
            smtp.Send(mm);
        }
        public async Task VerifyEmail(string id)
        {
            await Clients.Client(id).SendAsync("EmailVerified");
        }
    }
}
