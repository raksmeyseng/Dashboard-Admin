using AutoMapper;

namespace ArchtistStudio.Modules.Product;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<Product, GetCategoryProductByProductResponse>();
        CreateMap<Product, ChangeProductRequest>();
    }
}
