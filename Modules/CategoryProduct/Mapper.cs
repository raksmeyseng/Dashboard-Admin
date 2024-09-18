using AutoMapper;

namespace ArchtistStudio.Modules.CategoryProduct;

public class CategoryProductMapper : Profile
{
    public CategoryProductMapper()
    {
        CreateMap<CategoryProduct, ListCategoryProductResponse>();
        CreateMap<InsertCategoryProductRequest, CategoryProduct>();
        CreateMap<CategoryProduct, UpdateCategoryProductRequest>();
    }
}
