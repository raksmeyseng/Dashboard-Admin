
namespace ArchtistStudio.Modules.NewDescription;

public class ListNewDescriptionResponse
{
    public Guid Id { get; set; }
public string Description { get; set; } = null!;
}
 
public class InsertNewDescriptionRequest
{
public string Description { get; set; } = null!;
}

public class UpdateNewDescriptionRequest
{
public string Description { get; set; } = null!;
}
