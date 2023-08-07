using EntityLayer.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ToolsLayer.String;
using VideoSite.Helpers;
using VideoSite.InMemoryData;

namespace VideoSite.Hubs
{
    public class VerifyEmailHub : Hub
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public VerifyEmailHub(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task SendVerifyEmailRequest(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || (await _userManager.FindByEmailAsync(email)) != null || !email.IsEmail())
            {
                
            }
            else
            {
                var httpcontext = Context.GetHttpContext();
                var val = new ViewModels.VerifyEmailHub.EmailVertificationWaitingClientsViewModel() { ClientId = Context.ConnectionId, Email = email };
                EmailVertificationWaitingClientsSource.EmailVertificationWaitingClientsViewModel.Add(val);
                SendEmail.Send($"<a href='{httpcontext.Request.Scheme + "://" + httpcontext.Request.Host + "/identity/verifyEmail/" + Uri.EscapeDataString(val.Guid)}'>Emailinizi Onaylayın</a>",email,httpcontext);
                await Clients.Caller.SendAsync("emailStatus", true);
            }
        }
        public async Task ForgotPassword(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                await Clients.Caller.SendAsync("emailExistence", false);
            }
            else if(!UserTokenSource.UserTokenViewModels.Any(x=>x.ClientId==Context.ConnectionId))
            {
                var httpcontext = Context.GetHttpContext();
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                UserTokenSource.UserTokenViewModels.Add(new ViewModels.VerifyEmailHub.UserTokenViewModel() { ClientId = Context.ConnectionId, Token = token, User = user });
                SendEmail.Send($"<a href='{ httpcontext.Request.Scheme + "://" + httpcontext.Request.Host + "/identity/ForgotPassword/" + Uri.EscapeDataString( token)}'>Emailinizi Onaylayın</a>", user.Email, httpcontext);
                await Clients.Caller.SendAsync("emailExistence", true);
            }
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            UserTokenSource.UserTokenViewModels.Remove(UserTokenSource.UserTokenViewModels.Find(x=>x.ClientId== Context.ConnectionId));
            EmailVertificationWaitingClientsSource.EmailVertificationWaitingClientsViewModel.Remove(EmailVertificationWaitingClientsSource.EmailVertificationWaitingClientsViewModel.Find(x => x.ClientId == Context.ConnectionId));
            return base.OnDisconnectedAsync(exception);
        }
    }
}
