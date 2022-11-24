namespace AnonymousMail.Shared.Command.Response.Mail
{
    public class MailMessageResponse
    {
        public string FromUserId { get; set; } = default!;
        public string FromUser { get; set; } = default!;
        public string ToUserId { get; set; } = default!;
        public string ToUser { get; set; } = default!;
        public string Topic { get; set; } = default!;
        public string Body { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
    }
}
