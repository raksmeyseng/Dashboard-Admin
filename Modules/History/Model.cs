
namespace ArchtistStudio.Modules.History;

public class ListHistoryResponse
{
    public Guid Id { get; set; }
  public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}
public class DetailHistoryResponse
{
  public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}
 
public class InsertHistoryRequest
{
  public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}


public class UpdateHistoryRequest
{
  public string? Name { get; set; } 
    public string? Description { get; set; } 
}
