using System.ComponentModel.DataAnnotations;

namespace AnonymousMail.Shared.Command.Response.Mail
{
    public class MailMessageResponse
    {
        public string FromUserId { get; set; } = default!;
        public string FromUser { get; set; } = default!;
        [Required(ErrorMessage = "Укажите получателя")]
        public string ToUserId { get; set; } = default!;
        public string ToUser { get; set; } = default!;
        [Required(ErrorMessage = "Укажите тему сообщения")]
        [MaxLength(50, ErrorMessage = "Максимальная длина темы сообщения 50 символов")]
        public string Topic { get; set; } = default!;
        [Required(ErrorMessage = "Укажите текст сообщения")]
        public string Body { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
    }
}
