using AutoMapper;

namespace ArchtistStudio.Modules.CategoryArchitecture;

public class CategoryArchitectureMapper : Profile
{
    public CategoryArchitectureMapper()
    {
        CreateMap<CategoryArchitecture, ListCategoryArchitectureResponse>();
        CreateMap<InsertCategoryArchitectureRequest, CategoryArchitecture>();
        CreateMap<CategoryArchitecture, UpdateCategoryArchitectureRequest>();
    }
}
