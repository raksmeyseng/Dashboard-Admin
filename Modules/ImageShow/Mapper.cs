using AutoMapper;

namespace ArchtistStudio.Modules.ImageShow;

public class ImageShowMapper : Profile
{
    public ImageShowMapper()
    {
        CreateMap<ImageShow, ListImageShowResponse>();
         CreateMap<ImageShow, DatailImageShowResponse>();
        CreateMap<InsertImageShowRequest, ImageShow>()
           .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        CreateMap<ImageShow, UpdateImageShowRequest>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore());
        CreateMap<ListImageShowResponse, ImageShow>()
           .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        CreateMap<ImageShow, ListImageShowResponse>()
            .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }
}
