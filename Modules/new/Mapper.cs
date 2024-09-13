using AutoMapper;

namespace ArchtistStudio.Modules.New;

public class NewMapper : Profile
{
    public NewMapper()
    {
        CreateMap<New, ListNewResponse>();
        CreateMap<InsertNewRequest, New>();
        CreateMap<New, UpdateNewRequest>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore()); 
    }
}
