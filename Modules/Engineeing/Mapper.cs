using AutoMapper;

namespace ArchtistStudio.Modules.Engineeing;

public class EngineeingMapper : Profile
{
    public EngineeingMapper()
    {
        CreateMap<Engineeing, GetCategoryEngineeringByEngineeingResponse>();
        CreateMap<Engineeing, ChangeEngineeingRequest>();
    }
}
