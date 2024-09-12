using AutoMapper;

namespace ArchtistStudio.Modules.TopManagement;

public class TopManagementMapper : Profile
{
    public TopManagementMapper()
    {
        CreateMap<TopManagement, ListTopManagementResponse>();
        CreateMap<TopManagement, DetailTopManagementResponse>();
        CreateMap<InsertTopManagementRequest, TopManagement>();
       CreateMap<TopManagement, UpdateTopManagementRequest>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore()); 
    }
}
