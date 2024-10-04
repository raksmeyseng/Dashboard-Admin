using AutoMapper;

namespace ArchtistStudio.Modules.Email;

public class EmailMapper : Profile
{
    public EmailMapper()
    {
        CreateMap<Email, ListEmailResponse>();
        CreateMap<Email, DetailEmailResponse>();
        CreateMap<InsertEmailRequest, Email>();
       CreateMap<Email, UpdateEmailRequest>();
    }
}
