
namespace ArchtistStudio.Modules.Category;

public class ListCategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
   
}
 
public class InsertCategoryRequest
{
    public string Name { get; set; } = null!;
   
}

public class UpdateCategoryRequest
{
    public string? Name { get; set; }
}
