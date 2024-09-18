
namespace ArchtistStudio.Modules.CategoryEngineering;

public class ListCategoryEngineeringResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
   
}
 
public class InsertCategoryEngineeringRequest
{
    public string Name { get; set; } = null!;
   
}

public class UpdateCategoryEngineeringRequest
{
    public string? Name { get; set; }
}
