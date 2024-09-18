
namespace ArchtistStudio.Modules.CategoryProduct;

public class ListCategoryProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
   
}
 
public class InsertCategoryProductRequest
{
    public string Name { get; set; } = null!;
   
}

public class UpdateCategoryProductRequest
{
    public string? Name { get; set; }
}
