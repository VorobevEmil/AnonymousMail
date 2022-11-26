using AnonymousMail.Shared.Command.Request;
using AnonymousMail.Shared.Command.Response.Mail;
using AutoMapper;

namespace AnonymousMail.Server.Common.Mapper.MailMessage
{
    public class MailMessageRequestProfile : Profile
    {
        public MailMessageRequestProfile()
        {
            CreateMap<MailMessageRequest, Shared.Models.MailMessage > ()
                .ForMember(message => message.Topic, opt =>
                    opt.MapFrom(src => src.Topic));

            CreateMap<MailMessageRequest, Shared.Models.MailMessage>()
                .ForMember(message => message.Body, opt =>
                    opt.MapFrom(src => src.Body));

            CreateMap<MailMessageRequest, Shared.Models.MailMessage>()
                .ForMember(message => message.ToUserId, opt =>
                    opt.MapFrom(src => src.ToUserId));
        }
    }
}
