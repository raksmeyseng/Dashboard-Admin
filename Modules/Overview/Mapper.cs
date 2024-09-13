using AutoMapper;

namespace ArchtistStudio.Modules.Overview;

public class OverviewMapper : Profile
{
    public OverviewMapper()
    {
        CreateMap<Overview, ListOverviewResponse>();
        CreateMap<InsertOverviewRequest, Overview>();
        CreateMap<Overview, UpdateOverviewRequest>()
             .ForMember(dest => dest.ImagePath, opt => opt.Ignore());
    }
}
