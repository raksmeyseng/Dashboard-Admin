using AutoMapper;

namespace ArchtistStudio.Modules.Image;

public class ImageMapper : Profile
{
    public ImageMapper()
    {
        CreateMap<Image, ListImageResponse>();
         CreateMap<Image, DatailImageResponse>();
        CreateMap<InsertImageRequest, Image>()
           .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        CreateMap<Image, UpdateImageRequest>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore());
        CreateMap<ListImageResponse, Image>()
           .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        CreateMap<Image, ListImageResponse>()
            .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }
}
