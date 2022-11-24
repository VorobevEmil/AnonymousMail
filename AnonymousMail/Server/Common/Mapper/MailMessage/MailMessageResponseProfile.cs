using AnonymousMail.Shared.Command.Response.Mail;
using AutoMapper;

namespace AnonymousMail.Server.Common.Mapper.MailMessage
{
    public class MailMessageResponseProfile : Profile
    {
        public MailMessageResponseProfile()
        {
            CreateMap<Shared.Models.MailMessage, MailMessageResponse>()
                .ForMember(message => message.Topic, opt =>
                    opt.MapFrom(src => src.Topic))
                .ForMember(message => message.Body, opt =>
                    opt.MapFrom(src => src.Body))
                .ForMember(message => message.ToUserId, opt =>
                    opt.MapFrom(src => src.ToUserId))
                .ForMember(message => message.FromUserId, opt =>
                    opt.MapFrom(src => src.FromUserId))
                .ForMember(message => message.ToUser, opt =>
                    opt.MapFrom(src => src.ToUser.Username))
                .ForMember(message => message.FromUser, opt =>
                    opt.MapFrom(src => src.FromUser.Username));
        }
    }
}
