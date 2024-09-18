using AutoMapper;

namespace ArchtistStudio.Modules.CategoryEngineering;

public class CategoryEngineeringMapper : Profile
{
    public CategoryEngineeringMapper()
    {
        CreateMap<CategoryEngineering, ListCategoryEngineeringResponse>();
        CreateMap<InsertCategoryEngineeringRequest, CategoryEngineering>();
        CreateMap<CategoryEngineering, UpdateCategoryEngineeringRequest>();
    }
}
