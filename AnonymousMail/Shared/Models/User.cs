namespace AnonymousMail.Shared.Models
{
    public class User
    {
        public User()
        {
            MailMessagesFromUsers = new HashSet<MailMessage>();
            MailMessagesToUsers = new HashSet<MailMessage>();
        }

        public string Id { get; set; } = default!;
        public string Username { get; set; } = default!;
        public virtual ICollection<MailMessage> MailMessagesFromUsers { get; set; }
        public virtual ICollection<MailMessage> MailMessagesToUsers { get; set; }
    }
}
