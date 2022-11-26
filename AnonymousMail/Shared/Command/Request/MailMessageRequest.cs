namespace AnonymousMail.Shared.Command.Request
{
    public class MailMessageRequest
    {
        public string ToUserId { get; set; } = default!;
        public string Topic { get; set; } = default!;
        public string Body { get; set; } = default!;
    }
}
