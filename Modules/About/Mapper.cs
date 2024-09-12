using AutoMapper;

namespace ArchtistStudio.Modules.About;

public class AboutMapper : Profile
{
    public AboutMapper()
    {
        CreateMap<About, ListAboutResponse>();
        CreateMap<About, DetailAboutResponse>();
        CreateMap<InsertAboutRequest, About>();
        CreateMap<About, UpdateAboutRequest>()
          .ForMember(dest => dest.ImagePath, opt => opt.Ignore());
    }
}
