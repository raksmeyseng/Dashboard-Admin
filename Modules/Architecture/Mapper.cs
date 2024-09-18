using AutoMapper;

namespace ArchtistStudio.Modules.Architecture;

public class ArchitectureMapper : Profile
{
    public ArchitectureMapper()
    {
        CreateMap<Architecture, GetCategoryArchitectureByArchitectureResponse>();
        CreateMap<Architecture, ChangeArchitectureRequest>();
    }
}
