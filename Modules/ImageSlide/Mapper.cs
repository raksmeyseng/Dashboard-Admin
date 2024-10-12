using AutoMapper;

namespace ArchtistStudio.Modules.ImageSlide;

public class ImageSlideMapper : Profile
{
    public ImageSlideMapper()
    {
        CreateMap<ImageSlide, ListImageSlideResponse>();
         CreateMap<ImageSlide, DatailImageSlideResponse>();
         
        CreateMap<InsertImageSlideRequest, ImageSlide>()
           .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        CreateMap<ImageSlide, UpdateImageSlideRequest>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore());
        CreateMap<ListImageSlideResponse, ImageSlide>()
           .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        CreateMap<ImageSlide, ListImageSlideResponse>()
            .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }
}
