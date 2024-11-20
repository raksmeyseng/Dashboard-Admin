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
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore())
            .ForMember(dest => dest.ImagePathWe, dest => dest.Ignore())
            .ForMember(dest => dest.ImagePathVision, dest => dest.Ignore())
            .ForMember(dest => dest.ImagePathService, dest => dest.Ignore())
            .ForMember(dest => dest.ImagePathProcess, dest => dest.Ignore())
            .ForMember(dest => dest.ImagePathPlanning, dest => dest.Ignore());
    }
}
