using AutoMapper;

namespace ArchtistStudio.Modules.Product;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<Product, GetCategoryByProductResponse>();
        CreateMap<Product, ChangeProductRequest>();
    }
}
