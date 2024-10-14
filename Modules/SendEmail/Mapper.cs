using AutoMapper;

namespace ArchtistStudio.Modules.SendEmail;

public class SendEmailMapper : Profile
{
    public SendEmailMapper()
    {
        CreateMap<InsertSendEmailRequest, SendEmail>();
    }
}
