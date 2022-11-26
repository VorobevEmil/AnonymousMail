using AnonymousMail.Shared.Command.Response.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace AnonymousMail.Server.Hubs
{
    public class MailMessageHub : Hub
    {
        public async Task SendMailNotification(string fromUser, string toUserId)
        {
            await Clients.User(toUserId).SendAsync("ReceiveMailNotification", fromUser);
        }

        public async Task SendMailMessage(MailMessageResponse message, string toUserId)
        {
            await Clients.User(toUserId).SendAsync("ReceiveMailMessage", message);
        }
    }
}
