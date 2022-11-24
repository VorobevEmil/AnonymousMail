using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnonymousMail.Shared.Models
{
    public class MailMessage
    {
        public long Id { get; set; }
        public string FromUserId { get; set; } = default!;
        public User FromUser { get; set; } = default!;
        public string ToUserId { get; set; } = default!;
        public User ToUser { get; set; } = default!;
        public string Topic { get; set; } = default!;
        public string Body { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
    }
}
