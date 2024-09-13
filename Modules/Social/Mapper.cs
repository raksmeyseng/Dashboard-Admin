using AutoMapper;

namespace ArchtistStudio.Modules.Social;

public class SocialMapper : Profile
{
    public SocialMapper()
    {
        CreateMap<Social, ListSocialResponse>();
        CreateMap<InsertSocialRequest, Social>(); 
       CreateMap<Social, UpdateSocialRequest>();
    }
}
