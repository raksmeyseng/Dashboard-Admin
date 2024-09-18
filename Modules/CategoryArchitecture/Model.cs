
namespace ArchtistStudio.Modules.CategoryArchitecture;

public class ListCategoryArchitectureResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
   
}
 
public class InsertCategoryArchitectureRequest
{
    public string Name { get; set; } = null!;
   
}

public class UpdateCategoryArchitectureRequest
{
    public string? Name { get; set; }
}
