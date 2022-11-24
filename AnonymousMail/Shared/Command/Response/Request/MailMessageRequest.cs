using AnonymousMail.Shared.Models;

namespace AnonymousMail.Shared.Command.Response.Request
{
    public class MailMessageRequest
    {
        public string ToUserId { get; set; } = default!;
        public string Topic { get; set; } = default!;
        public string Body { get; set; } = default!;
    }
}
