using AutoMapper;

namespace ArchtistStudio.Modules.Category;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<Category, ListCategoryResponse>();
        CreateMap<InsertCategoryRequest, Category>();
        CreateMap<Category, UpdateCategoryRequest>();
    }
}
